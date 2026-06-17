# Capítulo 4: Cruce de Tablas (Join) en LINQ

## 1. ¿Qué es un Join y cuál es la lógica subyacente?

En las bases de datos relacionales normalizadas, la información está distribuida en múltiples tablas para evitar redundancia. Por ejemplo, la tabla `Products` no guarda el nombre de la categoría, solo guarda un número (`CategoryID`). Un Join, o cruce, es la operación que permite emparejar los registros de dos tablas diferentes utilizando una llave en común.

En lugar de hacer dos consultas separadas a la base de datos (una para traer productos y otra para traer categorías) y luego unirlas manualmente con un bucle `for` en la memoria RAM, lo cual colapsaría el servidor por lentitud, se utiliza la instrucción `Join` de LINQ. Entity Framework traduce esa instrucción a un `INNER JOIN` de SQL, y la base de datos, que está altamente optimizada para esta tarea, hace el cruce internamente y devuelve únicamente el resultado final ya fusionado.

---

## 2. Sintaxis de Join en LINQ: Query vs. Method

A diferencia del `Where` o el `Sum`, el cruce de tablas en C# se puede escribir de dos maneras. Profesionalmente, cuando se hacen cruces complejos, se prefiere la sintaxis de consulta (Query Syntax) porque resulta más fácil de leer que la sintaxis de métodos.

### Ejemplo Práctico: Unir Productos con Categorías

Supongamos que la Capa de Presentación necesita mostrar un catálogo web. Se ha creado un DTO llamado `ProductoDetalle` que espera recibir el nombre del producto, su precio y el nombre de su categoría como texto real, no el ID.

**Opción A: Sintaxis de Consulta (Recomendada para Joins)**

```csharp
public List<ProductoDetalle> ObtenerCatalogoProductos()
{
    var catalogo = (from p in _context.Products
                    join c in _context.Categories 
                    on p.CategoryID equals c.CategoryID // La llave en comun
                    select new ProductoDetalle          // Proyectamos directo al DTO
                    {
                        NombreProducto = p.ProductName,
                        NombreCategoria = c.CategoryName,
                        Precio = p.UnitPrice
                    }).ToList();
    return catalogo;
}
```

**Opción B: Sintaxis de Métodos (Lambda)**

El mismo cruce exacto, pero usando el método `.Join()`. Funciona igual, pero su lectura se vuelve confusa si se necesitan cruzar tres o cuatro tablas a la vez.

```csharp
public List<ProductoDetalle> ObtenerCatalogoMetodos()
{
    var catalogo = _context.Products
        .Join(
            _context.Categories,
            producto => producto.CategoryID,      // Llave de la tabla A
            categoria => categoria.CategoryID,    // Llave de la tabla B
            (producto, categoria) => new ProductoDetalle // Resultado combinado
            {
                NombreProducto = producto.ProductName,
                NombreCategoria = categoria.CategoryName,
                Precio = producto.UnitPrice
            }
        ).ToList();
    return catalogo;
}
```

Ambos enfoques anteriores generan exactamente el mismo tipo de cruce: un `INNER JOIN`, donde solo aparecen en el resultado las filas que tienen coincidencia en ambas tablas. Si una categoría no tiene ningún producto asociado, esa categoría simplemente no aparece en ninguno de los dos ejemplos anteriores. Las dos secciones siguientes resuelven justamente ese problema.

---

## 3. Combinaciones Agrupadas: GroupJoin

`GroupJoin` resuelve una necesidad distinta a la del `Join` clásico: en vez de devolver una fila por cada coincidencia entre las dos tablas, agrupa todos los elementos relacionados de la segunda tabla dentro de una sola colección por cada elemento de la primera. Es la forma natural de responder a la pregunta "para cada categoría, dame la lista completa de sus productos", en lugar de "dame una fila por cada producto junto a su categoría".

### Ejemplo: Cada categoría junto con todos sus productos

> **Referencia:** Proyecto `Northwind_Logica`, archivo `CategoriaConProductos_Logica.cs`.

```csharp
public List<CategoriaConProductos> ObtenerCategoriasConSusProductos()
{
    var resultado = (from c in _context.Categories
                      join p in _context.Products
                      on c.CategoryID equals p.CategoryID into productosDeLaCategoria
                      select new CategoriaConProductos
                      {
                          NombreCategoria = c.CategoryName,
                          Productos = productosDeLaCategoria.ToList() // Una lista completa por cada categoria
                      }).ToList();
    return resultado;
}
```

La palabra clave `into` es la que marca la diferencia frente a un `Join` normal: en lugar de aplanar el resultado en filas individuales, captura el grupo completo de productos que coinciden con cada categoría en una variable (`productosDeLaCategoria`), que después se puede convertir en lista, contar o recorrer. Nótese que, a diferencia del `Join` de la sección anterior, aquí cada categoría aparece una sola vez en el resultado, sin importar si tiene cero, uno o cien productos asociados.

---

## 4. Left Outer Join con DefaultIfEmpty

Tanto el `Join` como el `GroupJoin` vistos hasta ahora comparten una limitación: si una categoría no tiene ningún producto, simplemente no aparece en el resultado, o aparece con una lista de productos vacía pero la categoría sigue estando ahí solo en el caso de `GroupJoin`. Sin embargo, hay escenarios donde se necesita exactamente lo opuesto al comportamiento de un `Join` normal: traer todas las categorías sin excepción, incluso las que todavía no tienen productos cargados, para por ejemplo mostrar un reporte de inventario completo donde las categorías vacías también sean visibles.

Esto se conoce como Left Outer Join, y en LINQ se construye combinando un `GroupJoin` con el método `DefaultIfEmpty()`.

### Ejemplo: Todas las categorías, incluso sin productos

> **Referencia:** Proyecto `Northwind_Logica`, archivo `CategoriaConProductos_Logica.cs`, método `ObtenerTodasLasCategoriasConONoProductos`.

```csharp
public List<ProductoDetalle> ObtenerTodasLasCategoriasConONoProductos()
{
    var resultado = (from c in _context.Categories
                      join p in _context.Products
                      on c.CategoryID equals p.CategoryID into productosDeLaCategoria
                      from productoOpcional in productosDeLaCategoria.DefaultIfEmpty()
                      select new ProductoDetalle
                      {
                          NombreCategoria = c.CategoryName,
                          // Si no hay producto, dejamos el nombre y precio en valores por defecto
                          NombreProducto = productoOpcional != null ? productoOpcional.ProductName : "(Sin productos)",
                          Precio = productoOpcional != null ? productoOpcional.UnitPrice : 0
                      }).ToList();
    return resultado;
}
```

El paso clave es `productosDeLaCategoria.DefaultIfEmpty()`. Cuando una categoría sí tiene productos, este método no cambia nada: simplemente entrega esos productos uno por uno, igual que lo haría un `Join` normal. Pero cuando una categoría no tiene ningún producto asociado, en lugar de descartar esa categoría del resultado, `DefaultIfEmpty()` entrega un único elemento `null` en su lugar, lo que obliga a validar con `productoOpcional != null` antes de leer sus propiedades, para no provocar una excepción de referencia nula.

---

## 5. Ventajas y Desventajas del Uso de Joins en LINQ

| Aspecto | Evaluación Técnica |
| :--- | :--- |
| **Ventajas** | **Seguridad de Tipos:** El compilador de Visual Studio verifica que las llaves (`CategoryID`) sean del mismo tipo antes de ejecutar el programa, evitando errores en tiempo de ejecución.<br>**Protección SQL Injection:** LINQ parametriza automáticamente las consultas generadas por el Join, blindando la base de datos contra ataques cibernéticos. |
| **Desventajas** | **Verbosidad del Left Join:** Como se vio en la sección 4, lograr un comportamiento de "traer todo incluso sin coincidencia" requiere combinar `into` y `DefaultIfEmpty()`, una sintaxis considerablemente menos directa que escribir `LEFT JOIN` en SQL.<br>**Cuellos de Botella:** Unir más de 4 tablas masivas en un solo LINQ puede generar planes de ejecución subóptimos en SQL Server. En esos casos extremos, se prefieren Procedimientos Almacenados (Stored Procedures). |

---

## 6. Referencias

* [1] Microsoft, "Realizar combinaciones internas (LINQ en C#)," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-inner-joins](https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-inner-joins). [Accedido: 15-jun-2026].

* [2] Microsoft, "Sintaxis de consulta y sintaxis de método en LINQ," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/query-syntax-and-method-syntax-in-linq](https://learn.microsoft.com/es-es/dotnet/csharp/linq/query-syntax-and-method-syntax-in-linq). [Accedido: 15-jun-2026].

* [3] Microsoft, "Realizar combinaciones agrupadas (LINQ en C#)," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-grouped-joins](https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-grouped-joins). [Accedido: 16-jun-2026].

* [4] Microsoft, "Realizar combinaciones externas izquierdas (LINQ en C#)," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-left-outer-joins](https://learn.microsoft.com/es-es/dotnet/csharp/linq/perform-left-outer-joins). [Accedido: 16-jun-2026].

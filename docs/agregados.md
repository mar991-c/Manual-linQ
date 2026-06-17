# Capítulo 3: Funciones de Agregado y Agrupación en LINQ

## 1. ¿Qué son y cuál es la lógica por debajo?

En el desarrollo de software empresarial, es frecuente necesitar resúmenes estadísticos: totales, promedios, conteos. Las funciones de agregado de LINQ (`Count`, `Sum`, `Max`, `Min`, `Average`) permiten realizar estos cálculos matemáticos de forma nativa, sin tener que escribir bucles manuales para acumular resultados.

Si se necesita saber cuántos clientes compraron en Ecuador, la forma incorrecta sería traer toda la tabla a la memoria RAM del servidor y usar un bucle `foreach` para contarlos uno por uno. La forma correcta, usando LINQ to Entities, es ejecutar la función directamente dentro de la consulta. Al escribir `_context.Clientes.Where(c => c.Pais == "Ecuador").Count();`, LINQ traduce el comando a un `SELECT COUNT(*)` de SQL. El cálculo pesado lo hace el motor de la base de datos, que está diseñado y optimizado para precisamente ese tipo de operación, y a la memoria RAM de la aplicación solo viaja un número entero, por ejemplo `50`. Esto evita que el servidor colapse por saturación de memoria cuando la tabla crece.

---

## 2. Agrupación (GroupBy) y Proyecciones (Select)

Profesionalmente, las funciones de agregado rara vez se usan solas; casi siempre van acompañadas de un `GroupBy` (agrupación) y un `Select` (proyección hacia un DTO). El `GroupBy` toma una lista plana y la divide en subgrupos basados en una llave, por ejemplo agrupar todas las ventas por país. El `Select`, en este contexto, moldea el resultado final para que encaje exactamente en una clase plana o DTO (Data Transfer Object), evitando enviar entidades completas y pesadas a la Capa de Presentación.

### Ejemplo 1: Conteo y Agrupación (Count y GroupBy)

Basado en el DTO `CantidadCategoria` de nuestro sistema Northwind, calculamos cuántas órdenes existen por cada categoría.

> **Referencia:** Proyecto `Northwind_Logica`, archivo `CantidadCategoria_Logica.cs`.

```csharp
public List<CantidadCategoria> ObtenerOrdenesPorCategoria()
{
    var resultado = _context.OrderDetails
        .GroupBy(detalle => detalle.Product.Category.CategoryName) 
        .Select(grupo => new CantidadCategoria 
        {
            NombreCategoria = grupo.Key,         // .Key representa la llave de agrupacion
            NumeroOrdenes = grupo.Count()        // .Count() cuenta cuantos registros hay en este grupo
        })
        .OrderByDescending(c => c.NumeroOrdenes) 
        .ToList();
    return resultado;
}
```

### Ejemplo 2: Sumatoria y Cálculo Financiero (Sum)

Basado en el DTO `VentaPais`, sumamos el dinero total recaudado agrupado por la ubicación geográfica.

> **Referencia:** Proyecto `Northwind_Logica`, archivo `VentaPais_Logica.cs`.

```csharp
public List<VentaPais> ObtenerReporteVentasPorPais()
{
    var reporte = _context.Orders
        .GroupBy(orden => orden.Customer.Country)
        .Select(grupo => new VentaPais
        {
            Pais = grupo.Key,
            // Multiplicamos precio por cantidad y sumamos el total de todo el grupo
            TotalVentas = grupo.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity)),
            // Contamos los clientes unicos filtrando duplicados con Distinct
            NumeroClientes = grupo.Select(o => o.CustomerID).Distinct().Count() 
        })
        .ToList();
    return reporte;
}
```

Nótese que esta consulta ya usa `Distinct()` de paso, para contar cuántos clientes únicos hay detrás de un grupo de órdenes. Volveremos sobre este operador con más detalle en la sección 4 de este capítulo, porque conviene entender exactamente qué problema resuelve.

### Ejemplo 3: Máximos y Filtrados de Alto Nivel (OrderByDescending y FirstOrDefault)

Basado en el DTO `MejorEmpleadoTrimestre`, este es el patrón estándar para obtener el "Top 1" de un cálculo complejo.

> **Referencia:** Proyecto `Northwind_Logica`, archivo `MejorEmpleadoTrimestre_Logica.cs`.

```csharp
public MejorEmpleadoTrimestre ObtenerMejorEmpleado()
{
    var mejorEmpleado = _context.Orders
        .GroupBy(orden => orden.Employee.FirstName + " " + orden.Employee.LastName)
        .Select(grupo => new MejorEmpleadoTrimestre
        {
            NombreEmpleado = grupo.Key,
            MontoTotalOrden = grupo.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity))
        })
        .OrderByDescending(empleado => empleado.MontoTotalOrden) // Posiciona al que vendio mas en la cima
        .FirstOrDefault(); // Retorna estrictamente el primer elemento de la lista
    return mejorEmpleado;
}
```

### Ejemplo 4: Promedio (Average) y Ordenamiento (OrderBy)

Para obtener el precio promedio de los productos por categoría y ordenarlos de mayor a menor.

```csharp
public List<dynamic> ObtenerPreciosPromedio()
{
    using (var _context = new NorthwindContext())
    {
        return _context.Products
            .GroupBy(p => p.Category.CategoryName)
            .Select(g => new {
                Categoria = g.Key,
                PrecioPromedio = g.Average(p => p.UnitPrice) // Calcula el promedio
            })
            .OrderByDescending(x => x.PrecioPromedio)        // Ordena por el promedio
            .ToList<dynamic>();
    }
}
```

### Ejemplo 5: Valores Extremos por Grupo (Min y Max)

Hasta ahora se han usado `Count`, `Sum` y `Average`, pero quedan dos funciones de agregado igualmente importantes: `Min()` y `Max()`. Ambas devuelven, respectivamente, el valor más pequeño o más grande dentro de un grupo, y son especialmente útiles para detectar casos extremos: el producto más barato, el empleado con menos ventas, el pedido más antiguo.

> **Referencia:** Proyecto `Northwind_Logica`, archivo `RangoPrecioCategoria_Logica.cs`.

```csharp
public List<RangoPrecioCategoria> ObtenerRangoDePreciosPorCategoria()
{
    var rangos = _context.Products
        .GroupBy(p => p.Category.CategoryName)
        .Select(grupo => new RangoPrecioCategoria
        {
            NombreCategoria = grupo.Key,
            PrecioMinimo = grupo.Min(p => p.UnitPrice), // El producto mas barato del grupo
            PrecioMaximo = grupo.Max(p => p.UnitPrice)  // El producto mas caro del grupo
        })
        .ToList();
    return rangos;
}
```

A diferencia de `Sum` o `Average`, que recorren todos los elementos del grupo para acumular un resultado, `Min` y `Max` solo necesitan comparar valores entre sí. En SQL Server esto se traduce directamente a las funciones `MIN()` y `MAX()`, que suelen aprovechar los índices de la tabla para resolver la consulta sin necesidad de leer cada fila por separado.

---

## 3. Ventajas y Desventajas del Procesamiento de Agregados

| Aspecto | Evaluación Técnica |
| :--- | :--- |
| **Ventajas** | **Eficiencia de Red:** Al proyectar con `.Select()` hacia un DTO, solo viajan por la red los bytes estrictamente necesarios.<br>**Legibilidad de Código:** C# con LINQ es declarativo; es infinitamente más fácil leer un `.Sum()` encadenado que auditar bucles anidados. |
| **Desventajas** | **Generación de Consultas Complejas:** Agrupaciones múltiples pueden generar sentencias T-SQL ineficientes bajo el capó si la base de datos no tiene los índices correctos.<br>**Excepciones de Traducción:** Si se introduce un método propio de C# (ej. `.ToString()`) dentro de la expresión LINQ to Entities, el sistema fallará en tiempo de ejecución porque SQL Server no reconoce métodos de C#. |

---

## 4. Distinct: Eliminación de Duplicados

Aunque suele aparecer en la misma conversación que las funciones de agregado, `Distinct()` no es técnicamente una de ellas: no calcula un total ni un promedio, sino que elimina los valores repetidos de una colección. Se incluye en este capítulo porque casi siempre se combina con `Count()` para responder preguntas del tipo "¿cuántos valores diferentes hay aquí?", como ya se vio de paso en el Ejemplo 2.

La forma más simple de usarlo es sobre una lista de valores simples, por ejemplo para saber en qué países tiene clientes Northwind, sin que un mismo país aparezca repetido por cada cliente que vive ahí.

> **Referencia:** Proyecto `Northwind_Logica`, archivo `PaisesCliente_Logica.cs`.

```csharp
public List<string> ObtenerPaisesConClientes()
{
    var paises = _context.Customers
        .Select(c => c.Country)   // Primero proyectamos solo el campo Pais
        .Distinct()                // Luego eliminamos los duplicados
        .OrderBy(p => p)
        .ToList();
    return paises;
}
```

El orden de las operaciones importa: si se aplicara `Distinct()` antes del `Select()`, LINQ compararía objetos `Customer` completos (que casi nunca son idénticos entre sí, porque cada cliente tiene un ID distinto), en lugar de comparar solamente el texto del país. Proyectar primero con `Select()` y aplicar `Distinct()` después es lo que permite que la comparación se haga sobre el valor que realmente interesa.

---

## 5. Referencias

* [1] Microsoft, "Agrupación de datos en LINQ," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results](https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results). [Accedido: 15-jun-2026].

* [2] Microsoft, "Funciones de agregado (Transact-SQL)," *SQL Server Documentation*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/sql/t-sql/functions/aggregate-functions-transact-sql](https://learn.microsoft.com/es-es/sql/t-sql/functions/aggregate-functions-transact-sql). [Accedido: 15-jun-2026].

* [3] Microsoft, "Base de datos de ejemplo Northwind," *GitHub Sql Server Samples*. [Online]. Disponible en: [https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs). [Accedido: 15-jun-2026].

* [4] Microsoft, "Set operations (C#)," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/linq/set-operations](https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/linq/set-operations). [Accedido: 16-jun-2026].

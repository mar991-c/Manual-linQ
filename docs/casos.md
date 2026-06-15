# Capítulo 7: Casos de Estudio Aplicados

## 1. ¿Por qué Casos de Estudio?

Los capítulos anteriores explican cada técnica de LINQ de forma aislada. En la práctica profesional, sin embargo, una misma operación puede implementarse de **varias maneras distintas** dependiendo de la tecnología y el contexto del sistema.

Esta sección presenta cuatro casos reales que muestran LINQ aplicado en distintos escenarios: comparación de tecnologías de acceso a datos, agregados sobre grandes volúmenes, y sistemas sencillos completos con las 4 capas.

---

## 2. Caso de Estudio 1: Tres Formas de Implementar el CRUD de Pacientes

**Contexto del sistema:** Una clínica necesita un módulo para registrar, listar, actualizar y eliminar pacientes, cada uno asociado a un género (`Genero`). El sistema sigue la arquitectura de 4 capas: `GestionUsuariosEntidades` (modelos), `GestionUsuariosDatos` / `GestionUsuarios_DatosLinq` / `GestionUsuariosDatosEF` (capa de datos, en sus 3 variantes), `GestionUsuariosLogicaNegocio` (negocio) y `GestionUsuariosPresentacion` (formularios Windows Forms).

La entidad de transferencia es la misma para los tres enfoques:

> **Referencia:** Proyecto `GestionUsuarios`, archivo `GestionUsuariosEntidades/PacienteEntidades.cs`.

```csharp
public class PacienteEntidades
{
    public int ID { get; set; }
    public int Id_Genero { get; set; }
    public string Genero { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public bool Afiliado { get; set; }
    public string CodigoIESS { get; set; }
}
```

A continuación, el método **"Leer paciente por ID"** implementado en los tres enfoques.

### 2.1. Enfoque A: ADO.NET Puro (sin LINQ)

Este es el método tradicional, anterior a LINQ. Se construye manualmente el comando SQL como texto y se lee fila por fila con un `SqlDataReader`.

> **Referencia:** Proyecto `GestionUsuarios`, archivo `GestionUsuariosDatos/PacienteDatos.cs`.

```csharp
public static PacienteEntidades CargarPacientePorId(int id)
{
    try
    {
        SqlConnection conexion = new SqlConnection(GestionUsuariosDatos.Properties.Settings.Default.ConexionBD);
        conexion.Open();
        PacienteEntidades paciente = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conexion;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"SELECT p.[id], p.[Id_Genero], g.[nombre] as genero,
                                    p.[apellido], p.[nombres], p.[cedula],
                                    p.[telefono], p.[fechaNacimiento], p.[direccion],
                                    p.[CodigoIESS], p.[Afiliado]
                             FROM [dbo].[Pacientes] p
                             INNER JOIN Genero g ON p.Id_Genero = g.id
                             WHERE p.id = @id";
        cmd.Parameters.AddWithValue("@id", id);

        using (var dr = cmd.ExecuteReader())
        {
            while (dr.Read())
            {
                paciente = new PacienteEntidades();
                paciente.ID = Convert.ToInt32(dr["id"].ToString());
                paciente.Nombre = dr["nombres"].ToString();
                paciente.Genero = dr["genero"].ToString();
                // ... resto de propiedades
            }
        }
        conexion.Close();
        return paciente;
    }
    catch (Exception e)
    {
        string error = e.Message;
        return null;
    }
}
```

**Análisis:** El `INNER JOIN` y todos los nombres de columnas son texto plano (strings). Si alguien renombra la columna `nombres` en la base de datos, este código compila perfectamente pero falla en tiempo de ejecución.

### 2.2. Enfoque B: LINQ to SQL

Aquí la tabla `Pacientes` ya es una clase generada automáticamente (`Pacientes`, mapeada desde un archivo `.dbml`). Se usa `FirstOrDefault()` con una expresión lambda.

> **Referencia:** Proyecto `GestionUsuarios`, archivo `GestionUsuarios_DatosLinq/PacienteDatos.cs`.

```csharp
public static PacienteEntidades CargarPacientePorId(int id)
{
    try
    {
        PacienteEntidades paciente = new PacienteEntidades();
        using (Programacion_avanzadaDataContext contexto = new Programacion_avanzadaDataContext())
        {
            Pacientes _pacienteLinQ = contexto.Pacientes.FirstOrDefault(p => p.id == id);
            paciente.ID = _pacienteLinQ.id;
            paciente.Id_Genero = _pacienteLinQ.Id_Genero ?? 0;
            paciente.Cedula = _pacienteLinQ.cedula;
            paciente.Nombre = _pacienteLinQ.nombres;
            paciente.Apellido = _pacienteLinQ.apellido;
            paciente.Telefono = _pacienteLinQ.telefono;
            paciente.Direccion = _pacienteLinQ.direccion;
            paciente.FechaNacimiento = _pacienteLinQ.fechaNacimiento;
            paciente.Afiliado = _pacienteLinQ.Afiliado ?? false;
            paciente.CodigoIESS = _pacienteLinQ.CodigoIESS;
            return paciente;
        }
    }
    catch (Exception)
    {
        throw;
    }
}
```

**Análisis:** Ya no hay SQL en texto. `p.id == id` es código fuertemente tipado: si la columna `id` no existe en la clase `Pacientes`, Visual Studio marca el error antes de ejecutar. Sin embargo, el género (`Genero`) no viene incluido automáticamente — hay que resolverlo aparte con `GeneroDatos.DevolverNombreGeneroPorId(...)`, porque LINQ to SQL no trae relaciones anidadas tan fácilmente como EF.

### 2.3. Enfoque C: Entity Framework (con `.Include()`)

Esta es la versión más moderna. Usa `DbContext` y resuelve la relación `Paciente → Genero` en la misma consulta gracias a `.Include()` (Eager Loading, visto en el Capítulo 4).

> **Referencia:** Proyecto `GestionUsuarios`, archivo `GestionUsuariosDatosEF/PacienteDatos.cs`.

```csharp
public static PacienteEntidades CargarPacientePorId(int id)
{
    try
    {
        PacienteEntidades paciente = new PacienteEntidades();
        using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
        {
            var _pacienteEF = contexto.Pacientes
                         .Include("Genero")
                         .FirstOrDefault(p => p.id == id);

            paciente.ID = _pacienteEF.id;
            paciente.Id_Genero = _pacienteEF.Id_Genero ?? 0;
            paciente.Cedula = _pacienteEF.cedula;
            paciente.Nombre = _pacienteEF.nombres;
            paciente.Apellido = _pacienteEF.apellido;
            paciente.Telefono = _pacienteEF.telefono;
            paciente.Direccion = _pacienteEF.direccion;
            paciente.FechaNacimiento = _pacienteEF.fechaNacimiento;
            paciente.Afiliado = _pacienteEF.Afiliado ?? false;
            paciente.CodigoIESS = _pacienteEF.CodigoIESS ?? "";
            return paciente;
        }
    }
    catch (Exception)
    {
        throw;
    }
}
```

**Análisis:** Con una sola línea (`.Include("Genero")`), la relación con `Genero` viaja junto con el paciente, evitando el problema N+1 explicado en el Capítulo 4. Es el código más corto y más fácil de mantener de los tres.

### 2.4. Comparación General

| Criterio | ADO.NET Puro | LINQ to SQL | Entity Framework |
| :--- | :--- | :--- | :--- |
| **Detección de errores** | Solo en tiempo de ejecución (SQL en texto) | En tiempo de compilación | En tiempo de compilación |
| **Manejo de relaciones (Genero)** | Manual, vía `INNER JOIN` en SQL | Manual, con métodos separados | Automático con `.Include()` |
| **Cantidad de código** | Alta (SQL + lectura manual de columnas) | Media | Baja |
| **Curva de aprendizaje** | Baja (SQL clásico) | Media | Alta (requiere entender el ORM) |
| **Uso recomendado actualmente** | Proyectos legados o consultas muy específicas | Proyectos antiguos en .NET Framework | Estándar actual para nuevos proyectos |

---

## 3. Caso de Estudio 2: Reporte "Top 10 Clientes" (Northwind)

**Contexto:** El área comercial de Northwind necesita identificar a los 10 clientes que más dinero han generado históricamente, junto con su país y número de órdenes, para una campaña de fidelización.

**Capas involucradas:**
* `Datos_LinQ` → consulta LINQ to SQL sobre la vista `vw_GestionOrdenesPorEmpleados`.
* `Northwind_Logica` → expone el resultado a la capa superior sin transformarlo (regla de "no mezclar capas").
* `Northwind_Presentacion` → muestra el listado, por ejemplo en una tabla o gráfico de barras.

> **Referencia:** Proyecto `Northwind`, archivo `Datos_LinQ/ClienteGastos_Datos.cs`.

```csharp
public static class ClienteGastos_Datos
{
    public static List<ClienteGasto> ObtenerTop10Clientes()
    {
        using (DataClasses1DataContext contexto = new DataClasses1DataContext())
        {
            var consulta = contexto.vw_GestionOrdenesPorEmpleados
                .GroupBy(v => new { v.CompanyName, v.Country })
                .Select(grupo => new ClienteGasto
                {
                    NombreCliente = grupo.Key.CompanyName,
                    Pais = grupo.Key.Country,
                    TotalGastado = grupo.Sum(v => v.montoReacudado) ?? 0,
                    NumeroOrdenes = grupo.Count()
                })
                .OrderByDescending(x => x.TotalGastado)
                .Take(10)
                .ToList();

            return consulta;
        }
    }
}
```

**Análisis técnico:** Esta consulta combina **cuatro técnicas** vistas en capítulos anteriores en una sola instrucción:

* `GroupBy` con una llave compuesta (`new { v.CompanyName, v.Country }`) — agrupa por dos columnas a la vez (Capítulo 3).
* `Sum()` y `Count()` — funciones de agregado dentro del `Select` (Capítulo 3).
* `OrderByDescending()` — ordena del cliente que más gastó al que menos (Capítulo 3).
* `Take(10)` — paginación simplificada: toma solo el "Top 10" sin necesidad de `Skip()` (Capítulo 6).

> **Referencia:** Proyecto `Northwind`, archivo `Northwind_Logica/ClienteGasto_Logica.cs`.

```csharp
public static class ClienteGasto_Logica
{
    public static List<ClienteGasto> ObtenerTop10Clientes()
    {
        return ClienteGastos_Datos.ObtenerTop10Clientes();
    }
}
```

Nótese que la Capa de Negocio aquí actúa como un "intermediario transparente": no añade lógica adicional porque la consulta ya viene completamente resuelta desde la Capa de Datos. Esto respeta la regla de la Capa de Presentación: nunca debe llamar directamente a `Datos_LinQ`, sino siempre a través de `Northwind_Logica`.

---

## 4. Caso de Estudio 3: Sistema de Biblioteca

**Contexto:** Una institución educativa gestiona su colección de libros: registrar títulos, consultar disponibilidad, buscar por género y obtener estadísticas de la colección. Este caso ilustra cómo las mismas instrucciones LINQ operan de forma diferente según la capa — en la Capa de Datos generan SQL, en la Capa de Negocio operan en memoria.

> **Referencia:** Proyecto `BibliotecaApp`, archivo `Biblioteca.Entidades/LibroEntidad.cs`.

```csharp
public class LibroEntidad
{
    public int ID { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int AnioPub { get; set; }
    public bool Disponible { get; set; }
}
```

### 4.1. Capa de Datos — LINQ to Entities

`Where` y `OrderBy` se ejecutan en SQL Server, no en RAM. El `Select` proyecta solo los campos necesarios, reduciendo el tráfico de datos.

> **Referencia:** Proyecto `BibliotecaApp`, archivo `Biblioteca.Datos/LibroDatos.cs`, método `ObtenerLibrosDisponibles`.

```csharp
public static List<LibroEntidad> ObtenerLibrosDisponibles()
{
    using (var contexto = new BibliotecaContext())
    {
        return contexto.Libros
            .Where(l => l.Disponible == true)   // Se traduce a: WHERE Disponible = 1
            .OrderBy(l => l.Titulo)             // Se traduce a: ORDER BY Titulo
            .Select(l => new LibroEntidad
            {
                ID        = l.ID,
                Titulo    = l.Titulo,
                Autor     = l.Autor,
                Genero    = l.Genero,
                AnioPub   = l.AnioPub,
                Disponible = l.Disponible
            })
            .ToList();                          // Aquí se ejecuta el SQL
    }
}
```

El DELETE sigue el patrón estándar: buscar con `FirstOrDefault`, verificar que existe, luego eliminar.

> **Referencia:** Proyecto `BibliotecaApp`, archivo `Biblioteca.Datos/LibroDatos.cs`, método `EliminarLibro`.

```csharp
public static bool EliminarLibro(int id)
{
    using (var contexto = new BibliotecaContext())
    {
        var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);
        if (libro == null) return false;

        contexto.Libros.Remove(libro);
        contexto.SaveChanges();
        return true;
    }
}
```

### 4.2. Capa de Negocio — LINQ to Objects

Una vez que los datos llegan como `List<LibroEntidad>`, los filtros adicionales se aplican **en memoria**, sin volver a consultar la base de datos.

> **Referencia:** Proyecto `BibliotecaApp`, archivo `Biblioteca.Negocio/LibroNegocio.cs`.

```csharp
// El Where aquí recorre una lista en RAM, no genera SQL
public static List<LibroEntidad> FiltrarPorGenero(string genero)
{
    List<LibroEntidad> todosLosLibros = LibroDatos.ObtenerLibrosDisponibles();

    return todosLosLibros
        .Where(l => l.Genero.ToLower() == genero.ToLower())
        .ToList();
}

// GroupBy en memoria: produce un diccionario género → cantidad de libros
public static Dictionary<string, int> ContarPorGenero()
{
    List<LibroEntidad> todosLosLibros = LibroDatos.ObtenerLibrosDisponibles();

    return todosLosLibros
        .GroupBy(l => l.Genero)
        .ToDictionary(g => g.Key, g => g.Count());
}
```

**Diferencia clave con el Caso 1:** El `Where` de `LibroDatos.cs` genera `WHERE` en SQL. El `Where` de `LibroNegocio.cs` itera una lista ya cargada. El código se ve igual pero el motor de ejecución es completamente distinto.

---

## 5. Caso de Estudio 4: Sistema de Inventario

**Contexto:** Una tienda controla su stock: detectar productos por agotarse, calcular el valor del inventario por categoría y procesar ventas validando existencias. Este caso muestra cómo **combinar múltiples agregados en una sola consulta** y cómo la Capa de Negocio protege la integridad de los datos con reglas de negocio.

> **Referencia:** Proyecto `InventarioApp`, archivo `Inventario.Entidades/ProductoEntidad.cs`.

```csharp
public class ProductoEntidad
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

// DTO: clase liviana solo para transferir datos procesados, nunca se guarda en BD
public class ResumenCategoriaEntidad
{
    public string NombreCategoria { get; set; }
    public int TotalProductos { get; set; }
    public decimal ValorTotalInventario { get; set; }
    public decimal PrecioPromedio { get; set; }
    public int StockMinimo { get; set; }
}
```

### 5.1. Capa de Datos — Cuatro agregados en una sola consulta

Esta consulta combina `Count`, `Sum`, `Average` y `Min` en un único `GroupBy`, generando un solo `GROUP BY` en SQL Server.

> **Referencia:** Proyecto `InventarioApp`, archivo `Inventario.Datos/ProductoDatos.cs`, método `ObtenerResumenPorCategoria`.

```csharp
public static List<ResumenCategoriaEntidad> ObtenerResumenPorCategoria()
{
    using (var contexto = new InventarioContext())
    {
        return contexto.Productos
            .GroupBy(p => p.Categoria.Nombre)
            .Select(grupo => new ResumenCategoriaEntidad
            {
                NombreCategoria      = grupo.Key,
                TotalProductos       = grupo.Count(),
                ValorTotalInventario = grupo.Sum(p => p.Precio * p.Stock),
                PrecioPromedio       = grupo.Average(p => p.Precio),
                StockMinimo          = grupo.Min(p => p.Stock)
            })
            .OrderByDescending(r => r.ValorTotalInventario)
            .ToList();
    }
}
```

El SQL equivalente que genera EF Core es:

```sql
SELECT Categorias.Nombre,
       COUNT(*) AS TotalProductos,
       SUM(Precio * Stock) AS ValorTotalInventario,
       AVG(Precio) AS PrecioPromedio,
       MIN(Stock) AS StockMinimo
FROM Productos
INNER JOIN Categorias ON Productos.CategoriaID = Categorias.ID
GROUP BY Categorias.Nombre
ORDER BY ValorTotalInventario DESC
```

### 5.2. Capa de Negocio — Regla de negocio antes de modificar datos

La validación de stock no ocurre en la interfaz ni en la base de datos, sino exclusivamente en la Capa de Negocio. Si el stock es insuficiente, la operación se detiene antes de llegar a `SaveChanges()`.

> **Referencia:** Proyecto `InventarioApp`, archivo `Inventario.Negocio/ProductoNegocio.cs`, método `ProcesarVenta`.

```csharp
public static string ProcesarVenta(int idProducto, int cantidadVendida)
{
    // 1. Obtiene datos de la Capa de Datos (ya en memoria como List<T>)
    var producto = ProductoDatos.ObtenerTodos()
        .FirstOrDefault(p => p.ID == idProducto);  // LINQ to Objects

    if (producto == null)
        return "Error: Producto no encontrado.";

    // 2. Regla de negocio: ¿Hay stock suficiente?
    if (producto.Stock < cantidadVendida)
        return $"Error: Stock insuficiente. Disponible: {producto.Stock} unidades.";

    // 3. Solo si pasa la validación, se actualiza la base de datos
    int nuevoStock = producto.Stock - cantidadVendida;
    ProductoDatos.ActualizarStock(idProducto, nuevoStock);
    return $"Venta registrada. Stock actualizado a {nuevoStock} unidades.";
}
```

### 5.3. Resumen de agregados usados

| Función | Resultado en el reporte |
| :--- | :--- |
| `Count()` | Total de productos por categoría |
| `Sum(p => p.Precio * p.Stock)` | Valor económico total del inventario |
| `Average(p => p.Precio)` | Precio promedio de la categoría |
| `Min(p => p.Stock)` | Producto con menos existencias |
| `OrderByDescending` | Categorías más valiosas primero |

---

## 6. Referencias (Formato IEEE)

* [1] Microsoft, "LINQ to SQL," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/](https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/). [Accedido: 15-jun-2026].

* [2] Microsoft, "Introducción a Entity Framework," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/ef6/](https://learn.microsoft.com/es-es/ef/ef6/). [Accedido: 15-jun-2026].

* [3] Microsoft, "ADO.NET con SqlClient," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql-server-connections](https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql-server-connections). [Accedido: 15-jun-2026].

* [4] Microsoft, "Agrupación de datos (C#) — GroupBy," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results](https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results). [Accedido: 15-jun-2026].

* [5] Microsoft, "Guardado de datos básico — Entity Framework Core," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/core/saving/basic](https://learn.microsoft.com/es-es/ef/core/saving/basic). [Accedido: 15-jun-2026].

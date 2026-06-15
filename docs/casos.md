# Capítulo 7: Casos de Estudio Aplicados

## 1. ¿Por qué Casos de Estudio?

Los capítulos anteriores explican cada técnica de LINQ de forma aislada. En la práctica profesional, sin embargo, una misma operación (por ejemplo, "leer un paciente por su ID") puede implementarse de **varias maneras distintas** dependiendo de la tecnología de acceso a datos elegida.

Esta sección compara tres proyectos académicos reales que resuelven el **mismo problema** (un sistema de gestión de pacientes, capa por capa), pero usando tres enfoques diferentes de acceso a datos. El objetivo es que el lector entienda **cuándo y por qué** se elige cada uno.

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

**Análisis técnico:**

Esta consulta combina **cuatro técnicas** vistas en capítulos anteriores en una sola instrucción:

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

Nótese que la Capa de Negocio aquí actúa como un "intermediario transparente": no añade lógica adicional porque, en este caso, la consulta ya viene completamente resuelta desde la Capa de Datos. Esto respeta la regla de la Capa de Presentación: nunca debe llamar directamente a `Datos_LinQ`, sino siempre a través de `Northwind_Logica`.

---

## 4. Referencias (Formato IEEE)

* [1] Microsoft, "LINQ to SQL," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/](https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/). [Accedido: 15-jun-2026].

* [2] Microsoft, "Introducción a Entity Framework," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/ef6/](https://learn.microsoft.com/es-es/ef/ef6/). [Accedido: 15-jun-2026].

* [3] Microsoft, "ADO.NET con SqlClient," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql-server-connections](https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql-server-connections). [Accedido: 15-jun-2026].
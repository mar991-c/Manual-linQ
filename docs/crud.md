# Capítulo 5: Operaciones CRUD con LINQ y Entity Framework

## 1. ¿Qué es el CRUD y la Lógica del Change Tracking?

El acrónimo CRUD representa las cuatro operaciones fundamentales de persistencia de datos: Create (insertar), Read (leer), Update (actualizar) y Delete (eliminar).

En lugar de escribir comandos SQL manuales, propensos a inyecciones SQL y errores de sintaxis, se utiliza Entity Framework Core. Esta tecnología funciona gracias a un mecanismo llamado Change Tracking, o rastreo de cambios. Cuando se obtiene un cliente de la base de datos usando LINQ, EF Core guarda ese objeto en la memoria RAM y lo vigila. Si se cambia el nombre del cliente en C# y se ejecuta `.SaveChanges()`, EF Core detecta la modificación por sí solo y genera automáticamente el comando `UPDATE` exacto que hace falta enviar a la base de datos, sin que el desarrollador tenga que escribirlo a mano.

---

## 2. Una Decisión de Diseño: Clases Estáticas

Para los ejemplos de este capítulo se implementa el CRUD utilizando clases `static` en las capas de Datos y Negocio. Esta no es la única forma válida de hacerlo, pero resulta conveniente en sistemas donde las transacciones son directas y no requieren mantener datos persistentes en la memoria del usuario entre clics, es decir, en operaciones *stateless*.

Instanciar la clase de acceso a datos con `new CapaDatos()` una y otra vez consume recursos innecesarios y sobrecarga el recolector de basura del servidor. Al declarar la clase y sus métodos como estáticos, las funciones se cargan en la memoria de la aplicación una sola vez cuando el sistema arranca, lo que acelera los tiempos de respuesta. En proyectos más grandes, sobre todo aquellos que usan inyección de dependencias, suele preferirse en su lugar una clase de instancia inyectada como repositorio; ambos enfoques son válidos, y la elección depende del tamaño y las necesidades del sistema.

---

## 3. Implementación del CRUD (Tabla Customers)

La tabla `Customers` de Northwind utiliza un `string` de 5 caracteres como llave primaria (`CustomerID`), a diferencia de los clásicos enteros autonuméricos.

### 3.1. Create (Insertar)

Para insertar, se crea el objeto, se añade al contexto con `.Add()` y se guardan los cambios. La clase estática maneja su propio ciclo de vida de conexión usando el bloque `using`.

```csharp
public static class ClienteDatos
{
    public static void InsertarCliente(Customer nuevoCliente)
    {
        // El 'using' asegura que la conexion a la BD se cierre y destruya al terminar
        using (var _context = new NorthwindContext())
        {
            _context.Customers.Add(nuevoCliente);
            _context.SaveChanges(); // Aqui se ejecuta el INSERT en SQL Server
        }
    }
}
```

### 3.2. Read (Leer por ID)

Para buscar un registro específico, se utiliza el método LINQ `.FirstOrDefault()`.

```csharp
public static Customer ObtenerClientePorId(string idCliente)
{
    using (var _context = new NorthwindContext())
    {
        // Retorna el cliente si el ID coincide, o null si no existe
        return _context.Customers.FirstOrDefault(c => c.CustomerID == idCliente);
    }
}
```

### 3.3. Update (Actualizar)

Aprovechando el rastreo de cambios de EF Core, se modifica el objeto y se le indica al contexto que su estado ha cambiado a "modificado" usando `.Update()`.

```csharp
public static void ActualizarCliente(Customer clienteModificado)
{
    using (var _context = new NorthwindContext())
    {
        _context.Customers.Update(clienteModificado);
        _context.SaveChanges(); // Aqui se ejecuta el UPDATE en SQL Server
    }
}
```

### 3.4. Delete (Eliminar)

La lógica de eliminación exige primero consultar si el registro existe en la base de datos. Si existe, se procede a removerlo.

```csharp
public static void EliminarCliente(string idCliente)
{
    using (var _context = new NorthwindContext())
    {
        // 1. Buscamos el cliente
        var cliente = _context.Customers.FirstOrDefault(c => c.CustomerID == idCliente);
        // 2. Validamos que exista
        if (cliente != null)
        {
            _context.Customers.Remove(cliente); // 3. Lo marcamos para eliminacion
            _context.SaveChanges();             // 4. Se ejecuta el DELETE en SQL Server
        }
    }
}
```

---

## 4. La Versión Asíncrona: SaveChangesAsync

Todos los ejemplos anteriores usan métodos síncronos: mientras `SaveChanges()` espera la respuesta de la base de datos, el hilo de ejecución que lo llamó queda bloqueado, sin poder hacer nada más. En una aplicación de escritorio pequeña esto rara vez se nota, pero en aplicaciones web que atienden a muchos usuarios simultáneamente, bloquear un hilo por cada operación de base de datos puede agotar los recursos del servidor.

Por eso, EF Core ofrece una versión asíncrona de prácticamente todos sus métodos de consulta y guardado, identificable porque su nombre termina en `Async`: `ToListAsync()`, `FirstOrDefaultAsync()`, `SaveChangesAsync()`. Estas versiones, combinadas con las palabras clave `async` y `await` de C#, liberan el hilo mientras se espera la respuesta de la base de datos, permitiendo que el servidor atienda otras solicitudes en ese tiempo de espera.

El método `InsertarCliente` de la sección 3.1, reescrito en su forma asíncrona, se vería así:

```csharp
public static async Task InsertarClienteAsync(Customer nuevoCliente)
{
    using (var _context = new NorthwindContext())
    {
        await _context.Customers.AddAsync(nuevoCliente);
        await _context.SaveChangesAsync(); // El hilo queda libre mientras se espera la respuesta de la BD
    }
}
```

La estructura del método es prácticamente idéntica a la versión síncrona; el cambio real está en tres lugares: el método ahora retorna `Task` en lugar de `void`, está marcado con la palabra clave `async`, y cada llamada a un método de EF Core lleva `await` delante. Esta es la convención que se recomienda seguir en proyectos nuevos, especialmente en aplicaciones web con ASP.NET, mientras que los ejemplos síncronos de la sección 3 siguen siendo perfectamente válidos para aplicaciones de escritorio o consola donde la concurrencia no es una preocupación central.

---

## 5. Ventajas y Desventajas del CRUD con EF Core

| Aspecto | Evaluación Técnica |
| :--- | :--- |
| **Ventajas** | **Prevención de Errores:** Visual Studio revisa el código en tiempo de compilación. Si se intenta asignar un texto a un campo numérico, el programa no compilará.<br>**Mantenibilidad:** Se evita tener cadenas de texto SQL gigantes regadas por todo el código, facilitando la lectura y las pruebas. |
| **Desventajas** | **Operaciones Masivas (Bulk):** El Change Tracking es excelente para uno o cien registros, pero si se necesita actualizar cien mil clientes al mismo tiempo, el seguimiento puede saturar la memoria RAM. Para esos casos, se prefiere usar sentencias SQL directas (Raw SQL) o librerías externas como EF Core Bulk Extensions. |

---

## 6. Referencias

* [1] Microsoft, "Guardado de datos en Entity Framework Core," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/core/saving/](https://learn.microsoft.com/es-es/ef/core/saving/). [Accedido: 15-jun-2026].

* [2] Microsoft, "Seguimiento de cambios en EF Core," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/core/change-tracking/](https://learn.microsoft.com/es-es/ef/core/change-tracking/). [Accedido: 15-jun-2026].

* [3] Microsoft, "Clases estáticas y sus miembros (Guía de programación de C#)," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members](https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members). [Accedido: 15-jun-2026].

* [4] Microsoft, "Operaciones asíncronas de consultas y guardado," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/core/miscellaneous/async](https://learn.microsoft.com/es-es/ef/core/miscellaneous/async). [Accedido: 16-jun-2026].

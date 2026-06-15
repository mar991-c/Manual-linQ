\# Capítulo 5: Operaciones CRUD con Capas Estáticas



\## 1. ¿Qué es el CRUD y la Lógica del Change Tracking?



El acrónimo \*\*CRUD\*\* representa las cuatro operaciones fundamentales de persistencia de datos: \*\*C\*\*reate (Insertar), \*\*R\*\*ead (Leer), \*\*U\*\*pdate (Actualizar) y \*\*D\*\*elete (Eliminar).



En lugar de escribir comandos SQL manuales (que son propensos a inyecciones SQL y errores de sintaxis), utilizamos \*\*Entity Framework Core (EF Core)\*\*. 



\*\*¿Por qué se hace de esta manera?\*\*

EF Core utiliza un patrón llamado \*\*Change Tracking\*\* (Rastreo de Cambios). Cuando obtienes un cliente de la base de datos usando LINQ, EF Core guarda ese objeto en la memoria RAM y "lo vigila". Si cambias el nombre del cliente en C# y ejecutas `.SaveChanges()`, EF Core detecta la modificación y genera automáticamente el comando `UPDATE` exacto para la base de datos.



\---



\## 2. El Estándar Profesional: Clases Estáticas (Static)



Para este manual, implementaremos el CRUD utilizando clases `static` en nuestras capas de Datos y Negocio.



\*\*¿Cuándo y por qué se utiliza?\*\*

En sistemas donde las transacciones son directas y no requieren mantener datos persistentes en la memoria del usuario entre clics (operaciones \*stateless\*), instanciar la clase de acceso a datos con `new CapaDatos()` una y otra vez consume recursos innecesarios y sobrecarga el Recolector de Basura (Garbage Collector) del servidor. 



Al declarar la clase y sus métodos como estáticos (`static`), las funciones se cargan en la memoria de la aplicación una sola vez cuando el sistema arranca. Esto acelera drásticamente los tiempos de respuesta.



\---



\## 3. Implementación del CRUD (Tabla Customers)



La tabla `Customers` de Northwind utiliza un `string` de 5 caracteres como Llave Primaria (`CustomerID`), a diferencia de los clásicos enteros autonuméricos.



\### 3.1. Create (Insertar)

Para insertar, creamos el objeto, lo añadimos al contexto con `.Add()` y guardamos los cambios. La clase estática maneja su propio ciclo de vida de conexión usando el bloque `using`.



```csharp

public static class ClienteDatos

{

&#x20;   public static void InsertarCliente(Customer nuevoCliente)

&#x20;   {

&#x20;       // El 'using' asegura que la conexión a la BD se cierre y destruya al terminar

&#x20;       using (var \_context = new NorthwindContext())

&#x20;       {

&#x20;           \_context.Customers.Add(nuevoCliente);

&#x20;           \_context.SaveChanges(); // Aquí se ejecuta el INSERT en SQL Server

&#x20;       }

&#x20;   }

}

\### 3.2. Read (Leer por ID)

Para buscar un registro específico, utilizamos el método LINQ `.FirstOrDefault()`.



```csharp

public static Customer ObtenerClientePorId(string idCliente)

{

&#x20;   using (var \_context = new NorthwindContext())

&#x20;   {

&#x20;       // Retorna el cliente si el ID coincide, o null si no existe

&#x20;       return \_context.Customers.FirstOrDefault(c => c.CustomerID == idCliente);

&#x20;   }

}

```



\### 3.3. Update (Actualizar)

Aprovechamos el rastreo de EF Core. Modificamos el objeto y le indicamos al contexto que su estado ha cambiado a "Modificado" usando `.Update()`.



```csharp

public static void ActualizarCliente(Customer clienteModificado)

{

&#x20;   using (var \_context = new NorthwindContext())

&#x20;   {

&#x20;       \_context.Customers.Update(clienteModificado);

&#x20;       \_context.SaveChanges(); // Aquí se ejecuta el UPDATE en SQL Server

&#x20;   }

}

```



\### 3.4. Delete (Eliminar)

La lógica estricta de eliminación exige primero consultar si el registro existe en la base de datos. Si existe, se procede a removerlo.



```csharp

public static void EliminarCliente(string idCliente)

{

&#x20;   using (var \_context = new NorthwindContext())

&#x20;   {

&#x20;       // 1. Buscamos el cliente

&#x20;       var cliente = \_context.Customers.FirstOrDefault(c => c.CustomerID == idCliente);

&#x20;       

&#x20;       // 2. Validamos que exista

&#x20;       if (cliente != null)

&#x20;       {

&#x20;           \_context.Customers.Remove(cliente); // 3. Lo marcamos para eliminación

&#x20;           \_context.SaveChanges();             // 4. Se ejecuta el DELETE en SQL Server

&#x20;       }

&#x20;   }

}

```



\---



\## 4. Ventajas y Desventajas del CRUD con EF Core



| Aspecto | Evaluación Técnica |

| :--- | :--- |

| \*\*Ventajas\*\* | \*\*Prevención de Errores:\*\* Visual Studio revisa el código en tiempo de compilación. Si intentas asignar un texto a un campo numérico, el programa no compilará.<br>\*\*Mantenibilidad:\*\* Evitas tener cadenas de texto SQL gigantes (Strings mágicos) regadas por todo el código, facilitando la lectura y las pruebas. |

| \*\*Desventajas\*\* | \*\*Operaciones Masivas (Bulk):\*\* El \*Change Tracking\* es excelente para 1 o 100 registros. Pero si necesitas actualizar 100,000 clientes al mismo tiempo, el seguimiento colapsará la memoria RAM. Para esos casos, se prefiere usar sentencias SQL directas (Raw SQL) o librerías externas como \*EF Core Bulk Extensions\*. |



\---



\## 5. Referencias (Formato IEEE)



\* \[1] Microsoft, "Guardado de datos en Entity Framework Core," \*Microsoft Learn\*. \[Online]. Disponible en: \[https://learn.microsoft.com/es-es/ef/core/saving/](https://learn.microsoft.com/es-es/ef/core/saving/). \[Accedido: 15-jun-2026].

\* \[2] Microsoft, "Seguimiento de cambios en EF Core," \*Microsoft Learn\*. \[Online]. Disponible en: \[https://learn.microsoft.com/es-es/ef/core/change-tracking/](https://learn.microsoft.com/es-es/ef/core/change-tracking/). \[Accedido: 15-jun-2026].

\* \[3] Microsoft, "Clases estáticas y sus miembros (Guía de programación de C#)," \*Microsoft Learn\*. \[Online]. Disponible en: \[https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members](https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members). \[Accedido: 15-jun-2026].




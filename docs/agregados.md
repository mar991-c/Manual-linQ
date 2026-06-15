\# Capítulo 3: Funciones de Agregado y Agrupación en LINQ



\## 1. ¿Qué son y cuál es la lógica por debajo?



En el desarrollo de software empresarial, frecuentemente necesitamos obtener resúmenes estadísticos (totales, promedios, conteos). Las \*\*Funciones de Agregado\*\* de LINQ (`Count`, `Sum`, `Max`, `Min`, `Average`) nos permiten realizar estos cálculos matemáticos de forma nativa.



\*\*La Lógica Crítica de Rendimiento (¿Por qué se hace así?):\*\*

Si necesitas saber cuántos clientes compraron en Ecuador, la forma incorrecta sería traer toda la tabla a la memoria RAM de tu servidor y usar un bucle `foreach` para contarlos. La forma correcta usando LINQ to Entities es ejecutar la función directamente en la consulta.



Al utilizar `\_context.Clientes.Where(c => c.Pais == "Ecuador").Count();`, LINQ traduce el comando a un `SELECT COUNT(\*)` de SQL. El cálculo pesado lo hace el motor de la base de datos (que está diseñado y optimizado para eso) y a tu memoria RAM solo viaja un número entero (ej. `50`). Esto salva al servidor de colapsar por saturación de memoria.



\---



\## 2. Agrupación (GroupBy) y Proyecciones (Select)



Profesionalmente, las funciones de agregado rara vez se usan solas; casi siempre van acompañadas de un `GroupBy` (Agrupación) y un `Select` (Proyección hacia un DTO).



\* \*\*GroupBy:\*\* Toma una lista plana y la divide en subgrupos basados en una llave (ej. Agrupar todas las ventas \*por País\*).

\* \*\*Select (Proyección):\*\* Moldea el resultado final para que encaje perfectamente en una clase plana o DTO (Data Transfer Object), evitando enviar entidades gigantes a la Capa de Presentación.



\### Ejemplo 1: Conteo y Agrupación (Count y GroupBy)

Basado en el DTO `CantidadCategoria` de nuestro sistema Northwind, calculamos cuántas órdenes existen por cada categoría.



```csharp

public List<CantidadCategoria> ObtenerOrdenesPorCategoria()

{

&#x20;   var resultado = \_context.OrderDetails

&#x20;       .GroupBy(detalle => detalle.Product.Category.CategoryName) 

&#x20;       .Select(grupo => new CantidadCategoria 

&#x20;       {

&#x20;           NombreCategoria = grupo.Key,         // .Key representa la llave de agrupación (El nombre de la categoría)

&#x20;           NumeroOrdenes = grupo.Count()        // .Count() cuenta cuántos registros exactos hay en este grupo

&#x20;       })

&#x20;       .OrderByDescending(c => c.NumeroOrdenes) 

&#x20;       .ToList();



&#x20;   return resultado;

}

\### Ejemplo 2: Sumatoria y Cálculo Financiero (Sum)

Basado en el DTO `VentaPais`, sumamos el dinero total recaudado agrupado por la ubicación geográfica.



```csharp

public List<VentaPais> ObtenerReporteVentasPorPais()

{

&#x20;   var reporte = \_context.Orders

&#x20;       .GroupBy(orden => orden.Customer.Country)

&#x20;       .Select(grupo => new VentaPais

&#x20;       {

&#x20;           Pais = grupo.Key,

&#x20;           // Multiplicamos precio por cantidad y luego sumamos el total de todo el grupo

&#x20;           TotalVentas = grupo.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice \* od.Quantity)),

&#x20;           // Contamos los clientes únicos filtrando duplicados con Distinct

&#x20;           NumeroClientes = grupo.Select(o => o.CustomerID).Distinct().Count() 

&#x20;       })

&#x20;       .ToList();



&#x20;   return reporte;

}

```



\### Ejemplo 3: Máximos y Filtrados de Alto Nivel (OrderByDescending y FirstOrDefault)

Basado en el DTO `MejorEmpleadoTrimestre`, este es el estándar para obtener el "Top 1" de un cálculo complejo.



```csharp

public MejorEmpleadoTrimestre ObtenerMejorEmpleado()

{

&#x20;   var mejorEmpleado = \_context.Orders

&#x20;       .GroupBy(orden => orden.Employee.FirstName + " " + orden.Employee.LastName)

&#x20;       .Select(grupo => new MejorEmpleadoTrimestre

&#x20;       {

&#x20;           NombreEmpleado = grupo.Key,

&#x20;           MontoTotalOrden = grupo.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice \* od.Quantity))

&#x20;       })

&#x20;       .OrderByDescending(empleado => empleado.MontoTotalOrden) // Posiciona al que vendió más en la cima

&#x20;       .FirstOrDefault(); // Retorna estrictamente el primer elemento de la lista (El Top 1)



&#x20;   return mejorEmpleado;

}

```



\---



\## 3. Ventajas y Desventajas del Procesamiento de Agregados



| Aspecto | Evaluación Técnica |

| :--- | :--- |

| \*\*Ventajas\*\* | \*\*Eficiencia de Red:\*\* Al proyectar con `.Select()` hacia un DTO, solo viajan por la red los bytes estrictamente necesarios.<br>\*\*Legibilidad de Código:\*\* C# con LINQ es declarativo; es infinitamente más fácil leer un `.Sum()` encadenado que auditar bucles anidados. |

| \*\*Desventajas\*\* | \*\*Generación de Consultas Complejas:\*\* Agrupaciones múltiples pueden generar sentencias T-SQL ineficientes bajo el capó si la base de datos no tiene los índices correctos.<br>\*\*Excepciones de Traducción:\*\* Si se introduce un método propio de C# (ej. `.ToString()`) dentro de la expresión LINQ to Entities, el sistema fallará en tiempo de ejecución porque SQL Server no reconoce métodos de C#. |



\---



\## 4. Referencias (Formato IEEE)



\* \[1] Microsoft, "Agrupación de datos en LINQ," \*Microsoft Learn\*. \[Online]. Disponible en: \[https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results](https://learn.microsoft.com/es-es/dotnet/csharp/linq/group-query-results). \[Accedido: 15-jun-2026].

\* \[2] Microsoft, "Funciones de agregado (Transact-SQL)," \*SQL Server Documentation\*. \[Online]. Disponible en: \[https://learn.microsoft.com/es-es/sql/t-sql/functions/aggregate-functions-transact-sql](https://learn.microsoft.com/es-es/sql/t-sql/functions/aggregate-functions-transact-sql). \[Accedido: 15-jun-2026].

\* \[3] Microsoft, "Base de datos de ejemplo Northwind," \*GitHub Sql Server Samples\*. \[Online]. Disponible en: \[https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs). \[Accedido: 15-jun-2026].


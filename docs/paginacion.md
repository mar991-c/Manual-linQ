# Capítulo 6: Paginación y Rendimiento (Skip y Take)

## 1. La Lógica de la Paginación

En sistemas de nivel empresarial, cargar todos los registros de una tabla simultáneamente es una de las principales causas de caídas de servidores (Timeouts y OutOfMemoryExceptions). 

Para evitar esto, implementamos la **Paginación del Lado del Servidor (Server-side Pagination)**. En lugar de extraer 100,000 clientes, extraemos únicamente el bloque de 20 clientes que el usuario está viendo en su pantalla en este preciso momento.

* **¿Por qué se hace así?** Porque delegamos el trabajo pesado al motor de la base de datos. SQL Server es extremadamente rápido procesando índices y saltando registros, lo que mantiene nuestra Capa de Negocio ligera y veloz.

---

## 2. Implementación con Skip y Take

En LINQ, la paginación se logra combinando dos métodos: `.Skip()` (saltar) y `.Take()` (tomar). 

Es **obligatorio** usar un método de ordenamiento (como `.OrderBy()`) antes de paginar, ya que las bases de datos relacionales no garantizan un orden predeterminado a menos que se les indique explícitamente.

### La Fórmula Matemática

Para calcular cuántos registros debemos saltar dinámicamente según la página solicitada, el estándar de la industria utiliza esta fórmula:

`Saltar = (NumeroDePagina - 1) * TamañoDePagina`

### Ejemplo Práctico: Paginando Clientes

Supongamos que el gerente quiere ver la lista de Clientes, ordenados alfabéticamente, pero mostrando solo 10 por página.

```csharp
public static List<Customer> ObtenerClientesPaginados(int numeroPagina, int tamañoPagina)
{
    using (var _context = new NorthwindContext())
    {
        // Aplicamos la fórmula
        int registrosASaltar = (numeroPagina - 1) * tamañoPagina;
        var paginaClientes = _context.Customers
            .OrderBy(c => c.CompanyName)      // 1. Siempre ordenar primero
            .Skip(registrosASaltar)           // 2. Omitir los registros de las páginas anteriores
            .Take(tamañoPagina)               // 3. Tomar estrictamente la cantidad solicitada
            .ToList();                        // 4. Ejecutar consulta en SQL
        return paginaClientes;
    }
}
```

Si el usuario pide la Página 3 con un tamaño de 10: La fórmula calcula (3 - 1) * 10 = 20. LINQ le dirá a SQL Server que salte los primeros 20 registros y nos devuelva los siguientes 10.

## 3. Ventajas y Desventajas de Skip y Take

| Aspecto | Evaluación Técnica |
| :--- | :--- |
| **Ventajas** | **Rendimiento Extremo:** El tiempo de respuesta de la base de datos se vuelve constante (O(1) en términos prácticos de red) sin importar si la tabla tiene 100 o 10 millones de registros.<br>**Ahorro de Ancho de Banda:** Reduce drásticamente los megabytes de información que viajan entre la base de datos, el servidor y el navegador del cliente. |
| **Desventajas** | **Degradación en Páginas Profundas (Deep Paging):** Si el usuario intenta acceder a la página 50,000, el motor de base de datos de igual forma tiene que "leer y saltar" todos esos registros bajo el capó, lo que requiere estrategias de indexación más avanzadas (como *Keyset Pagination*). |

---

## 4. Referencias (Formato IEEE)

* [1] Microsoft, "Creación de particiones de datos (C#) - Skip y Take," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/csharp/linq/partition-data](https://learn.microsoft.com/es-es/dotnet/csharp/linq/partition-data). [Accedido: 15-jun-2026].

* [2] Microsoft, "Paginación - Entity Framework Core," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/ef/core/querying/pagination](https://learn.microsoft.com/es-es/ef/core/querying/pagination). [Accedido: 15-jun-2026].
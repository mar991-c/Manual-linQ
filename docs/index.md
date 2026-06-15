# Bienvenidos al Manual Avanzado de LINQ

Este repositorio documenta el uso de LINQ to Entities bajo una arquitectura de 4 capas
(Entidades, Datos, Negocio, Presentación).

Aquí encontrarás:

* Fundamentos de LINQ y su ejecución diferida
* Funciones de agregado (Sum, Count, Average, etc.)
* Operaciones CRUD
* Casos de estudio aplicados
* Proyectos de ejemplo completos con código real

---

## Matriz de Trazabilidad: Implementación de LINQ

A continuación se detalla exactamente en qué proyectos y archivos físicos se han implementado las diferentes técnicas de LINQ documentadas en este manual:

| Técnica LINQ Aplicada | Proyecto Académico | Capa / Archivo Exacto |
| :--- | :--- | :--- |
| **GroupBy + Count** (Agregados) | `Northwind` | `Northwind_Logica/CantidadCategoria_Logica.cs` |
| **GroupBy + Sum + Take** (Top 10) | `Northwind` | `Datos_LinQ/ClienteGastos_Datos.cs` |
| **Join** (Sintaxis de Consulta) | `Northwind` | `Northwind_Logica` (Método de Catálogo) |
| **CRUD con Entity Framework** (`.Include`) | `GestionUsuarios` | `GestionUsuariosDatosEF/PacienteDatos.cs` |
| **CRUD con LINQ to SQL** | `GestionUsuarios` | `GestionUsuarios_DatosLinq/PacienteDatos.cs` |
| **Paginación** (`Skip` y `Take`) | `Northwind` | `Northwind_Logica` (Paginación de Clientes) |
| **Where + OrderBy + Select** | `BibliotecaApp` | `Biblioteca.Datos/LibroDatos.cs` |
| **GroupBy + Sum + Average + Min** | `InventarioApp` | `Inventario.Datos/ProductoDatos.cs` |
| **Reglas de negocio con LINQ to Objects** | `InventarioApp` | `Inventario.Negocio/ProductoNegocio.cs` |

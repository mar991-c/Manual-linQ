# Bienvenidos al Manual Avanzado de LINQ

Este repositorio documenta el uso de LINQ to Entities bajo una arquitectura de 4 capas
(Entidades, Datos, Negocio, Presentación).

Aquí encontrarás:

* Fundamentos de LINQ y su ejecución diferida
* Funciones de agregado (Sum, Count, Average, etc.)
* Operaciones CRUD
* Casos de estudio aplicados

---

## Matriz de Trazabilidad: Implementación de LINQ

A continuación se detalla en qué proyectos y archivos se han implementado las técnicas documentadas:

| Técnica LINQ Aplicada | Proyecto | Capa / Archivo |
| :--- | :--- | :--- |
| **GroupBy + Count** | `Northwind` | `Northwind_Logica/CantidadCategoria_Logica.cs` |
| **GroupBy + Sum + Take** | `Northwind` | `Datos_LinQ/ClienteGastos_Datos.cs` |
| **Join** (Sintaxis de Consulta) | `Northwind` | `Northwind_Logica` |
| **CRUD con Entity Framework** | `GestionUsuarios` | `GestionUsuariosDatosEF/PacienteDatos.cs` |
| **CRUD con LINQ to SQL** | `GestionUsuarios` | `GestionUsuarios_DatosLinq/PacienteDatos.cs` |
| **Paginación** (Skip y Take) | `Northwind` | `Northwind_Logica` |
| **Where + OrderBy + Select** | `BibliotecaApp` | `Biblioteca.Datos/LibroDatos.cs` |
| **GroupBy + Sum + Average + Min** | `InventarioApp` | `Inventario.Datos/ProductoDatos.cs` |
| **LINQ to Objects + Reglas de negocio** | `InventarioApp` | `Inventario.Negocio/ProductoNegocio.cs` |

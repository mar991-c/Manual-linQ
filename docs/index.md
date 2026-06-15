\# Bienvenidos al Manual Avanzado de LINQ



Este repositorio documenta el uso de LINQ to Entities bajo una arquitectura de 4 capas

(Entidades, Datos, Negocio, Presentación).



Aquí encontrarás:

\* Fundamentos de LINQ y su ejecución diferida

\* Funciones de agregado (Sum, Count, Average, etc.)

\* Operaciones CRUD

\* Casos de estudio aplicados



\## Matriz de Trazabilidad: Implementación de LINQ



A continuación, se detalla exactamente en qué proyectos y archivos físicos se han implementado las diferentes técnicas de LINQ requeridas para este manual:



| Técnica LINQ Aplicada | Proyecto Académico | Capa / Archivo Exacto |

| :--- | :--- | :--- |

| \*\*GroupBy + Count\*\* (Agregados) | `Northwind` | `Northwind\_Logica/CantidadCategoria\_Logica.cs` |

| \*\*GroupBy + Sum + Take\*\* (Top 10) | `Northwind` | `Datos\_LinQ/ClienteGastos\_Datos.cs` |

| \*\*Join\*\* (Sintaxis de Consulta) | `Northwind` | `Northwind\_Logica` (Método de Catálogo) |

| \*\*CRUD con Entity Framework\*\* (`.Include`) | `GestionUsuarios` | `GestionUsuariosDatosEF/PacienteDatos.cs` |

| \*\*CRUD con LINQ to SQL\*\* | `GestionUsuarios` | `GestionUsuarios\_DatosLinq/PacienteDatos.cs` |

| \*\*Paginación\*\* (`Skip` y `Take`) | `Northwind` | `Northwind\_Logica` (Paginación de Clientes) |


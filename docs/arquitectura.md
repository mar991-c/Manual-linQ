# Capítulo 2: Arquitectura de 4 Capas

## 1. ¿Qué es el modelo de 4 Capas y por qué se utiliza?

En el desarrollo de software profesional, no se escribe todo el código (diseño visual, validaciones y conexiones a base de datos) en un solo lugar. El modelo de **4 Capas (Arquitectura N-Tier)** es un patrón de diseño que divide el sistema en proyectos físicos independientes. 

**La Lógica y el Porqué:**

El objetivo principal es el **Desacoplamiento** y la **Separación de Responsabilidades (Separation of Concerns)**. 

* ¿Por qué se hace de esta manera y no de otra? Si un desarrollador mezcla el código SQL en el botón de un formulario web, y mañana la empresa decide lanzar una aplicación móvil, ese código no sirve porque está "atrapado" en la web. 

* Al separar el sistema en capas, la lógica y los datos se escriben una sola vez y pueden ser consumidos por cualquier interfaz (Web, Móvil, Consola).

---

## 2. Responsabilidades de Cada Capa

Cada proyecto tiene un rol estrictamente definido y limitaciones claras sobre con quién puede comunicarse.

### 1. Capa de Entidades (Transversal)

Es el núcleo de los datos. Esta capa es una biblioteca de clases simple: no tiene lógica, no tiene conexiones, solo tiene la estructura.

* **Responsable de:** Modelos, Clases (ej. `Cliente.cs`, `Factura.cs`) y Objetos de transferencia (DTOs).

* **Interacción con LINQ:** Ninguna. Solo provee los moldes que LINQ va a llenar con datos.

### 2. Capa de Datos (DAL - Data Access Layer)

Es la única capa que tiene permitido saber qué es una base de datos, qué es SQL y cuál es la cadena de conexión.

* **Responsable de:** Consultas directas a la base de datos, Operaciones CRUD, mapeo relacional mediante Entity Framework / LINQ to SQL.

* **Interacción con LINQ:** Aquí vive **LINQ to Entities / LINQ to SQL**. Se encarga de traducir las consultas C# a comandos SQL optimizados. Retorna los datos crudos a la capa superior.

### 3. Capa de Negocio (BLL - Business Logic Layer)

Es el "cerebro" de la aplicación.

* **Responsable de:** Reglas de negocio (ej. "No permitir el registro si es menor de edad"), validaciones complejas y procesamiento matemático de información.

* **Interacción con LINQ:** Aquí vive **LINQ to Objects**. Recibe listas de la Capa de Datos y usa funciones de agregado (`Sum`, `Count`) y agrupaciones (`GroupBy`) en memoria para preparar reportes antes de mandarlos a la pantalla.

### 4. Capa de Presentación (UI / API)

Es la "cara" del sistema. Es la única capa que el usuario final ve y toca.

* **Responsable de:** Formularios, Interfaces gráficas, Menús, Interacción directa con el usuario (clics, inputs).

* **Interacción con LINQ:** **Estrictamente nula.** Esta capa no hace consultas, solo pide información a la Capa de Negocio y la dibuja en pantalla.

---

## 3. Flujo de Comunicación y Dependencias

Profesionalmente, las capas no pueden hablar entre sí de forma caótica. Existe una regla de oro inquebrantable de unidireccionalidad para evitar dependencias circulares:

```mermaid
graph TD
    A[Capa de Presentación] -->|Solo conoce a| B[Capa de Negocio]
    B -->|Solo conoce a| C[Capa de Datos]
    D((Capa de Entidades))

    A -.->|Usa Modelos de| D
    B -.->|Usa Modelos de| D
    C -.->|Llena Modelos de| D

    style A fill:#e1f5fe,stroke:#0288d1,stroke-width:2px,color:#000000
    style B fill:#fff3e0,stroke:#f57c00,stroke-width:2px,color:#000000
    style C fill:#e8f5e9,stroke:#388e3c,stroke-width:2px,color:#000000
    style D fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,stroke-dasharray: 5 5,color:#000000

## 4. Ventajas y Desventajas del Modelo

| Aspecto | Evaluación Técnica |
| :--- | :--- |
| **Ventajas** | **Mantenibilidad:** Si se cambia la base de datos, solo se reescribe la Capa de Datos.<br>**Seguridad:** La interfaz no tiene acceso al motor de la base de datos, impidiendo borrados accidentales.<br>**Trabajo en Equipo:** Permite desarrollo simultáneo en diferentes capas sin generar conflictos. |
| **Desventajas** | **Sobrecarga Inicial:** Requiere crear más archivos, carpetas e interfaces al inicio del proyecto.<br>**Redundancia:** A menudo requiere mapear los objetos de la base de datos a objetos de presentación (DTOs). |

---

## 5. Referencias (Formato IEEE)

* [1] Microsoft, "Estilo de arquitectura de n niveles," *Azure Architecture Center*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/azure/architecture/guide/architecture-styles/n-tier](https://learn.microsoft.com/es-es/azure/architecture/guide/architecture-styles/n-tier). [Accedido: 15-jun-2026].

* [2] Microsoft, "Diseño de la capa de persistencia de la infraestructura," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design](https://learn.microsoft.com/es-es/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design). [Accedido: 15-jun-2026].
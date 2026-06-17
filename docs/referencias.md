# Formato de Referencias y Trazabilidad

Para garantizar el rigor académico y la trazabilidad técnica de este manual, se ha establecido un sistema de doble referenciación. Este modelo permite identificar claramente qué código pertenece a los desarrollos internos de la asignatura y qué conceptos teóricos provienen de la documentación oficial.

---

## 1. Referenciación de Código Fuente Interno

Dado que el objetivo de este manual es documentar la implementación de LINQ orientada al manejo de capas, **todo el código mostrado en los ejemplos no es genérico**, sino que ha sido extraído directamente de los proyectos desarrollados en clase.

Para referenciar el código fuente, se utiliza un bloque de cita (`>`) justo antes del fragmento de código, siguiendo esta estructura estándar:

> **Referencia:** Proyecto `[Nombre del Proyecto]`, archivo `[Nombre de la Carpeta o Capa]/[Nombre del Archivo.cs]`.

**Ejemplo de aplicación:**

> **Referencia:** Proyecto `GestionUsuarios`, archivo `GestionUsuariosDatosEF/PacienteDatos.cs`.

Esta trazabilidad demuestra exactamente en qué capa (Entidades, Datos, Negocio o Presentación) se está ejecutando la instrucción LINQ.

---

## 2. Referenciación Bibliográfica Externa (Formato IEEE)

Para la teoría, los conceptos arquitectónicos y la definición de las funciones de LINQ, se ha consultado la documentación técnica oficial (principalmente *Microsoft Learn*).

Estas fuentes se listan al final de cada capítulo correspondiente utilizando el estándar internacional **IEEE (Institute of Electrical and Electronics Engineers)** para documentos web.

**Estructura del estándar aplicado:**

* [Número] Autor, "Título del artículo o sección," *Nombre del Sitio Web*. [Online]. Disponible en: [URL]. [Accedido: Fecha-de-acceso].

**Ejemplo de aplicación:**

* [1] Microsoft, "LINQ to SQL," *Microsoft Learn*. [Online]. Disponible en: [https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/](https://learn.microsoft.com/es-es/dotnet/framework/data/adonet/sql/linq/). [Accedido: 15-jun-2026].

## 3. Código Fuente Completo

El código completo de los proyectos documentados en este manual está disponible en la carpeta `src/` de este mismo repositorio:

[**Ver código fuente completo en GitHub**](https://github.com/mar991-c/Manual-linQ/tree/main/src)

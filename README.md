# Sistema ETL - Sistema de Análisis de Opiniones de Clientes

## Descripción General
El sistema ETL (Extract, Transform, Load) está diseñado para procesar y cargar datos de diferentes fuentes en la base de datos **MercaJumbo**.  
El programa procesa archivos CSV de diversas entidades de negocio, aplica reglas de limpieza y validación, y persiste los datos en la base de datos para su análisis posterior.

---

## Arquitectura del Sistema
La aplicación sigue una **arquitectura por capas**:

- **Presentation (Program.cs)**: Punto de entrada de la aplicación.
- **Business (EtlService.cs)**: Contiene la lógica de negocio y el procesamiento ETL.
- **Data**: Repositorios para el acceso a datos en la base.
- **Entities**: Modelos de datos que representan las tablas de la base.

### Componente Principal: `EtlService`
Responsable de todo el flujo ETL: extracción, transformación y carga de los datos.

---

## Responsabilidades del Sistema

### Extracción
- Lectura de archivos CSV mediante la librería **CsvHelper**.
- Carga de datos en memoria para su posterior procesamiento.

### Transformación
- Limpieza y validación de los datos según reglas de negocio.
- Normalización de campos (eliminación de espacios, formatos consistentes).

### Carga
- Persistencia de los datos limpios en la base de datos **MercaJumbo**.

---

## Entidades Procesadas
1. **Clientes**: Información de usuarios registrados.  
2. **Productos**: Catálogo de productos con precios y categorías.  
3. **Fuentes de Datos**: URLs y tipos de fuentes de información.  
4. **Encuestas**: Respuestas de clientes a preguntas.  
5. **Comentarios Sociales**: Comentarios en redes sociales con reacciones.  
6. **Reseñas Web**: Reviews de productos con calificaciones (1-5).  
7. **Ventas**: Transacciones de compra.  
8. **Opiniones**: Calificaciones y comentarios de productos.  

---

## Reglas de Limpieza de Datos

### Validaciones Comunes
- Eliminación de espacios en blanco.
- Verificación de campos obligatorios.
- Detección y eliminación de registros duplicados.

### Validaciones Específicas
- **Clientes**: Email único, fecha de registro válida.  
- **Productos**: Precio mayor a 0, nombre único.  
- **Calificaciones**: Rango válido de 1 a 5 estrellas.  
- **Ventas**: Cantidad y monto mayor a 0.

---

## Uso
1. Colocar los archivos CSV en la carpeta de entrada.
2. Ejecutar la aplicación (`Program.cs`) para iniciar el proceso ETL.
3. Verificar en la base de datos **MercaJumbo** que los datos se han cargado correctamente.

---

## Dependencias
- .NET 8
- CsvHelper
- Microsoft.Data.SqlClient

---

## Autor
Desarrollado por el equipo de **MercaJumbo ETL**.

### Documento README

```markdown
# Proyecto de Facturación

## Descripción

Este proyecto es una aplicación de facturación de productos que permite gestionar clientes, productos y facturas. La aplicación está desarrollada en .NET 8 utilizando Entity Framework Core para el acceso a datos y SQL Server como base de datos relacional.

## Funcionalidades

- CRUD de Personas
- CRUD de Productos
- CRUD de Facturas
- Vistas SQL para obtener:
  - Total facturado por cada persona
  - Persona que compró el producto más caro
  - Productos ordenados por cantidad facturada
  - Productos ordenados por utilidad generada
  - Margen de ganancia de cada producto

## Requisitos

- .NET 8 SDK
- SQL Server
- Docker (opcional, para despliegue con contenedores)

## Instalación

1. Clonar el repositorio:

   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd <NOMBRE_DEL_PROYECTO>
   ```

2. Configurar la cadena de conexión en `appsettings.json`.

3. Restaurar los paquetes NuGet:

   ```bash
   dotnet restore
   ```

4. Aplicar las migraciones:

   ```bash
   dotnet ef database update
   ```

5. Ejecutar la aplicación:

   ```bash
   dotnet run
   ```

## Dockerización

1. Crear la imagen Docker:

   ```bash
   docker build -t nombre-del-proyecto .
   ```

2. Ejecutar el contenedor:

   ```bash
   docker run -d -p 8080:80 nombre-del-proyecto
   ```

## Documentación de API

La documentación de la API se puede acceder a través de Swagger en la ruta `/swagger` cuando la aplicación está en ejecución.

## Ventajas y Desventajas

### Ventajas

- Uso de Entity Framework Core para facilitar el acceso y manejo de datos.
- Arquitectura limpia y modular basada en principios SOLID.
- Dockerización para facilitar el despliegue y la portabilidad.
- Uso de vistas SQL para optimizar las consultas y cálculos.

### Desventajas

- Dependencia de SQL Server, lo que puede limitar la portabilidad si se desea utilizar otro sistema de gestión de bases de datos.
- Complejidad adicional al manejar vistas SQL y sincronización con Entity Framework.

## Motivos de Selección de Herramientas

- **.NET 8**: Framework moderno y robusto con gran soporte para desarrollo web y de APIs.
- **Entity Framework Core**: Proporciona una forma intuitiva y poderosa de interactuar con la base de datos.
- **SQL Server**: Sistema de gestión de bases de datos relacional confiable y con buenas capacidades de integración.
- **Docker**: Facilita la creación, despliegue y ejecución de aplicaciones en contenedores, garantizando consistencia en diferentes entornos.

## Contribución

Si deseas contribuir a este proyecto, por favor crea un fork del repositorio y abre un pull request con tus cambios.

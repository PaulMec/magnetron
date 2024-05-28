### README.md

```markdown
# Magnetron

Magnetron es una aplicación de facturación diseñada para gestionar productos, personas, y facturas. Esta solución se implementa utilizando una arquitectura de capas (Onion Architecture) que separa las responsabilidades en diferentes capas, asegurando un código más limpio y mantenible.

## Tabla de Contenidos
- [Instalación](#instalación)
- [Uso](#uso)
- [API](#api)
- [Pruebas](#pruebas)
- [Dockerización](#dockerización)
- [Decisiones de Diseño](#decisiones-de-diseño)
- [Herramientas Seleccionadas](#herramientas-seleccionadas)
- [Contribución](#contribución)

## Instalación

### Requisitos Previos
- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/products/docker-desktop) (Opcional, para despliegue con contenedores)

### Configuración
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PaulMec/magnetron.git
   cd magnetron
   ```

2. Configura la base de datos:
   - Actualiza el archivo `appsettings.json` con tu cadena de conexión a la base de datos.

3. Ejecuta las migraciones para crear la base de datos:
   ```bash
   dotnet ef database update
   ```

4. Compila y ejecuta el proyecto:
   ```bash
   dotnet build
   dotnet run
   ```

## Uso

Una vez que la aplicación está en ejecución, puedes acceder a la API mediante `http://localhost:5000/swagger` para ver la documentación interactiva de los endpoints disponibles.

## API

### Endpoints Principales
- **Personas**
  - `GET /api/persons` - Obtener todas las personas
  - `GET /api/persons/{id}` - Obtener una persona por ID
  - `POST /api/persons` - Crear una nueva persona
  - `PUT /api/persons/{id}` - Actualizar una persona
  - `DELETE /api/persons/{id}` - Eliminar una persona

- **Productos**
  - `GET /api/products` - Obtener todos los productos
  - `GET /api/products/{id}` - Obtener un producto por ID
  - `POST /api/products` - Crear un nuevo producto
  - `PUT /api/products/{id}` - Actualizar un producto
  - `DELETE /api/products/{id}` - Eliminar un producto

- **Facturas**
  - `GET /api/invoices` - Obtener todas las facturas
  - `GET /api/invoices/{id}` - Obtener una factura por ID
  - `POST /api/invoices` - Crear una nueva factura
  - `PUT /api/invoices/{id}` - Actualizar una factura
  - `DELETE /api/invoices/{id}` - Eliminar una factura

Para una documentación completa de la API, visita `http://localhost:5000/swagger` una vez que la aplicación esté en ejecución.

## Pruebas

### Ejecución de Pruebas Unitarias
El proyecto incluye pruebas unitarias para asegurar la calidad del código. Para ejecutar las pruebas, utiliza el siguiente comando:
```bash
dotnet test
```

### Cobertura de Pruebas
La cobertura de pruebas se genera utilizando [coverlet](https://github.com/coverlet-coverage/coverlet). Para generar un informe de cobertura, usa el siguiente comando:
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Dockerización

### Configuración de Docker
El proyecto incluye un archivo `Dockerfile` y un `docker-compose.yml` para facilitar la configuración y el despliegue en contenedores.

### Construcción y Ejecución
Para construir y ejecutar el contenedor Docker, utiliza los siguientes comandos:
```bash
docker-compose build
docker-compose up
```

Esto levantará los servicios definidos en `docker-compose.yml`, incluyendo la aplicación y la base de datos.

## Decisiones de Diseño

### Arquitectura
Se ha adoptado una arquitectura de capas (Onion Architecture) para separar claramente las responsabilidades y asegurar la mantenibilidad del código. Las capas incluyen:
- **Capa de Presentación:** Controladores que manejan las solicitudes HTTP.
- **Capa de Aplicación:** Servicios que contienen la lógica de negocio.
- **Capa de Dominio:** Modelos de dominio y lógica empresarial.
- **Capa de Infraestructura:** Implementaciones de acceso a datos y repositorios.

### Patrones de Diseño
- **Patrón Repositorio:** Se utiliza para abstraer el acceso a los datos y asegurar que el código de acceso a datos esté aislado.
- **Inyección de Dependencias:** Se utiliza para facilitar la inyección de servicios y repositorios en los controladores y servicios de aplicación.

### Validaciones y Manejo de Excepciones
Se implementan validaciones en los modelos y se maneja adecuadamente las excepciones para asegurar que la aplicación sea robusta y fácil de depurar.

## Ventajas y Desventajas
### Ventajas
Mantenibilidad: La arquitectura en capas permite un código más limpio y organizado, facilitando el mantenimiento y la extensión de la aplicación.
Pruebas Unitarias: La separación de responsabilidades facilita la creación de pruebas unitarias efectivas, asegurando la calidad del código.
Portabilidad: El uso de Docker permite desplegar la aplicación en diferentes entornos sin problemas de configuración.

### Desventajas
Complejidad Inicial: La implementación de una arquitectura en capas puede aumentar la complejidad inicial del proyecto.
Curva de Aprendizaje: La necesidad de comprender múltiples capas y patrones de diseño puede ser un desafío para desarrolladores menos experimentados.

## Herramientas Seleccionadas

### .NET
Elegido por su alto rendimiento, soporte a largo plazo y robustez en el desarrollo de aplicaciones web y APIs.

### Entity Framework Core
Utilizado para el acceso a datos debido a su facilidad de uso, integración con .NET y capacidades avanzadas de mapeo objeto-relacional.

### xUnit y Moq
- **xUnit:** Framework de pruebas unitarias utilizado por su simplicidad y funcionalidad.
- **Moq:** Biblioteca de mocking utilizada para aislar las dependencias en las pruebas unitarias.

### Docker
Utilizado para la creación de contenedores que facilitan el despliegue y la portabilidad de la aplicación.

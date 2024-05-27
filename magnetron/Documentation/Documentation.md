### Documentación de Uso e Instalación del Software

#### Requisitos Previos

1. **.NET 8 SDK**: Asegúrate de tener instalado el SDK de .NET 8. Puedes descargarlo desde [aquí](https://dotnet.microsoft.com/download/dotnet/8.0).
2. **SQL Server**: Necesitas tener acceso a una instancia de SQL Server.
3. **Docker**: Para la dockerización, asegúrate de tener Docker instalado. Puedes descargarlo desde [aquí](https://www.docker.com/products/docker-desktop).

#### Configuración de la Base de Datos

1. **Crear la Base de Datos**: Usa el siguiente script para crear la base de datos y las tablas necesarias.

   ```sql
   -- Usar la base de datos existente
   USE master;
   GO

   -- Crear la base de datos
   CREATE DATABASE DBMagnetron;
   GO

   -- Usar la base de datos recién creada
   USE DBMagnetron;
   GO

   -- Crear tablas
   CREATE TABLE Person (
       PersonId INT PRIMARY KEY IDENTITY,
       FirstName NVARCHAR(50) NOT NULL,
       LastName NVARCHAR(50) NOT NULL,
       DocumentType NVARCHAR(20) NOT NULL,
       DocumentNumber NVARCHAR(20) NOT NULL
   );
   GO

   CREATE TABLE Product (
       ProductId INT PRIMARY KEY IDENTITY,
       Description NVARCHAR(100) NOT NULL,
       Price DECIMAL(18, 2) NOT NULL,
       Cost DECIMAL(18, 2) NOT NULL,
       UnitOfMeasure NVARCHAR(20) NOT NULL
   );
   GO

   CREATE TABLE InvoiceHeader (
       InvoiceHeaderId INT PRIMARY KEY IDENTITY,
       InvoiceNumber INT NOT NULL,
       InvoiceDate DATETIME NOT NULL,
       PersonId INT NOT NULL,
       FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
   );
   GO

   CREATE TABLE InvoiceDetail (
       InvoiceDetailId INT PRIMARY KEY IDENTITY,
       LineNumber INT NOT NULL,
       Quantity INT NOT NULL,
       ProductId INT NOT NULL,
       InvoiceHeaderId INT NOT NULL,
       FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
       FOREIGN KEY (InvoiceHeaderId) REFERENCES InvoiceHeader(InvoiceHeaderId)
   );
   GO

   -- Crear vistas
   IF OBJECT_ID('TotalFacturadoPorPersona', 'V') IS NOT NULL
       DROP VIEW TotalFacturadoPorPersona;
   GO

   IF OBJECT_ID('PersonaProductoMasCaro', 'V') IS NOT NULL
       DROP VIEW PersonaProductoMasCaro;
   GO

   IF OBJECT_ID('ProductosPorCantidadFacturada', 'V') IS NOT NULL
       DROP VIEW ProductosPorCantidadFacturada;
   GO

   IF OBJECT_ID('ProductosPorUtilidad', 'V') IS NOT NULL
       DROP VIEW ProductosPorUtilidad;
   GO

   IF OBJECT_ID('ProductosMargenGanancia', 'V') IS NOT NULL
       DROP VIEW ProductosMargenGanancia;
   GO

   -- Vista para listar cada persona con el total facturado
   CREATE VIEW TotalFacturadoPorPersona AS
   SELECT 
       P.PersonId,
       P.FirstName,
       P.LastName,
       ISNULL(SUM(D.Quantity * Pr.Price), 0) AS TotalFacturado
   FROM 
       Person P
   LEFT JOIN 
       InvoiceHeader H ON P.PersonId = H.PersonId
   LEFT JOIN 
       InvoiceDetail D ON H.InvoiceHeaderId = D.InvoiceHeaderId
   LEFT JOIN 
       Product Pr ON D.ProductId = Pr.ProductId
   GROUP BY 
       P.PersonId, P.FirstName, P.LastName;
   GO

   -- Vista para listar la persona que haya comprado el producto más caro
   CREATE VIEW PersonaProductoMasCaro AS
   SELECT TOP 1 
       P.PersonId,
       P.FirstName,
       P.LastName,
       Pr.Description,
       Pr.Price
   FROM 
       Person P
   JOIN 
       InvoiceHeader H ON P.PersonId = H.PersonId
   JOIN 
       InvoiceDetail D ON H.InvoiceHeaderId = D.InvoiceHeaderId
   JOIN 
       Product Pr ON D.ProductId = Pr.ProductId
   ORDER BY 
       Pr.Price DESC;
   GO

   -- Vista para listar productos según su cantidad facturada
   CREATE VIEW ProductosPorCantidadFacturada AS
   SELECT 
       Pr.ProductId,
       Pr.Description,
       SUM(D.Quantity) AS QuantitySold
   FROM 
       Product Pr
   JOIN 
       InvoiceDetail D ON Pr.ProductId = D.ProductId
   GROUP BY 
       Pr.ProductId, Pr.Description;
   GO

   -- Vista para listar productos según su utilidad generada por facturación
   CREATE VIEW ProductosPorUtilidad AS
   SELECT 
       Pr.ProductId,
       Pr.Description,
       SUM(D.Quantity * (Pr.Price - Pr.Cost)) AS Profit
   FROM 
       Product Pr
   JOIN 
       InvoiceDetail D ON Pr.ProductId = D.ProductId
   GROUP BY 
       Pr.ProductId, Pr.Description;
   GO

   -- Vista para listar productos y el margen de ganancia de cada uno según su facturación
   CREATE VIEW ProductosMargenGanancia AS
   SELECT 
       Pr.ProductId,
       Pr.Description,
       (Pr.Price - Pr.Cost) AS ProfitMargin
   FROM 
       Product Pr;
   GO
   ```

2. **Insertar Datos de Prueba**: Usa el siguiente script para insertar datos de prueba.

   ```sql
   -- Insertar registros en la tabla Person
   INSERT INTO Person (FirstName, LastName, DocumentType, DocumentNumber) VALUES 
   ('John', 'Doe', 'DNI', '12345678'),
   ('Jane', 'Smith', 'DNI', '87654321'),
   ('Alice', 'Johnson', 'Passport', 'A1234567'),
   ('Bob', 'Brown', 'Passport', 'B7654321'),
   ('Charlie', 'Black', 'DNI', '13579246'),
   ('Dave', 'White', 'DNI', '24681357'),
   ('Eve', 'Green', 'Passport', 'C9876543'),
   ('Frank', 'Blue', 'Passport', 'D8765432'),
   ('Grace', 'Red', 'DNI', '31415926'),
   ('Hank', 'Yellow', 'DNI', '27182818');
   GO

   -- Insertar registros en la tabla Product
   INSERT INTO Product (Description, Price, Cost, UnitOfMeasure) VALUES 
   ('Laptop', 1000.00, 800.00, 'Unit'),
   ('Smartphone', 600.00, 400.00, 'Unit'),
   ('Tablet', 400.00, 250.00, 'Unit'),
   ('Monitor', 200.00, 150.00, 'Unit'),
   ('Keyboard', 50.00, 30.00, 'Unit'),
   ('Mouse', 25.00, 10.00, 'Unit'),
   ('Printer', 150.00, 100.00, 'Unit'),
   ('Scanner', 120.00, 80.00, 'Unit'),
   ('Webcam', 80.00, 50.00, 'Unit'),
   ('Headphones', 75.00, 40.00, 'Unit');
   GO

   -- Insertar registros en la tabla InvoiceHeader
   INSERT INTO InvoiceHeader (InvoiceNumber, InvoiceDate, PersonId) VALUES 
   (1001, '2024-05-01', 1),
   (1002, '2024-05-02', 2),
   (1003, '2024-05-03', 3),
   (1004, '2024-05-04', 4),
   (1005, '2024-05-05', 5),
   (1006, '2024-05-06', 6),
   (1007, '2024-05-07', 7),
   (1008, '2024-05-08', 8),
   (1009, '2024-05-09', 9),
   (1010, '2024-05-10', 10);
   GO

   -- Insertar registros en la tabla InvoiceDetail
   INSERT INTO InvoiceDetail (LineNumber, Quantity, ProductId, InvoiceHeaderId) VALUES 
   (1, 2, 1, 1),
   (2, 1, 2, 1),
   (1, 3, 3, 2),
   (1, 1, 4, 3),
   (2, 2, 2, 3),
   (1, 1, 5, 4),
   (2, 2, 6, 4),
   (1, 1, 7, 5),
   (2, 2, 8, 5),
   (1, 1, 9, 6),
   (2, 2, 10, 6),
   (1, 3, 1, 7),
   (2, 2, 3, 7),
   (1, 1, 4, 8),
   (2, 3, 5, 8),
   (1, 2, 6, 9),
   (2, 1, 7, 9),
   (1, 3, 8, 10),
   (2, 2, 9, 10);
   GO
   ```

#### Configuración del Proyecto

1. **Clonar el Repositorio**: Clona el repositorio desde GitHub o GitLab.

   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd <NOMBRE_DEL_PROYECTO>
   ```

2. **Configurar la Cadena de Conexión**: Asegúrate de que el archivo `appsettings.json` tenga la cadena de conexión correcta a tu base de datos SQL Server.

   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DBMagnetron;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
     }
   }
   ```

3. **Restaurar Paquetes NuGet**: Restaura los paquetes necesarios.

   ```bash
   dotnet restore
   ```

4. **Aplicar Migraciones**: Aplica las migraciones para crear la base de datos y las tablas.

   ```bash
   dotnet ef database update
   ```

5. **Ejecutar la Aplicación**: Ejecuta la aplicación.

   ```bash
   dotnet run
   ```

#### Dockerización

1. **Crear Dockerfile**: Crea un archivo `Dockerfile` en la raíz del proyecto.

   ```dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /src
   COPY ["<NOMBRE_DEL_PROYECTO>.csproj", "./"]
   RUN dotnet restore "<NOMBRE_DEL_PROYECTO>.csproj"
   COPY . .
   WORKDIR "/src/."
   RUN dotnet build "<NOMBRE_DEL_PROYECTO>.csproj" -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish "<NOMBRE_DEL_PROYECTO>.csproj" -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "<NOMBRE_DEL_PROYECTO>.dll"]
   ```

2. **Construir y Ejecutar la Imagen Docker**: Construye y ejecuta la imagen Docker.

   ```bash
   docker build -t nombre-del-proyecto .
   docker run -d -p 8080:80 nombre-del-proyecto
   ```
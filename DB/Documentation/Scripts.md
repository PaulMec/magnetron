### Creación de Script SQL para Generar la Base de Datos, Tablas, Vistas y Registros

### Conceptos Básicos

**Tablas**: Son estructuras en una base de datos que almacenan datos en filas y columnas. Cada tabla se define con un esquema que especifica las columnas y sus tipos de datos.

**Vistas**: Son consultas guardadas en la base de datos que proporcionan una forma de ver los datos de una o más tablas sin almacenarlos físicamente. Las vistas pueden simplificar la complejidad de las consultas y proporcionar una capa de seguridad al ocultar partes de los datos.

**Registros**: Son filas en una tabla de base de datos. Cada registro contiene datos correspondientes a las columnas de la tabla.

### Script SQL para Crear la Base de Datos y Tablas

```sql
-- Crear la base de datos
CREATE DATABASE DBMagnetron;
GO

-- Usar la base de datos recién creada
USE DBMagnetron;
GO

-- Tabla Persona
CREATE TABLE Person (
    PersonId INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DocumentType NVARCHAR(20) NOT NULL,
    DocumentNumber NVARCHAR(20) NOT NULL
);
GO

-- Tabla Producto
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Cost DECIMAL(18, 2) NOT NULL,
    UnitOfMeasure NVARCHAR(20) NOT NULL
);
GO

-- Tabla Fact_Encabezado
CREATE TABLE InvoiceHeader (
    InvoiceHeaderId INT PRIMARY KEY IDENTITY,
    InvoiceNumber INT NOT NULL,
    InvoiceDate DATETIME NOT NULL,
    PersonId INT NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);
GO

-- Tabla Fact_Detalle
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
```

### Script SQL para Crear Vistas

```sql
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

-- Vista para listar productos según su cantidad facturada en orden descendente
CREATE VIEW ProductosPorCantidadFacturada AS
SELECT 
    Pr.ProductId,
    Pr.Description,
    SUM(D.Quantity) AS CantidadFacturada
FROM 
    Product Pr
JOIN 
    InvoiceDetail D ON Pr.ProductId = D.ProductId
GROUP BY 
    Pr.ProductId, Pr.Description
ORDER BY 
    CantidadFacturada DESC;
GO

-- Vista para listar productos según su utilidad generada por facturación
CREATE VIEW ProductosPorUtilidad AS
SELECT 
    Pr.ProductId,
    Pr.Description,
    SUM(D.Quantity * (Pr.Price - Pr.Cost)) AS Utilidad
FROM 
    Product Pr
JOIN 
    InvoiceDetail D ON Pr.ProductId = D.ProductId
GROUP BY 
    Pr.ProductId, Pr.Description
ORDER BY 
    Utilidad DESC;
GO

-- Vista para listar productos y el margen de ganancia de cada uno según su facturación
CREATE VIEW ProductosMargenGanancia AS
SELECT 
    Pr.ProductId,
    Pr.Description,
    (Pr.Price - Pr.Cost) AS MargenGanancia
FROM 
    Product Pr;
GO
```

### Script SQL para Insertar Registros de Prueba

```sql
-- Insertar registros en la tabla Person
INSERT INTO Person (FirstName, LastName, DocumentType, DocumentNumber) VALUES 
('John', 'Doe', 'DNI', '12345678'),
('Jane', 'Smith', 'DNI', '87654321'),
('Alice', 'Johnson', 'Passport', 'A1234567'),
('Bob', 'Brown', 'Passport', 'B7654321');
GO

-- Insertar registros en la tabla Product
INSERT INTO Product (Description, Price, Cost, UnitOfMeasure) VALUES 
('Laptop', 1000.00, 800.00, 'Unit'),
('Smartphone', 600.00, 400.00, 'Unit'),
('Tablet', 400.00, 250.00, 'Unit'),
('Monitor', 200.00, 150.00, 'Unit');
GO

-- Insertar registros en la tabla InvoiceHeader
INSERT INTO InvoiceHeader (InvoiceNumber, InvoiceDate, PersonId) VALUES 
(1, '2023-01-01', 1),
(2, '2023-01-02', 2),
(3, '2023-01-03', 1),
(4, '2023-01-04', 3);
GO

-- Insertar registros en la tabla InvoiceDetail
INSERT INTO InvoiceDetail (LineNumber, Quantity, ProductId, InvoiceHeaderId) VALUES 
(1, 2, 1, 1),
(2, 1, 2, 1),
(1, 3, 3, 2),
(1, 1, 4, 3),
(2, 2, 2, 3),
(1, 5, 1, 4);
GO
```

### Explicación

- **Tablas**: Las tablas `Person`, `Product`, `InvoiceHeader` y `InvoiceDetail` se crean para almacenar los datos de personas, productos y facturas.
- **Vistas**: Se crean varias vistas (`TotalFacturadoPorPersona`, `PersonaProductoMasCaro`, `ProductosPorCantidadFacturada`, `ProductosPorUtilidad` y `ProductosMargenGanancia`) para obtener diferentes informes y análisis de los datos almacenados en las tablas.
- **Registros**: Se insertan algunos registros de prueba en las tablas `Person`, `Product`, `InvoiceHeader` y `InvoiceDetail` para tener datos con los que trabajar.

### Ejecución

1. **Crear la base de datos y tablas**: Ejecuta el script SQL para crear la base de datos y las tablas.
2. **Crear las vistas**: Ejecuta el script SQL para crear las vistas.
3. **Insertar registros de prueba**: Ejecuta el script SQL para insertar registros de prueba en las tablas.
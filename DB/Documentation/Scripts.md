### Creación de Script SQL para Generar la Base de Datos, Tablas, Vistas y Registros

### Conceptos Básicos

**Tablas**: Son estructuras en una base de datos que almacenan datos en filas y columnas. Cada tabla se define con un esquema que especifica las columnas y sus tipos de datos.

**Vistas**: Son consultas guardadas en la base de datos que proporcionan una forma de ver los datos de una o más tablas sin almacenarlos físicamente. Las vistas pueden simplificar la complejidad de las consultas y proporcionar una capa de seguridad al ocultar partes de los datos.

**Registros**: Son filas en una tabla de base de datos. Cada registro contiene datos correspondientes a las columnas de la tabla.

### Script SQL para Crear la Base de Datos y Tablas

```sql
-- Usar la base de datos existente
USE DBMagnetron;
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

-- Vista para listar productos según su cantidad facturada en orden descendente
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
    Pr.ProductId, Pr.Description
ORDER BY 
    QuantitySold DESC;
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
    Pr.ProductId, Pr.Description
ORDER BY 
    Profit DESC;
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


### Script SQL para Insertar Registros de Prueba

```sql
-- Usar la base de datos existente
USE DBMagnetron;
GO

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

### Explicación

- **Tablas**: Las tablas `Person`, `Product`, `InvoiceHeader` y `InvoiceDetail` se crean para almacenar los datos de personas, productos y facturas.
- **Vistas**: Se crean varias vistas (`TotalFacturadoPorPersona`, `PersonaProductoMasCaro`, `ProductosPorCantidadFacturada`, `ProductosPorUtilidad` y `ProductosMargenGanancia`) para obtener diferentes informes y análisis de los datos almacenados en las tablas.
- **Registros**: Se insertan algunos registros de prueba en las tablas `Person`, `Product`, `InvoiceHeader` y `InvoiceDetail` para tener datos con los que trabajar.

### Ejecución

1. **Crear la base de datos y tablas**: Ejecuta el script SQL para crear la base de datos y las tablas.
2. **Crear las vistas**: Ejecuta el script SQL para crear las vistas.
3. **Insertar registros de prueba**: Ejecuta el script SQL para insertar registros de prueba en las tablas.
### README

# Proyecto de Facturación de Productos

## Descripción

Este proyecto tiene como objetivo desarrollar un software de facturación de productos utilizando Entity Framework Core con enfoque Code First en .NET 8.0. Incluye la creación de modelos, configuración del contexto de datos, migraciones automáticas y la creación de vistas y registros en la base de datos.

## Pasos Realizados

### 1. Creación de Modelos

Definimos los modelos para representar las tablas de la base de datos:
- **Person**
- **Product**
- **InvoiceHeader**
- **InvoiceDetail**

### 2. Configuración del DbContext

Configuramos el `FacturacionContext` para manejar las entidades y sus relaciones. Esto incluye definir las tablas y las claves foráneas en el método `OnModelCreating`.

### 3. Configuración de `program.cs`

En `program.cs`, configuramos el DbContext para usar SQL Server y añadimos la lógica para aplicar migraciones automáticas al iniciar la aplicación.

### 4. Migraciones

Utilizamos la consola del Administrador de Paquetes de NuGet para crear y aplicar las migraciones:
- **Crear la Migración Inicial**:
    ```bash
    Add-Migration InitialCreate
    ```
- **Actualizar la Base de Datos**:
    ```bash
    Update-Database
    ```

### 5. Creación de Script SQL

Generamos scripts SQL para crear tablas, vistas y registros iniciales en la base de datos.

- **Tablas**: Creamos tablas para `Person`, `Product`, `InvoiceHeader` y `InvoiceDetail` definiendo sus columnas, tipos de datos y relaciones.
- **Vistas**: Creamos vistas para consultas específicas como total facturado por persona, la persona que compró el producto más caro, productos por cantidad facturada, productos por utilidad generada y margen de ganancia.
- **Registros de Prueba**: Insertamos registros de prueba para tener datos iniciales en las tablas.

#### Script SQL para Insertar Registros de Prueba

```sql
-- Insertar registros en la tabla Person
INSERT INTO Person (FirstName, LastName, DocumentType, DocumentNumber) VALUES 
('John', 'Doe', 'DNI', '12345678'),
('Jane', 'Smith', 'DNI', '87654321'),
('Alice', 'Johnson', 'Passport', 'A1234567'),
('Bob', 'Brown', 'Passport', 'B7654321');

-- Insertar registros en la tabla Product
INSERT INTO Product (Description, Price, Cost, UnitOfMeasure) VALUES 
('Laptop', 1000.00, 800.00, 'Unit'),
('Smartphone', 600.00, 400.00, 'Unit'),
('Tablet', 400.00, 250.00, 'Unit'),
('Monitor', 200.00, 150.00, 'Unit');

-- Insertar registros en la tabla InvoiceHeader
INSERT INTO InvoiceHeader (InvoiceNumber, InvoiceDate, PersonId) VALUES 
(1, '2023-01-01', 1),
(2, '2023-01-02', 2),
(3, '2023-01-03', 1),
(4, '2023-01-04', 3);

-- Insertar registros en la tabla InvoiceDetail
INSERT INTO InvoiceDetail (LineNumber, Quantity, ProductId, InvoiceHeaderId) VALUES 
(1, 2, 1, 1),
(2, 1, 2, 1),
(1, 3, 3, 2),
(1, 1, 4, 3),
(2, 2, 2, 3),
(1, 5, 1, 4);
```

## Conclusión

Este proyecto implementa una solución de facturación de productos, utilizando buenas prácticas de desarrollo y principios SOLID. Se configuró el modelo de datos, el contexto de la base de datos, las migraciones automáticas y se generaron scripts SQL para la creación de la base de datos, vistas y registros iniciales.
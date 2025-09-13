create database APIFacturacion
go
use APIFacturacion
go
CREATE TABLE FormaPago (
    idFormaPago INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL
);

CREATE TABLE Articulo (
    idArticulo INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL
);

CREATE TABLE Factura (
    nroFactura INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    cliente NVARCHAR(100) NOT NULL,
    idFormaPago INT NOT NULL,
    FOREIGN KEY (idFormaPago) REFERENCES FormaPago(idFormaPago)
);

CREATE TABLE DetalleFactura (
    idDetalle INT IDENTITY(1,1) PRIMARY KEY,
    nroFactura INT NOT NULL,
    idArticulo INT NOT NULL,
    cantidad INT NOT NULL,
    FOREIGN KEY (nroFactura) REFERENCES Factura(nroFactura),
    FOREIGN KEY (idArticulo) REFERENCES Articulo(idArticulo)
);

--Insertar factura
CREATE PROCEDURE spInsertFactura
    @fecha DATE,
    @cliente NVARCHAR(50),
    @idFormaPago INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Factura (fecha, cliente, idFormaPago)
    VALUES (@fecha, @cliente, @idFormaPago);

    -- Devolvemos el ID generado
    SELECT SCOPE_IDENTITY() AS nroFactura;
END

--Insertar Detalle
CREATE PROCEDURE spSaveDetalleFactura
    @nroFactura INT,
    @idArticulo INT,
    @cantidad INT
AS
BEGIN
    INSERT INTO DetalleFactura(nroFactura, idArticulo, cantidad)
    VALUES(@nroFactura, @idArticulo, @cantidad);
END
GO


--Eliminar Factura y sus Detalles
CREATE PROCEDURE spDeleteFactura
    @nroFactura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE nroFactura = @nroFactura;
    DELETE FROM Factura WHERE nroFactura = @nroFactura;
END
GO


--Traer factura con detalles
CREATE PROCEDURE spGetAllFacturasConDetalles
AS
BEGIN
    -- Traigo todas las facturas
    SELECT 
        f.nroFactura,
        f.fecha,
        f.cliente,
        f.idFormaPago
    FROM Factura f;

    -- Traigo todos los detalles
    SELECT 
        d.idDetalle,
        d.nroFactura,
        d.idArticulo,
        d.cantidad
    FROM DetalleFactura d;
END

--Update factura
CREATE PROCEDURE spUpdateFactura
    @nroFactura INT,
    @fecha DATE,
    @cliente NVARCHAR(50),
    @idFormaPago INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Factura
    SET fecha = @fecha,
        cliente = @cliente,
        idFormaPago = @idFormaPago
    WHERE nroFactura = @nroFactura;
END

-- Inserciones en FormaPago
INSERT INTO FormaPago (nombre) VALUES ('Efectivo');
INSERT INTO FormaPago (nombre) VALUES ('Tarjeta de Crédito');
INSERT INTO FormaPago (nombre) VALUES ('Transferencia Bancaria');

-- Inserciones en Articulo
INSERT INTO Articulo (nombre, precioUnitario) VALUES ('Teclado Mecánico', 25000.00);
INSERT INTO Articulo (nombre, precioUnitario) VALUES ('Mouse Inalámbrico', 12000.50);
INSERT INTO Articulo (nombre, precioUnitario) VALUES ('Monitor 24"', 85000.99);
INSERT INTO Articulo (nombre, precioUnitario) VALUES ('Auriculares Gamer', 30000.00);
INSERT INTO Articulo (nombre, precioUnitario) VALUES ('Silla Ergonómica', 150000.00);

-- Inserciones en Factura
INSERT INTO Factura (fecha, cliente, idFormaPago) VALUES ('2025-09-01', 'Juan Pérez', 1);
INSERT INTO Factura (fecha, cliente, idFormaPago) VALUES ('2025-09-03', 'María Gómez', 2);
INSERT INTO Factura (fecha, cliente, idFormaPago) VALUES ('2025-09-05', 'Carlos López', 3);

-- Inserciones en DetalleFactura
-- Factura 1
INSERT INTO DetalleFactura (nroFactura, idArticulo, cantidad) VALUES (1, 1, 2); -- 2 Teclados
INSERT INTO DetalleFactura (nroFactura, idArticulo, cantidad) VALUES (1, 2, 1); -- 1 Mouse

-- Factura 2
INSERT INTO DetalleFactura (nroFactura, idArticulo, cantidad) VALUES (2, 3, 1); -- 1 Monitor
INSERT INTO DetalleFactura (nroFactura, idArticulo, cantidad) VALUES (2, 4, 2); -- 2 Auriculares

-- Factura 3
INSERT INTO DetalleFactura (nroFactura, idArticulo, cantidad) VALUES (3, 5, 1); -- 1 Silla



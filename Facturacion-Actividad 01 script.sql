
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

------------------------------------------------------------------------SP de Factura------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Eliminar Factura
CREATE PROCEDURE spDeleteFactura
    @nroFactura INT
AS
BEGIN
    DELETE FROM Factura WHERE nroFactura = @nroFactura;
END
GO

-- Obtener todas las Facturas
CREATE PROCEDURE spGetAllFacturas
AS
BEGIN
    SELECT f.nroFactura, f.fecha, f.cliente, f.idFormaPago, fp.nombre AS formaPago
    FROM Factura f
    INNER JOIN FormaPago fp ON f.idFormaPago = fp.idFormaPago;
END
GO

-------------------------------------------------------------------------SP de Detalle----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Eliminar los Detalles de una Factura
CREATE PROCEDURE spDeleteDetalleFacturaByFactura
    @nroFactura INT
AS
BEGIN
    DELETE FROM DetalleFactura WHERE nroFactura = @nroFactura;
END
GO

-- Obtener los Detalles de una Factura
CREATE PROCEDURE spGetAllDetalles
AS
BEGIN
    SELECT 
        idDetalle,
        nroFactura,
        idArticulo,
        cantidad
    FROM DetalleFactura;
END
GO

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--UPSERT FACTURA Y DETALLEFACTURA
CREATE PROCEDURE spSaveFactura
    @nroFactura INT,
    @fecha DATE,
    @cliente NVARCHAR(100),
    @idFormaPago INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Factura WHERE nroFactura = @nroFactura)
    BEGIN
        -- Ya existe ? UPDATE
        UPDATE Factura
        SET fecha = @fecha,
            cliente = @cliente,
            idFormaPago = @idFormaPago
        WHERE nroFactura = @nroFactura;
    END
    ELSE
    BEGIN
        -- No existe ? INSERT
        INSERT INTO Factura (nroFactura, fecha, cliente, idFormaPago)
        VALUES (@nroFactura, @fecha, @cliente, @idFormaPago);
    END
END;

CREATE PROCEDURE spSaveDetalleFactura
    @idDetalle INT,
    @nroFactura INT,
    @idArticulo INT,
    @cantidad INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM DetalleFactura WHERE idDetalle = @idDetalle)
    BEGIN
        -- Ya existe ? UPDATE
        UPDATE DetalleFactura
        SET nroFactura = @nroFactura,
            idArticulo = @idArticulo,
            cantidad = @cantidad
        WHERE idDetalle = @idDetalle;
    END
    ELSE
    BEGIN
        -- No existe ? INSERT
        INSERT INTO DetalleFactura (idDetalle, nroFactura, idArticulo, cantidad)
        VALUES (@idDetalle, @nroFactura, @idArticulo, @cantidad);
    END
END;
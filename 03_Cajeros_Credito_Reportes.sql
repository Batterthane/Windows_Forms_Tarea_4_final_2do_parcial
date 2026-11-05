/* =====================================================
   AMPLIACION: cajeros por turno, credito, cobro y reportes
   Ejecutar DESPUES de 01_Almacen.sql y 02_Facturacion.sql
   Seguro de repetir. Conserva las facturas ya hechas.
   ===================================================== */
USE ALMACEN;
GO
IF OBJECT_ID('dbo.Factura') IS NULL OR OBJECT_ID('dbo.FacturaDetalle') IS NULL
    RAISERROR(N'Ejecute primero 01_Almacen.sql y 02_Facturacion.sql.', 16, 1);
GO

/* ---------- 1) USUARIOS: rol Cajero, turno y estado activo ---------- */
IF COL_LENGTH('dbo.usuario','Turno')  IS NULL ALTER TABLE dbo.usuario ADD Turno NVARCHAR(10) NULL;
IF COL_LENGTH('dbo.usuario','Activo') IS NULL ALTER TABLE dbo.usuario ADD Activo BIT NOT NULL CONSTRAINT DF_usuario_Activo DEFAULT 1;
GO
DECLARE @n sysname, @sql NVARCHAR(400);
WHILE 1 = 1
BEGIN
    SET @n = NULL;
    SELECT TOP 1 @n = name FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.usuario')
      AND (definition LIKE '%Estado%' OR definition LIKE '%Turno%');
    IF @n IS NULL BREAK;
    SET @sql = N'ALTER TABLE dbo.usuario DROP CONSTRAINT ' + QUOTENAME(@n);
    EXEC (@sql);
END
GO
ALTER TABLE dbo.usuario ADD CONSTRAINT CK_usuario_Estado CHECK (Estado IN (N'Administrador', N'Cajero', N'Invitado'));
ALTER TABLE dbo.usuario ADD CONSTRAINT CK_usuario_Turno  CHECK (Turno IS NULL OR Turno IN (N'Manana', N'Tarde', N'Noche'));
ALTER TABLE dbo.usuario ADD CONSTRAINT CK_usuario_CajeroTurno CHECK (Estado <> N'Cajero' OR Turno IS NOT NULL);
GO
/* Un cajero de prueba por turno (clave: Caja2026). Puedes desactivarlos desde la app. */
IF NOT EXISTS (SELECT 1 FROM dbo.usuario WHERE Estado = N'Cajero')
INSERT INTO dbo.usuario(NombreUsuario, Clave, Estado, Turno) VALUES
 (N'cajeromanana', CONVERT(CHAR(64), HASHBYTES('SHA2_256', CONVERT(VARCHAR(50),'Caja2026')),2), N'Cajero', N'Manana'),
 (N'cajerotarde',  CONVERT(CHAR(64), HASHBYTES('SHA2_256', CONVERT(VARCHAR(50),'Caja2026')),2), N'Cajero', N'Tarde'),
 (N'cajeronoche',  CONVERT(CHAR(64), HASHBYTES('SHA2_256', CONVERT(VARCHAR(50),'Caja2026')),2), N'Cajero', N'Noche');
GO

/* ---------- 2) CLIENTES y CUENTAS POR COBRAR ---------- */
IF OBJECT_ID('dbo.Cliente') IS NULL
CREATE TABLE dbo.Cliente(
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Nombre   NVARCHAR(100) NOT NULL,
    Cedula   NVARCHAR(20)  NULL,
    Telefono NVARCHAR(20)  NULL,
    Activo   BIT NOT NULL DEFAULT 1
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Cliente_Cedula')
    CREATE UNIQUE INDEX UX_Cliente_Cedula ON dbo.Cliente(Cedula) WHERE Cedula IS NOT NULL;
GO

/* ---------- 3) FACTURA: tipo, cliente, turno, pago y numero de 4 digitos ---------- */
IF COL_LENGTH('dbo.Factura','Tipo')        IS NULL ALTER TABLE dbo.Factura ADD Tipo NVARCHAR(10) NOT NULL CONSTRAINT DF_Factura_Tipo DEFAULT N'Contado';
IF COL_LENGTH('dbo.Factura','ClienteId')   IS NULL ALTER TABLE dbo.Factura ADD ClienteId INT NULL CONSTRAINT FK_Factura_Cliente REFERENCES dbo.Cliente(Id);
IF COL_LENGTH('dbo.Factura','Turno')       IS NULL ALTER TABLE dbo.Factura ADD Turno NVARCHAR(10) NULL;
IF COL_LENGTH('dbo.Factura','MontoPagado') IS NULL ALTER TABLE dbo.Factura ADD MontoPagado DECIMAL(12,2) NULL;
IF COL_LENGTH('dbo.Factura','Devuelta')    IS NULL ALTER TABLE dbo.Factura ADD Devuelta DECIMAL(12,2) NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Factura_Tipo')
    ALTER TABLE dbo.Factura ADD CONSTRAINT CK_Factura_Tipo CHECK (Tipo IN (N'Contado', N'Credito'));
GO
/* El numero deja de ser calculado (FAC-000001) y pasa a ser un consecutivo propio */
IF EXISTS (SELECT 1 FROM sys.computed_columns WHERE object_id = OBJECT_ID('dbo.Factura') AND name = 'Numero')
BEGIN
    ALTER TABLE dbo.Factura DROP COLUMN Numero;
    ALTER TABLE dbo.Factura ADD Numero INT NULL;
END
GO
IF COL_LENGTH('dbo.Factura','Numero') IS NULL ALTER TABLE dbo.Factura ADD Numero INT NULL;
GO
UPDATE dbo.Factura SET Numero = Id WHERE Numero IS NULL;
GO
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Factura') AND name = 'Numero' AND is_nullable = 1)
    ALTER TABLE dbo.Factura ALTER COLUMN Numero INT NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Factura_Numero')
    CREATE UNIQUE INDEX UX_Factura_Numero ON dbo.Factura(Numero);
GO
IF OBJECT_ID('dbo.Consecutivo') IS NULL
    CREATE TABLE dbo.Consecutivo(Id INT PRIMARY KEY, Ultimo INT NOT NULL);
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Consecutivo)
    INSERT INTO dbo.Consecutivo(Id, Ultimo) SELECT 1, ISNULL(MAX(Numero), 0) FROM dbo.Factura;
GO

/* ---------- 4) DETALLE: ITBIS por linea ---------- */
IF COL_LENGTH('dbo.FacturaDetalle','Itbis') IS NULL
    ALTER TABLE dbo.FacturaDetalle ADD Itbis DECIMAL(12,2) NOT NULL CONSTRAINT DF_FacDet_Itbis DEFAULT 0;
GO
UPDATE dbo.FacturaDetalle SET Itbis = ROUND(Importe * 0.18, 2) WHERE Itbis = 0 AND Importe > 0;
GO

/* ---------- 5) CUENTAS POR COBRAR ---------- */
IF OBJECT_ID('dbo.CuentaPorCobrar') IS NULL
CREATE TABLE dbo.CuentaPorCobrar(
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId INT NOT NULL UNIQUE REFERENCES dbo.Factura(Id),
    ClienteId INT NOT NULL REFERENCES dbo.Cliente(Id),
    Fecha     DATETIME2(0)  NOT NULL DEFAULT SYSDATETIME(),
    Monto     DECIMAL(12,2) NOT NULL,
    Saldo     DECIMAL(12,2) NOT NULL,
    Estado    NVARCHAR(10)  NOT NULL DEFAULT N'Pendiente' CHECK (Estado IN (N'Pendiente', N'Pagada'))
);
GO

/* =====================  PROCEDIMIENTOS  ===================== */

/* Login: devuelve rol y turno solo si el usuario esta activo */
CREATE OR ALTER PROCEDURE dbo._iniciosesion
    @usuario NVARCHAR(50), @clave CHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Estado, Turno FROM dbo.usuario
    WHERE NombreUsuario = @usuario AND Clave = @clave AND Activo = 1;
END
GO

/* ---- Cajeros ---- */
CREATE OR ALTER PROCEDURE dbo.SP_LISTA_CAJEROS
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, NombreUsuario AS Usuario,
           CASE Turno WHEN N'Manana' THEN N'Mañana' ELSE Turno END AS Turno,
           CASE Activo WHEN 1 THEN N'Activo' ELSE N'Inactivo' END AS Situacion
    FROM dbo.usuario WHERE Estado = N'Cajero'
    ORDER BY CASE Turno WHEN N'Manana' THEN 1 WHEN N'Tarde' THEN 2 ELSE 3 END, NombreUsuario;
END
GO
CREATE OR ALTER PROCEDURE dbo.SP_CREA_CAJERO
    @usuario NVARCHAR(50), @clave CHAR(64), @turno NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    IF @turno NOT IN (N'Manana', N'Tarde', N'Noche') THROW 50020, N'Turno no válido.', 1;
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE NombreUsuario = @usuario) THROW 50021, N'Ya existe un usuario con ese nombre.', 1;
    INSERT INTO dbo.usuario(NombreUsuario, Clave, Estado, Turno) VALUES (@usuario, @clave, N'Cajero', @turno);
    SELECT CONVERT(INT, SCOPE_IDENTITY());
END
GO
CREATE OR ALTER PROCEDURE dbo.SP_ACTIVA_CAJERO
    @id INT, @activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.usuario SET Activo = @activo WHERE Id = @id AND Estado = N'Cajero';
    SELECT @@ROWCOUNT;
END
GO
CREATE OR ALTER PROCEDURE dbo.SP_LISTA_USUARIOS_VENTA
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NombreUsuario FROM dbo.usuario WHERE Estado IN (N'Administrador', N'Cajero') ORDER BY NombreUsuario;
END
GO

/* ---- Clientes ---- */
CREATE OR ALTER PROCEDURE dbo.SP_LISTA_CLIENTES
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Cedula, Telefono FROM dbo.Cliente WHERE Activo = 1 ORDER BY Nombre;
END
GO
CREATE OR ALTER PROCEDURE dbo.SP_CREA_CLIENTE
    @nombre NVARCHAR(100), @cedula NVARCHAR(20), @telefono NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Cliente(Nombre, Cedula, Telefono)
    VALUES (LTRIM(RTRIM(@nombre)), NULLIF(LTRIM(RTRIM(@cedula)), N''), NULLIF(LTRIM(RTRIM(@telefono)), N''));
    SELECT CONVERT(INT, SCOPE_IDENTITY());
END
GO

/* ---- Crear factura (contado o credito), todo o nada ---- */
CREATE OR ALTER PROCEDURE dbo.SP_CREA_FACTURA
    @tipo        NVARCHAR(10),
    @clienteId   INT = NULL,
    @usuario     NVARCHAR(50),
    @montoPagado DECIMAL(12,2) = NULL,
    @detalle     dbo.TipoDetalleFactura READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @tasa DECIMAL(5,4) = 0.18;
    DECLARE @msg NVARCHAR(200), @id INT, @num INT, @turno NVARCHAR(10);
    DECLARE @cliente NVARCHAR(100) = N'Consumidor final';
    DECLARE @sub DECIMAL(12,2), @itbis DECIMAL(12,2), @total DECIMAL(12,2), @devuelta DECIMAL(12,2);

    IF @tipo NOT IN (N'Contado', N'Credito') THROW 50004, N'Tipo de venta no válido.', 1;
    IF NOT EXISTS (SELECT 1 FROM @detalle) THROW 50001, N'La factura no tiene artículos.', 1;
    IF @tipo = N'Credito' AND @clienteId IS NULL THROW 50005, N'Una venta a crédito requiere un cliente.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @turno = Turno FROM dbo.usuario WHERE NombreUsuario = @usuario;

        IF @clienteId IS NOT NULL
        BEGIN
            SELECT @cliente = Nombre FROM dbo.Cliente WHERE Id = @clienteId AND Activo = 1;
            IF @@ROWCOUNT = 0 THROW 50006, N'El cliente seleccionado no existe.', 1;
        END

        SELECT Codigo, SUM(Cantidad) AS Cantidad INTO #d FROM @detalle GROUP BY Codigo;

        SELECT TOP 1 @msg = N'El producto con código ' + CAST(d.Codigo AS NVARCHAR(10)) + N' ya no existe.'
        FROM #d d LEFT JOIN dbo.Productos p WITH (UPDLOCK, HOLDLOCK) ON p.Codigo = d.Codigo
        WHERE p.Codigo IS NULL;
        IF @msg IS NOT NULL THROW 50002, @msg, 1;

        SELECT TOP 1 @msg = N'Stock insuficiente de "' + p.Nombre + N'". Disponible: ' + CAST(p.Stock AS NVARCHAR(10)) + N'.'
        FROM #d d JOIN dbo.Productos p WITH (UPDLOCK, HOLDLOCK) ON p.Codigo = d.Codigo
        WHERE d.Cantidad > p.Stock;
        IF @msg IS NOT NULL THROW 50003, @msg, 1;

        /* siguiente numero de factura, sin saltos */
        UPDATE dbo.Consecutivo SET @num = Ultimo = Ultimo + 1 WHERE Id = 1;

        INSERT INTO dbo.Factura(Numero, Tipo, ClienteId, Cliente, Usuario, Turno, Subtotal, Itbis, Total)
        VALUES (@num, @tipo, @clienteId, @cliente, @usuario, @turno, 0, 0, 0);
        SET @id = CONVERT(INT, SCOPE_IDENTITY());

        INSERT INTO dbo.FacturaDetalle(FacturaId, Codigo, Nombre, Cantidad, Precio, Itbis, Importe)
        SELECT @id, p.Codigo, p.Nombre, d.Cantidad, p.Precio,
               CAST(ROUND(d.Cantidad * p.Precio * @tasa, 2) AS DECIMAL(12,2)),
               CAST(d.Cantidad * p.Precio AS DECIMAL(12,2))
        FROM #d d JOIN dbo.Productos p ON p.Codigo = d.Codigo;

        UPDATE p SET p.Stock = p.Stock - d.Cantidad
        FROM dbo.Productos p JOIN #d d ON d.Codigo = p.Codigo;

        SELECT @sub = SUM(Importe), @itbis = SUM(Itbis) FROM dbo.FacturaDetalle WHERE FacturaId = @id;
        SET @total = @sub + @itbis;

        IF @tipo = N'Contado'
        BEGIN
            IF @montoPagado IS NULL OR @montoPagado < @total
                THROW 50007, N'El monto pagado es menor que el total a pagar.', 1;
            SET @devuelta = @montoPagado - @total;
        END
        ELSE
        BEGIN
            SET @montoPagado = NULL;
            SET @devuelta = NULL;
        END

        UPDATE dbo.Factura
        SET Subtotal = @sub, Itbis = @itbis, Total = @total, MontoPagado = @montoPagado, Devuelta = @devuelta
        WHERE Id = @id;

        IF @tipo = N'Credito'
            INSERT INTO dbo.CuentaPorCobrar(FacturaId, ClienteId, Monto, Saldo)
            VALUES (@id, @clienteId, @total, @total);

        COMMIT TRANSACTION;
        SELECT @id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

/* ---- Obtener factura: cabecera + detalle ---- */
CREATE OR ALTER PROCEDURE dbo.SP_OBTENER_FACTURA
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT f.Id, FORMAT(f.Numero, '0000') AS Numero, f.Fecha, f.Tipo, f.Usuario AS Cajero, f.Turno,
           f.Cliente, f.Subtotal, f.Itbis, f.Total, f.MontoPagado, f.Devuelta
    FROM dbo.Factura f WHERE f.Id = @id;

    SELECT Codigo, Nombre, Cantidad, Precio, Itbis, Importe
    FROM dbo.FacturaDetalle WHERE FacturaId = @id ORDER BY Id;
END
GO

/* =====================  REPORTES  ===================== */

/* Cuadre del dia (todos los cajeros o uno solo) */
CREATE OR ALTER PROCEDURE dbo.SP_REP_CUADRE
    @fecha DATE, @cajero NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Concepto, Cantidad, Monto
    FROM (
        SELECT 1 AS Orden, N'Ventas de contado' AS Concepto, COUNT(*) AS Cantidad, ISNULL(SUM(Total), 0) AS Monto
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND Tipo = N'Contado' AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 2, N'Ventas a crédito', COUNT(*), ISNULL(SUM(Total), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND Tipo = N'Credito' AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 3, N'TOTAL VENDIDO', COUNT(*), ISNULL(SUM(Total), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 4, N'Total bruto (sin ITBIS)', NULL, ISNULL(SUM(Subtotal), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 5, N'Total ITBIS', NULL, ISNULL(SUM(Itbis), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 6, N'Efectivo recibido (contado)', NULL, ISNULL(SUM(MontoPagado), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND Tipo = N'Contado' AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 7, N'Devueltas entregadas', NULL, ISNULL(SUM(Devuelta), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND Tipo = N'Contado' AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 8, N'EFECTIVO NETO EN CAJA', NULL, ISNULL(SUM(MontoPagado - Devuelta), 0)
          FROM dbo.Factura WHERE CAST(Fecha AS DATE) = @fecha AND Tipo = N'Contado' AND (@cajero IS NULL OR Usuario = @cajero)
        UNION ALL
        SELECT 9, N'Cuentas por cobrar generadas', COUNT(*), ISNULL(SUM(c.Monto), 0)
          FROM dbo.CuentaPorCobrar c JOIN dbo.Factura f ON f.Id = c.FacturaId
          WHERE CAST(f.Fecha AS DATE) = @fecha AND (@cajero IS NULL OR f.Usuario = @cajero)
    ) x
    ORDER BY Orden;
END
GO

/* Ventas por cajero en un rango de fechas */
CREATE OR ALTER PROCEDURE dbo.SP_REP_POR_CAJERO
    @desde DATE, @hasta DATE, @cajero NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT f.Usuario AS Cajero,
           ISNULL(CASE f.Turno WHEN N'Manana' THEN N'Mañana' ELSE f.Turno END, N'-') AS Turno,
           COUNT(*) AS Facturas,
           ISNULL(SUM(CASE WHEN f.Tipo = N'Contado' THEN f.Total END), 0) AS Contado,
           ISNULL(SUM(CASE WHEN f.Tipo = N'Credito' THEN f.Total END), 0) AS Credito,
           SUM(f.Total) AS Total
    FROM dbo.Factura f
    WHERE CAST(f.Fecha AS DATE) BETWEEN @desde AND @hasta AND (@cajero IS NULL OR f.Usuario = @cajero)
    GROUP BY f.Usuario, f.Turno
    ORDER BY SUM(f.Total) DESC;
END
GO

/* Inventario en stock */
CREATE OR ALTER PROCEDURE dbo.SP_REP_INVENTARIO
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Codigo, Nombre, Marca, Stock, Precio, CAST(Stock * Precio AS DECIMAL(14,2)) AS Valor
    FROM dbo.Productos ORDER BY Nombre;
END
GO

/* Facturas de contado o de credito (@tipo NULL = todas) */
CREATE OR ALTER PROCEDURE dbo.SP_REP_FACTURAS
    @desde DATE, @hasta DATE, @tipo NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FORMAT(f.Numero, '0000') AS Numero, f.Fecha,
           CASE f.Tipo WHEN N'Credito' THEN N'Crédito' ELSE N'Contado' END AS Condicion,
           f.Usuario AS Cajero, f.Cliente, f.Total
    FROM dbo.Factura f
    WHERE CAST(f.Fecha AS DATE) BETWEEN @desde AND @hasta AND (@tipo IS NULL OR f.Tipo = @tipo)
    ORDER BY f.Numero;
END
GO

/* 5 clientes con mas ventas (solo clientes registrados) */
CREATE OR ALTER PROCEDURE dbo.SP_REP_TOP_CLIENTES
    @desde DATE, @hasta DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5 ROW_NUMBER() OVER (ORDER BY SUM(f.Total) DESC) AS Pos,
           c.Nombre AS Cliente, ISNULL(c.Cedula, N'') AS Cedula,
           COUNT(*) AS Facturas, SUM(f.Total) AS Vendido
    FROM dbo.Factura f JOIN dbo.Cliente c ON c.Id = f.ClienteId
    WHERE CAST(f.Fecha AS DATE) BETWEEN @desde AND @hasta
    GROUP BY c.Id, c.Nombre, c.Cedula
    ORDER BY SUM(f.Total) DESC;
END
GO

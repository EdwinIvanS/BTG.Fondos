
USE BTG;
GO

-- =============
-- LIMPIAR DATOS 
-- =============
DELETE FROM visitan;
DELETE FROM disponibilidad;
DELETE FROM inscripcion;
DELETE FROM sucursal;
DELETE FROM producto;
DELETE FROM cliente;
GO

-- CLIENTES
INSERT INTO cliente (id, nombre, apellidos, ciudad) VALUES
(1, 'Carlos', 'Gómez', 'Bogotá'),
(2, 'Ana', 'Martínez', 'Medellín'),
(3, 'Luis', 'Rojas', 'Cali'),
(4, 'María', 'Pérez', 'Bogotá');
GO

-- PRODUCTOS
INSERT INTO producto (id, nombre, tipoProducto) VALUES
(1, 'Guitarra Eléctrica', 'Instrumento Musical'),
(2, 'Automóvil Deportivo', 'Vehículo'),
(3, 'Refrigerador LG', 'Electrodoméstico'),
(4, 'Televisor Samsung', 'Electrodoméstico'),
(5, 'Batería Acústica', 'Instrumento Musical');
GO

-- SUCURSALES
INSERT INTO sucursal (id, nombre, ciudad) VALUES
(1, 'Sucursal Norte', 'Bogotá'),
(2, 'Sucursal Sur', 'Bogotá'),
(3, 'Sucursal Centro', 'Medellín'),
(4, 'Sucursal Occidente', 'Cali');
GO

-- INSCRIPCIONES 
INSERT INTO inscripcion (idProducto, idCliente) VALUES
(1, 1),
(2, 1), 
(3, 2),
(4, 3), 
(5, 4); 
GO

-- DISPONIBILIDAD
INSERT INTO disponibilidad (idSucursal, idProducto) VALUES
(1, 1), 
(2, 1), 
(1, 2),
(3, 3), 
(4, 4), 
(1, 5), 
(2, 5); 
GO

-- VISITAN 
INSERT INTO visitan (idSucursal, idCliente, fechaVisita) VALUES
(1, 1, '2025-10-01'), 
(2, 1, '2025-10-02'), 
(3, 2, '2025-10-03'), 
(2, 3, '2025-10-04'), 
(1, 4, '2025-10-05'), 
(2, 4, '2025-10-06'); 
GO
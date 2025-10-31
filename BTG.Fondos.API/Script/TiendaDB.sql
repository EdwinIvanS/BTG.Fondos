CREATE DATABASE BTG;
GO

USE BTG;
GO

-- TABLA: cliente
CREATE TABLE cliente (
    id INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellidos VARCHAR(50) NOT NULL,
    ciudad VARCHAR(50) NOT NULL
);
GO

-- TABLA: producto
CREATE TABLE producto (
    id INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    tipoProducto VARCHAR(50) NOT NULL
);
GO

-- TABLA: sucursal
CREATE TABLE sucursal (
    id INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    ciudad VARCHAR(50) NOT NULL
);
GO

-- TABLA: inscripcion
CREATE TABLE inscripcion (
    idProducto INT NOT NULL,
    idCliente INT NOT NULL,
    PRIMARY KEY (idProducto, idCliente),
    FOREIGN KEY (idProducto) REFERENCES producto(id),
    FOREIGN KEY (idCliente) REFERENCES cliente(id)
);
GO

-- TABLA: disponibilidad
CREATE TABLE disponibilidad (
    idSucursal INT NOT NULL,
    idProducto INT NOT NULL,
    PRIMARY KEY (idSucursal, idProducto),
    FOREIGN KEY (idSucursal) REFERENCES sucursal(id),
    FOREIGN KEY (idProducto) REFERENCES producto(id)
);
GO

-- TABLA: visitan
CREATE TABLE visitan (
    idSucursal INT NOT NULL,
    idCliente INT NOT NULL,
    fechaVisita DATE NOT NULL,
    PRIMARY KEY (idSucursal, idCliente),
    FOREIGN KEY (idSucursal) REFERENCES sucursal(id),
    FOREIGN KEY (idCliente) REFERENCES cliente(id)
);
GO
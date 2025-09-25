CREATE DATABASE MercaJumbo;

USE MercaJumbo;

CREATE TABLE Clientes (
	IdCliente INT PRIMARY KEY,
	Nombre VARCHAR(100),
	Apellido VARCHAR(100),
	Correo VARCHAR(150) UNIQUE,
    Telefono VARCHAR(20),
    FechaRegistro DATE DEFAULT GETDATE()
);

CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY,
    NombreProducto NVARCHAR(150),
    Descripcion NVARCHAR(250),
    Categoria NVARCHAR(100),
    Precio DECIMAL(10,2)
);

CREATE TABLE Ventas (
    IdVenta INT PRIMARY KEY,
    IdCliente INT,
    IdProducto INT,
    FechaVenta DATE DEFAULT GETDATE(),
    Cantidad INT,
    MontoTotal DECIMAL(10,2),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

CREATE TABLE Opiniones (
    IdOpinion INT PRIMARY KEY,
    IdCliente INT,
    IdProducto INT,
    Calificacion INT CHECK (Calificacion BETWEEN 1 AND 5),
    Comentario NVARCHAR(200),
    FechaOpinion DATE DEFAULT GETDATE(),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

CREATE TABLE Encuestas (
    IdEncuesta INT PRIMARY KEY,
    IdCliente INT,
    Pregunta NVARCHAR(255),
    Respuesta NVARCHAR(100),
    FechaEncuesta DATE DEFAULT GETDATE(),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE FuentesDatos (
    IdFuente INT PRIMARY KEY,
    NombreFuente NVARCHAR(100),
    TipoFuente NVARCHAR(50),
    URL NVARCHAR(255)
);

CREATE TABLE ComentariosSociales (
    IdComentario INT PRIMARY KEY,
    IdFuente INT,
    IdCliente INT,
    TextoComentario NVARCHAR(100),
    FechaComentario DATE DEFAULT GETDATE(),
    Reacciones INT DEFAULT 0,
    FOREIGN KEY (IdFuente) REFERENCES FuentesDatos(IdFuente),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);

CREATE TABLE ResenhasWeb (
    IdResenha INT PRIMARY KEY,
    IdFuente INT,
    IdProducto INT,
    Calificacion INT CHECK (Calificacion BETWEEN 1 AND 5),
    Comentario NVARCHAR(250),
    FechaResenha DATE DEFAULT GETDATE(),
    FOREIGN KEY (IdFuente) REFERENCES FuentesDatos(IdFuente),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

SELECT * FROM Clientes;
SELECT * FROM Productos;
SELECT * FROM Ventas;
SELECT * FROM Opiniones;
SELECT * FROM Encuestas;
SELECT * FROM FuentesDatos
SELECT * FROM ComentariosSociales
SELECT * FROM ResenhasWeb


CREATE DATABASE LojaCarros;

USE LojaCarros;

CREATE TABLE Marcas (
MarcaID int IDENTITY(1,1) PRIMARY KEY not null,
NomeMarca nvarchar(100) not null UNIQUE
);

CREATE TABLE Modelos (
ModeloID int IDENTITY(1,1) PRIMARY KEY not null,
Modelo nvarchar(100) not null,
MarcaID int not null,

CONSTRAINT FK_Modelo_Marca
FOREIGN KEY (MarcaID) REFERENCES Marcas(MarcaID),

CONSTRAINT UQ_Modelo_Marca UNIQUE (Modelo, MarcaID)
);

CREATE TABLE Carros (
CarroID INT IDENTITY(1,1) PRIMARY KEY not null,
Ano int NOT NULL,
Placa NVARCHAR(10) NOT NULL unique,
UltimaInspecao date,
vendido BIT not null,
ModeloID int not null,

CONSTRAINT FK_Carro_Modelo FOREIGN KEY (ModeloID) REFERENCES Modelos(ModeloID)
);

-- Script de base de datos
-- Sistema de Parqueo Inteligente
-- Base de datos: Parqueo.db

PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS Pagos;
DROP TABLE IF EXISTS Vehiculos;
DROP TABLE IF EXISTS Espacios;
DROP TABLE IF EXISTS Configuracion;

CREATE TABLE Espacios (
    id_espacio INTEGER PRIMARY KEY AUTOINCREMENT,
    numero INTEGER NOT NULL,
    estado TEXT NOT NULL DEFAULT 'Disponible',
    seccion TEXT NOT NULL
);

CREATE TABLE Vehiculos (
    id_vehiculos INTEGER PRIMARY KEY AUTOINCREMENT,
    placa TEXT NOT NULL UNIQUE,
    tipo TEXT NOT NULL,
    hora_entrada TEXT NOT NULL,
    espacio_id INTEGER NOT NULL,
    FOREIGN KEY (espacio_id) REFERENCES Espacios(id_espacio)
);

CREATE TABLE Pagos (
    id_pago INTEGER PRIMARY KEY AUTOINCREMENT,
    vehiculo_id INTEGER NOT NULL,
    hora_salida TEXT NOT NULL,
    total_pagar REAL NOT NULL,
    FOREIGN KEY (vehiculo_id) REFERENCES Vehiculos(id_vehiculos)
);

CREATE TABLE Configuracion (
    clave TEXT PRIMARY KEY,
    valor TEXT NOT NULL
);

-- Configuración inicial
INSERT INTO Configuracion (clave, valor) VALUES ('tarifa_carro', '2.00');
INSERT INTO Configuracion (clave, valor) VALUES ('tarifa_moto', '2.00');
INSERT INTO Configuracion (clave, valor) VALUES ('total_espacios_seccion1', '20');
INSERT INTO Configuracion (clave, valor) VALUES ('total_espacios_seccion2', '10');

-- Espacios para carros: Sección 1
INSERT INTO Espacios (numero, estado, seccion) VALUES
(1, 'Disponible', 'Seccion 1'),
(2, 'Disponible', 'Seccion 1'),
(3, 'Disponible', 'Seccion 1'),
(4, 'Disponible', 'Seccion 1'),
(5, 'Disponible', 'Seccion 1'),
(6, 'Disponible', 'Seccion 1'),
(7, 'Disponible', 'Seccion 1'),
(8, 'Disponible', 'Seccion 1'),
(9, 'Disponible', 'Seccion 1'),
(10, 'Disponible', 'Seccion 1'),
(11, 'Disponible', 'Seccion 1'),
(12, 'Disponible', 'Seccion 1'),
(13, 'Disponible', 'Seccion 1'),
(14, 'Disponible', 'Seccion 1'),
(15, 'Disponible', 'Seccion 1'),
(16, 'Disponible', 'Seccion 1'),
(17, 'Disponible', 'Seccion 1'),
(18, 'Disponible', 'Seccion 1'),
(19, 'Disponible', 'Seccion 1'),
(20, 'Disponible', 'Seccion 1');

-- Espacios para motos: Sección 2
INSERT INTO Espacios (numero, estado, seccion) VALUES
(21, 'Disponible', 'Seccion 2'),
(22, 'Disponible', 'Seccion 2'),
(23, 'Disponible', 'Seccion 2'),
(24, 'Disponible', 'Seccion 2'),
(25, 'Disponible', 'Seccion 2'),
(26, 'Disponible', 'Seccion 2'),
(27, 'Disponible', 'Seccion 2'),
(28, 'Disponible', 'Seccion 2'),
(29, 'Disponible', 'Seccion 2'),
(30, 'Disponible', 'Seccion 2');
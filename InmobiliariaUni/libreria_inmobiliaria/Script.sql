CREATE DATABASE inmobiliariaUni;
GO

USE inmobiliariaUni;
GO

DROP DATABASE inmobiliariaUni;
USE db_academiaBaile;

CREATE TABLE Auditorias (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TipoAccion NVARCHAR(40) NOT NULL,
	Entidad NVARCHAR(40) NOT NULL,
	IdEntidad INT NOT NULL,
	Fecha DATETIME NOT NULL,
	Descripcion NVARCHAR(600) NOT NULL
);

CREATE TABLE Nacionalidades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
	Estado BIT DEFAULT 1
);

CREATE TABLE Departamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
	Estado BIT DEFAULT 1
);
 
CREATE TABLE TiposPropiedades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
	Estado BIT DEFAULT 1
);

CREATE TABLE Ciudades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Poblacion INT,
    FechaCreacion DATETIME,
    CodigoPostal NVARCHAR(20),
    Estado BIT DEFAULT 1 NOT NULL,
    Departamento INT NOT NULL,    

    FOREIGN KEY (Departamento) REFERENCES Departamentos(Id)
);

CREATE TABLE Sectores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Estado BIT DEFAULT 1 NOT NULL,
    Ciudad INT NOT NULL,

    FOREIGN KEY (Ciudad) REFERENCES Ciudades(Id)
);

CREATE TABLE UsuariosRoles (
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Correo VARCHAR(70) NOT NULL,
    Contraseña VARCHAR(70) NOT NULL,
    Rol VARCHAR(30) NOT NULL
);

CREATE TABLE Personas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cedula NVARCHAR(20),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100),
    FechaNacimiento DATETIME NOT NULL,
    FechaRegistro DATETIME NOT NULL,
    Estado BIT,
    Nacionalidad INT NOT NULL,

    FOREIGN KEY (Nacionalidad) REFERENCES Nacionalidades(Id),
);

CREATE TABLE Compradores (
    Id INT NOT NULL PRIMARY KEY,
    PresupuestoMaximo DECIMAL(18,2),

    FOREIGN KEY (Id) REFERENCES Personas(Id)
);

CREATE TABLE Codeudores (
    Id INT NOT NULL PRIMARY KEY,
    Comprador INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (Comprador) REFERENCES Compradores(Id)
);

-- Tabla que indica el respaldo financiero de el cliente y el codeudor
CREATE TABLE RespaldosFinancieros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DeudasTotales DECIMAL(18,2) NOT NULL,
    IngresosMensuales DECIMAL(18,2),
    Observaciones VARCHAR(200) NOT NULL
);

-- Tabla que indica el respaldo del comprado
CREATE TABLE RespaldosCompradores (
    Id INT NOT NULL PRIMARY KEY,
    Comprador INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES RespaldosFinancieros(Id),
    FOREIGN KEY (Comprador) REFERENCES Compradores(Id)
);

-- Tabla que indica el respaldo del codeudor
CREATE TABLE RespaldosCodeudores (
    Id INT NOT NULL PRIMARY KEY,
    Codeudor INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES RespaldosFinancieros(Id),
    FOREIGN KEY (Codeudor) REFERENCES Codeudores(Id)
);

-- Esta lleva la clave foranea de la tabla principal para distinguir entre comprador y codeudor
CREATE TABLE Bienes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150),
    Descripcion NVARCHAR(500),
    FechaAdquisicion DATETIME,
    ValorAdquisicion DECIMAL(18,2),
    ValorActual DECIMAL(18,2),
    RespaldoFinanciero INT NOT NULL,

    FOREIGN KEY (RespaldoFinanciero) REFERENCES RespaldosFinancieros(Id)
);

-- Esta lleva la clave foranea de la tabla principal para distinguir entre comprador y codeudor
CREATE TABLE ActivosFinancieros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150),
    Descripcion NVARCHAR(500),
    FechaAdquisicion DATETIME,
    Valor DECIMAL(18,2),
    RespaldoFinanciero INT NOT NULL,

    FOREIGN KEY (RespaldoFinanciero) REFERENCES RespaldosFinancieros(Id)
);

CREATE TABLE AdministradoresDepartamentos (
    Id INT NOT NULL PRIMARY KEY,
    PresupuestoDepartamento DECIMAL(18,2),
    HorarioTrabajo VARCHAR(20) NOT NULL,
    Sueldo DECIMAL(18,2),
    Departamento INT NOT NULL,
    UsuarioRol INT NOT NULL,

    FOREIGN KEY (UsuarioRol) REFERENCES UsuariosRoles(Id),
    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (Departamento) REFERENCES Departamentos(Id),
);

CREATE TABLE JefesSectores (
    Id INT NOT NULL PRIMARY KEY,
    PresupuestoSector DECIMAL(18,2),
    HorarioTrabajo VARCHAR(20) NOT NULL,
    Sueldo DECIMAL(18,2),
    Sector INT NOT NULL,
    AdministradorSector INT NOT NULL,
    UsuarioRol INT NOT NULL,

    FOREIGN KEY (UsuarioRol) REFERENCES UsuariosRoles(Id),
    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (Sector) REFERENCES Sectores(Id),
    FOREIGN KEY (AdministradorSector) REFERENCES AdministradoresDepartamentos(Id),
);

CREATE TABLE EmpleadosSectores (
    Id INT NOT NULL PRIMARY KEY,
    HorarioTrabajo VARCHAR(20) NOT NULL,
    Sueldo DECIMAL(18,2),
    Sector INT NOT NULL,
    JefeSector INT NOT NULL,
    UsuarioRol INT NOT NULL,

    FOREIGN KEY (UsuarioRol) REFERENCES UsuariosRoles(Id),
    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (Sector) REFERENCES Sectores(Id),
    FOREIGN KEY (JefeSector) REFERENCES JefesSectores(Id),
);

CREATE TABLE Clientes (
    Id INT NOT NULL PRIMARY KEY,
    PorcentajeComision DECIMAL(18,2),
    Calificacion INT,
    EmpleadoSector INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (EmpleadoSector) REFERENCES EmpleadosSectores(Id)
);

CREATE TABLE Direcciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoVia NVARCHAR(100),
    Numero NVARCHAR(50),
    Complemento NVARCHAR(200),
    Persona INT NOT NULL,
    Ciudad INT NOT NULL,

    FOREIGN KEY (Persona) REFERENCES Personas(Id),
    FOREIGN KEY (Ciudad) REFERENCES Ciudades(Id)
);

CREATE TABLE Telefonos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(20),
    Prefijo NVARCHAR(10),
    Persona INT NOT NULL,

    FOREIGN KEY (Persona) REFERENCES Personas(Id)
);

CREATE TABLE ExpedientesLaborales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FechaIngreso DATETIME,
    Cargo NVARCHAR(150),
    Antiguedad INT,
    EstadoLaboral NVARCHAR(100),
    Persona INT NOT NULL,

    FOREIGN KEY (Persona) REFERENCES Personas(Id)
);

CREATE TABLE Propiedades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(60) NOT NULL,
    NumeroHabitaciones INT,
    NumeroBaños INT,
    Patio BIT,
    Garaje BIT,
    Pisos INT,
    FechaConstruccion DATETIME,
    ValorPropiedad DECIMAL(18,2),
    ValorArriendo DECIMAL(18,2),
    Disponible BIT,
    Cliente INT NOT NULL,
    TipoPropiedad INT NOT NULL,
    Sector INT NOT NULL,

    FOREIGN KEY (Cliente) REFERENCES Clientes(Id),
    FOREIGN KEY (TipoPropiedad) REFERENCES TiposPropiedades(Id),
    FOREIGN KEY (Sector) REFERENCES Sectores(Id)
);

-- Tabla padre (campos comunes a los dos tipos)
CREATE TABLE Contratos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FechaContrato DATETIME,
    FechaFinalizacion DATETIME,
    PrecioAcordado DECIMAL(18,2) NOT NULL,
    Estado VARCHAR(40) NOT NULL,
    Codeudor INT NOT NULL,
    Cliente INT NOT NULL,
    Comprador INT NOT NULL,
    EmpleadoSector INT NOT NULL,
    Propiedad INT NOT NULL,

    FOREIGN KEY (Codeudor) REFERENCES Codeudores(Id),
    FOREIGN KEY (Cliente) REFERENCES Clientes(Id),
    FOREIGN KEY (Comprador) REFERENCES Compradores(Id),
    FOREIGN KEY (EmpleadoSector) REFERENCES EmpleadosSectores(Id),
    FOREIGN KEY (Propiedad) REFERENCES Propiedades(Id),
);

-- Tabla hija arriendo (campos exclusivos de arriendo)
CREATE TABLE ContratosArriendo (
    Id INT NOT NULL PRIMARY KEY,
    ValorMensual DECIMAL(18,2) NOT NULL,
    DiaPago INT NOT NULL,              
    Renovable BIT,           

    FOREIGN KEY (Id) REFERENCES Contratos(Id)
);

CREATE TABLE ContratosCuotas (
    Id INT NOT NULL PRIMARY KEY,
    NumeroCuotas INT NOT NULL,        
    ValorCuota DECIMAL(18,2) NOT NULL,
    PagoInicial DECIMAL(18,2) NOT NULL,

    FOREIGN KEY (Id) REFERENCES Contratos(Id)
);

CREATE TABLE ContratosContado (	
    Id INT NOT NULL PRIMARY KEY,
    PrecioAcordado DECIMAL(18,2)

    FOREIGN KEY (Id) REFERENCES Contratos(Id)
);

ALTER TABLE Departamentos 
ADD Latitud FLOAT NULL,
    Longitud FLOAT NULL;

-- Usuarios para pruebas
INSERT INTO UsuariosRoles (Correo, Contraseña, Rol) VALUES
('admin@inmobiliaria.com', 'Admin123', 'Administrador'),
('jefe@inmobiliaria.com', 'Jefe123', 'Jefe'),
('empleado@inmobiliaria.com', 'Empleado123', 'Empleado');

-- Nacionalidad base para pruebas
INSERT INTO Nacionalidades (Nombre, Estado) VALUES ('Colombiana', 1);

-- Departamento base para pruebas
INSERT INTO Departamentos (Nombre, Estado) VALUES ('Departamento Principal', 1);

-- Ciudad base para pruebas
INSERT INTO Ciudades (Nombre, Poblacion, FechaCreacion, CodigoPostal, Estado, Departamento) 
VALUES ('Medellin', 2500000, GETDATE(), '050001', 1, 1);

-- Sector base para pruebas
INSERT INTO Sectores (Nombre, Estado, Ciudad) VALUES ('Sector Principal', 1, 1);

-- Personas para pruebas
INSERT INTO Personas (Cedula, Nombre, Apellido, FechaNacimiento, FechaRegistro, Estado, Nacionalidad) VALUES
('1000000001', 'Carlos', 'Garcia', '1985-03-15', GETDATE(), 1, 1),  -- Admin
('1000000002', 'Maria', 'Lopez', '1990-07-22', GETDATE(), 1, 1),    -- Jefe
('1000000003', 'Juan', 'Martinez', '1995-11-10', GETDATE(), 1, 1);  -- Empleado

-- Administrador Id = 1 de Personas, UsuarioRol = 1 para pruebas
INSERT INTO AdministradoresDepartamentos (Id, PresupuestoDepartamento, HorarioTrabajo, Sueldo, Departamento, UsuarioRol)
VALUES (1, 50000000.00, '10pm a 6am', 8000000.00, 1, 1);

-- Jefe Id = 2 de Personas, UsuarioRol = 2 para pruebas
INSERT INTO JefesSectores (Id, PresupuestoSector, HorarioTrabajo, Sueldo, Sector, AdministradorSector, UsuarioRol)
VALUES (2, 20000000.00, '10pm a 6am', 5000000.00, 1, 1, 2);

-- Empleado Id = 3 de Personas, UsuarioRol = 3 para pruebas
INSERT INTO EmpleadosSectores (Id, HorarioTrabajo, Sueldo, Sector, JefeSector, UsuarioRol)
VALUES (3, '10pm a 6am', 3000000.00, 1, 2, 3);


-- Persona Comprador Id = 5
INSERT INTO Personas (Cedula, Nombre, Apellido, FechaNacimiento, FechaRegistro, Estado, Nacionalidad)
VALUES ('1000000010', 'Pedro', 'Ramirez', '1988-05-20', GETDATE(), 1, 1);

-- Comprador Id = 5
INSERT INTO Compradores (Id, PresupuestoMaximo)
VALUES (5, 500000000.00);

-- Respaldo financiero del comprador Id = 1
INSERT INTO RespaldosFinancieros (DeudasTotales, IngresosMensuales, Observaciones)
VALUES (5000000.00, 8000000.00, 'Comprador con ingresos estables');

INSERT INTO RespaldosCompradores (Id, Comprador)
VALUES (1, 5);

INSERT INTO Bienes (Nombre, Descripcion, FechaAdquisicion, ValorAdquisicion, ValorActual, RespaldoFinanciero)
VALUES ('Vehiculo', 'Toyota Corolla 2020', '2020-01-15', 60000000.00, 50000000.00, 1);

INSERT INTO ActivosFinancieros (Nombre, Descripcion, FechaAdquisicion, Valor, RespaldoFinanciero)
VALUES ('Cuenta bancaria', 'Cuenta de ahorros Bancolombia', GETDATE(), 20000000.00, 1);

-- ─── Codeudor ───
INSERT INTO Personas (Cedula, Nombre, Apellido, FechaNacimiento, FechaRegistro, Estado, Nacionalidad)
VALUES ('1000000011', 'Laura', 'Gomez', '1990-03-10', GETDATE(), 1, 1);

-- Codeudor Id = 6, referencia al comprador Id = 5
INSERT INTO Codeudores (Id, Comprador)
VALUES (6, 5);

-- Respaldo financiero del codeudor Id = 2
INSERT INTO RespaldosFinancieros (DeudasTotales, IngresosMensuales, Observaciones)
VALUES (2000000.00, 5000000.00, 'Codeudor con ingresos moderados');

INSERT INTO RespaldosCodeudores (Id, Codeudor)
VALUES (2, 6);

INSERT INTO Bienes (Nombre, Descripcion, FechaAdquisicion, ValorAdquisicion, ValorActual, RespaldoFinanciero)
VALUES ('Apartamento', 'Apto en Laureles', '2015-06-01', 200000000.00, 220000000.00, 2);

INSERT INTO ActivosFinancieros (Nombre, Descripcion, FechaAdquisicion, Valor, RespaldoFinanciero)
VALUES ('CDT', 'CDT Davivienda', GETDATE(), 15000000.00, 2);

-- ─── Cliente ───
INSERT INTO Personas (Cedula, Nombre, Apellido, FechaNacimiento, FechaRegistro, Estado, Nacionalidad)
VALUES ('1000000012', 'Sofia', 'Torres', '1992-08-14', GETDATE(), 1, 1);

-- Cliente Id = 7, asignado al empleado Id = 3
INSERT INTO Clientes (Id, PorcentajeComision, Calificacion, EmpleadoSector)
VALUES (7, 5.00, 8, 3);

-- ─── Tipo propiedad ───
INSERT INTO TiposPropiedades (Nombre, Estado)
VALUES ('Apartamento', 1);

-- ─── Propiedad del cliente ───
INSERT INTO Propiedades (Codigo, NumeroHabitaciones, NumeroBaños, Patio, Garaje, Pisos, FechaConstruccion, ValorPropiedad, ValorArriendo, Disponible, Cliente, TipoPropiedad, Sector)
VALUES ('PROP-001', 3, 2, 1, 1, 2, '2010-06-15', 350000000.00, 2000000.00, 1, 7, 1, 1);

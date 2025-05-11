CREATE TABLE rol(
id_rol INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50),
descripcion VARCHAR(100)
);

CREATE TABLE categoria(
nombre VARCHAR(50) PRIMARY KEY 
);

CREATE TABLE patrocinador(
id_patrocinador INT PRIMARY KEY,
dinero FLOAT,
texto VARCHAR(100),
imagen VARCHAR(100)
);

CREATE TABLE equipo(
id_equipo INT PRIMARY KEY,
escudo VARCHAR(100),
nombre VARCHAR(50),
categoria VARCHAR(50),
CONSTRAINT FK_equipo_cat FOREIGN KEY (categoria) REFERENCES categoria
);

CREATE TABLE pais(
id_pais INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(100),
);

CREATE TABLE provincia(
id_provincia INT IDENTITY(1,1) PRIMARY KEY,
id_pais INT,
nombre VARCHAR(100),
CONSTRAINT FK_provi_pais FOREIGN KEY (id_pais) REFERENCES pais
);

CREATE TABLE municipio(
id_municipio INT IDENTITY(1,1) PRIMARY KEY,
id_provincia INT,
nombre VARCHAR(100),
CONSTRAINT FK_muni_provi FOREIGN KEY (id_provincia) REFERENCES provincia
);

CREATE TABLE usuario (
id INT IDENTITY(1,1) PRIMARY KEY,
imagen VARCHAR(100),
contrasenya VARCHAR(50),
correo VARCHAR(50),
nombre VARCHAR(50),
saldo FLOAT,
numero_tar VARCHAR(50),
caducidad_tar DATE,
cvv VARCHAR(3),
direccion VARCHAR(50),
telefono VARCHAR(9),
id_rol INT,
id_municipio INT,
CONSTRAINT FK_usuario_rol FOREIGN KEY (id_rol) REFERENCES rol,
CONSTRAINT FK_usuario_muni FOREIGN KEY (id_municipio) REFERENCES municipio
);

CREATE TABLE notificacion(
id INT IDENTITY(1,1),
id_usuario INT,
texto VARCHAR(200),
CONSTRAINT PK_notificacion PRIMARY KEY (id, id_usuario),
CONSTRAINT FK_noti_usu FOREIGN KEY (id_usuario) REFERENCES usuario
);

CREATE TABLE favoritos(
id INT IDENTITY(1,1),
id_usuario INT,
CONSTRAINT PK_fav PRIMARY KEY (id, id_usuario),
CONSTRAINT FK_fav_usu FOREIGN KEY (id_usuario) REFERENCES usuario
);

CREATE TABLE transaccion(
id INT IDENTITY(1,1),
dinero FLOAT,
metodo VARCHAR(50),
id_usu INT,
CONSTRAINT PK_trans PRIMARY KEY (id),
CONSTRAINT FK_trans_usu FOREIGN KEY (id_usu) REFERENCES usuario
);

CREATE TABLE estadio(
nombre VARCHAR(50) PRIMARY KEY,
capacidad INT,
texto VARCHAR(300),
id_municipio INT,
CONSTRAINT FK_estadio_municipio FOREIGN KEY (id_municipio) REFERENCES municipio
);

CREATE TABLE apuesta (
id_apuesta INT PRIMARY KEY,
resultado INT,
fecha DATE,
id_equipo1 INT,
id_equipo2 INT,
estadio VARCHAR(50),
CONSTRAINT FK_apuesta_estadio FOREIGN KEY (estadio) REFERENCES estadio,
CONSTRAINT FK_apuesta_equipo1 FOREIGN KEY (id_equipo1) REFERENCES equipo,
CONSTRAINT FK_apuesta_equipo2 FOREIGN KEY (id_equipo2) REFERENCES equipo,
);

CREATE TABLE apuesta_usu(
id_usuario INT,
id_apuesta INT,
dinero_apostado FLOAT,
cuota FLOAT,
prediccion INT,
CONSTRAINT PK_apuestausu PRIMARY KEY (id_usuario, id_apuesta),
CONSTRAINT FK_apuestausu_usuario FOREIGN KEY (id_usuario) REFERENCES usuario,
CONSTRAINT FK_apuestausu_apuesta FOREIGN KEY (id_apuesta) REFERENCES apuesta
);
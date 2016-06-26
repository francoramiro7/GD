CREATE SCHEMA PERSISTIENDO

CREATE TABLE PERSISTIENDO.Usuario(
Usuario_username nvarchar(30) NOT NULL,
Usuario_password varbinary(100) NOT NULL,
Usuario_habilitado bit NOT NULL,
Usuario_nuevo bit NOT NULL,
Usuario_administrador bit NOT NULL,
Usuario_intentos int NOT NULL,
PRIMARY KEY (Usuario_username)
)
GO

CREATE TABLE PERSISTIENDO.Rol(
Rol_codigo INT IDENTITY (1,1) NOT NULL,
Rol_nombre nvarchar(30) NOT NULL,
Rol_habilitado bit,
PRIMARY KEY(Rol_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Funcionalidad(
Func_codigo INT IDENTITY (1,1) NOT NULL,
Func_nombre nvarchar(30) NOT NULL,
PRIMARY KEY(Func_codigo)
)
GO


CREATE TABLE PERSISTIENDO.Rol_Por_Usuario(
RPU_username nvarchar(30) NOT NULL,
RPU_codigo_rol int NOT NULL,
PRIMARY KEY(RPU_username, RPU_codigo_rol),
CONSTRAINT fk_rpu_username FOREIGN KEY (RPU_username) REFERENCES PERSISTIENDO.Usuario (Usuario_username),
CONSTRAINT fk_rpu_codigo_rol FOREIGN KEY (RPU_codigo_rol) REFERENCES PERSISTIENDO.Rol (Rol_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Funcionalidad_por_rol(
FPR_codigo_func int NOT NULL,
FPR_codigo_rol int NOT NULL,
PRIMARY KEY(FPR_codigo_func, FPR_codigo_rol),
CONSTRAINT fk_fpr_codigo_func FOREIGN KEY (FPR_codigo_func) REFERENCES PERSISTIENDO.Funcionalidad (Func_codigo),
CONSTRAINT fk_fpr_codigo_rol FOREIGN KEY (FPR_codigo_rol) REFERENCES PERSISTIENDO.Rol (Rol_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Cliente(
Cliente_Username nvarchar(30) NOT NULL,
Cliente_apellido nvarchar(255) NOT NULL,
Cliente_nombre nvarchar(255) NOT NULL,
Cliente_mail nvarchar(255) NOT NULL,
Cliente_dom_calle nvarchar(255) NOT NULL,
Cliente_codigo_postal nvarchar(50) NOT NULL,
Cliente_depto nvarchar(50),
Cliente_piso numeric(18,0),
Cliente_dni numeric(18,0) NOT NULL,
Cliente_tipo_documento nvarchar(255) NOT NULL,
Cliente_nro_calle numeric(18,0) NOT NULL,
Cliente_fecha_nacimiento datetime NOT NULL,
Cliente_feche_creacion datetime,
Cliente_localidad nvarchar(255),
PRIMARY KEY(Cliente_username),
CONSTRAINT fk_cliente_username FOREIGN KEY (Cliente_username) REFERENCES PERSISTIENDO.Usuario (Usuario_username)
)
GO


CREATE TABLE PERSISTIENDO.Visibilidad(
Visibilidad_cod numeric(18,0) NOT NULL,
Visibilidad_descripcion nvarchar(255),
Visibilidad_precio numeric(18,2) NOT NULL,
Visibilidad_porcentaje numeric(18,2) NOT NULL,
Visibilidad_precio_envio numeric(18,2) NOT NULL,
PRIMARY KEY(Visibilidad_cod)
)
GO


CREATE TABLE PERSISTIENDO.Tipo_publicacion(
Tipo_publicacion_codigo int IDENTITY(1,1) NOT NULL,
Tipo_publicacion_descripcion nvarchar(255) NOT NULL,
Tipo_publicacion_envio_habilitado bit NOT NULL,
PRIMARY KEY(Tipo_publicacion_codigo)
)
Go

CREATE TABLE PERSISTIENDO.Estado_Publicacion(
Estado_Publicacion_codigo int IDENTITY(1,1) NOT NULL,
Estado_Publicacion_descripcion nvarchar(255) NOT NULL,
PRIMARY KEY(Estado_Publicacion_codigo)
)

CREATE TABLE PERSISTIENDO.Rubro(
Rubro_codigo INT IDENTITY (1,1) NOT NULL,
Rubro_descripcion nvarchar(255) NOT NULL,
PRIMARY KEY (Rubro_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Empresa(
Empresa_username nvarchar(30) NOT NULL,
Empresa_razon_social nvarchar(255) NOT NULL,
Empresa_cuil nvarchar(50) NOT NULL,
Empresa_fecha_creacion datetime NOT NULL,
Empresa_mail nvarchar(50) NOT NULL,
Empresa_dom_calle nvarchar(100) NOT NULL,
Empresa_nro_calle numeric(18,0) NOT NULL,
Empresa_piso numeric(18,0),
Empresa_depto nvarchar(50),
Empresa_cod_postal nvarchar(50) NOT NULL,
Empresa_rubro int,
Empresa_nombre_contacto nvarchar(255),
Empresa_localidad nvarchar(255),
Empresa_ciudad nvarchar(255),
Empresa_telefono numeric(18,0),
PRIMARY KEY(Empresa_username),
CONSTRAINT fk_empresa_username FOREIGN KEY (Empresa_username) REFERENCES PERSISTIENDO.Usuario (Usuario_username),
CONSTRAINT fk_empresa_rubro FOREIGN KEY (Empresa_rubro) REFERENCES PERSISTIENDO.Rubro(Rubro_codigo) 
)
GO
CREATE TABLE PERSISTIENDO.Publicacion(
Publicacion_codigo numeric(18,0) NOT NULL,
Publicacion_descripcion nvarchar(255),
Publicacion_stock numeric(18,0) NOT NULL,
Publicacion_fecha datetime NOT NULL,
Publicacion_fecha_vencimiento datetime NOT NULL,
Publicacion_precio numeric(18,2) NOT NULL,
Publicacion_rubro int NOT NULL,
Publicacion_envio bit NOT NULL,
Publicacion_vendedor nvarchar(30) NOT NULL,
Publicacion_tipo int NOT NULL,
Publicacion_preguntas bit NOT NULL,
Publicacion_visibilidad numeric(18,0) NOT NULL,
Publicacion_estado int NOT NULL,
PRIMARY KEY( Publicacion_codigo),
CONSTRAINT fk_publicacion_username FOREIGN KEY (Publicacion_vendedor) REFERENCES PERSISTIENDO.Usuario (Usuario_username),
CONSTRAINT fk_publicacion_tipo FOREIGN KEY (Publicacion_tipo) REFERENCES PERSISTIENDO.Tipo_publicacion (Tipo_Publicacion_codigo),
CONSTRAINT fk_publicacion_estado FOREIGN KEY (Publicacion_estado) REFERENCES PERSISTIENDO.Estado_publicacion (Estado_Publicacion_codigo),
CONSTRAINT fk_publicacion_rubro FOREIGN KEY (Publicacion_rubro) REFERENCES PERSISTIENDO.Rubro(Rubro_codigo),
CONSTRAINT fk_publicacion_visibilidad FOREIGN KEY (Publicacion_visibilidad) REFERENCES PERSISTIENDO.Visibilidad (Visibilidad_cod)
)
GO

CREATE TABLE PERSISTIENDO.Oferta(
Oferta_codigo INT IDENTITY (1,1) NOT NULL,
Oferta_fecha datetime NOT NULL,
Oferta_monto numeric(18,2) NOT NULL,
Oferta_publicacion numeric(18,0) NOT NULL,
Oferta_ofertante nvarchar(30) NOT NULL,
Oferta_envio bit not null,
PRIMARY KEY(Oferta_codigo),
CONSTRAINT fk_oferta_ofertante FOREIGN KEY (Oferta_ofertante) REFERENCES PERSISTIENDO.Usuario(Usuario_username),
CONSTRAINT fk_oferta_publicaciones FOREIGN KEY (Oferta_publicacion) REFERENCES PERSISTIENDO.Publicacion(Publicacion_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Factura(
Factura_numero numeric(18,2) NOT NULL,
Factura_total numeric(18,2) NOT NULL,
Factura_fecha datetime NOT NULL,
Factura_forma_de_pago nvarchar(255) NOT NULL,
Factura_codigo_publicacion numeric(18) NOT NULL,
PRIMARY KEY(Factura_numero),
CONSTRAINT fk_factura_codigo_publicacion FOREIGN KEY (Factura_codigo_publicacion) REFERENCES PERSISTIENDO.Publicacion(Publicacion_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Item_Factura(
Item_Factura_codigo INT IDENTITY (1,1) NOT NULL,
Item_Factura_monto numeric(18,2) NOT NULL,
Item_Factura_cantidad numeric(18,2) NOT NULL,
Item_Factura_detalle nvarchar(255),
Item_Factura_codigo_factura numeric(18,2),
PRIMARY KEY (Item_Factura_codigo),
CONSTRAINT fk_item_factura_codigo_factura FOREIGN KEY (Item_Factura_codigo_factura) REFERENCES PERSISTIENDO.Factura
)
GO

CREATE TABLE PERSISTIENDO.Compra(
Compra_codigo INT IDENTITY (1,1) NOT NULL,
Compra_comprador nvarchar(30) NOT NULL,
Compra_codigo_publicacion numeric(18,0) NOT NULL,
Compra_cantidad numeric(18,0) NOT NULL,
Compra_fecha datetime NOT NULL,
PRIMARY KEY (Compra_codigo),
CONSTRAINT fk_compra_comprador FOREIGN KEY (Compra_comprador) REFERENCES PERSISTIENDO.Usuario(Usuario_username),
CONSTRAINT fk_compra_publicacion FOREIGN KEY (Compra_codigo_publicacion) REFERENCES PERSISTIENDO.Publicacion(Publicacion_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Calificacion(
Calificacion_codigo numeric(18,0) NOT NULL,
Calificacion_codigo_compra int not null,
Calificacion_cant_estrellas numeric(18,0) NOT NULL,
Calificacion_descripcion nvarchar(255),
PRIMARY KEY (Calificacion_codigo),
CONSTRAINT fk_calificacion_compra FOREIGN KEY (Calificacion_codigo_compra) REFERENCES PERSISTIENDO.Compra(Compra_codigo)
)
GO

CREATE TABLE PERSISTIENDO.Preguntas(
Preguntas_codigo INT IDENTITY(1,1) NOT NULL,
Preguntas_detalle nvarchar(255) NOT NULL,
Preguntas_codigo_publicacion numeric(18,0) NOT NULL,
PRIMARY KEY (Preguntas_codigo),
CONSTRAINT fk_preguntas_publicacion FOREIGN KEY(Preguntas_codigo_publicacion) REFERENCES PERSISTIENDO.Publicacion(Publicacion_codigo)
)
GO

INSERT INTO PERSISTIENDO.Rubro (Rubro_descripcion)
SELECT DISTINCT Publicacion_Rubro_Descripcion from GD1C2016.gd_esquema.Maestra

Insert into PERSISTIENDO.Visibilidad (Visibilidad_cod,Visibilidad_descripcion,Visibilidad_precio,Visibilidad_porcentaje,Visibilidad_precio_envio)
Select Distinct Publicacion_Visibilidad_Cod,Publicacion_Visibilidad_Desc,Publicacion_Visibilidad_Precio,Publicacion_Visibilidad_Porcentaje,200 from GD1C2016.gd_esquema.Maestra

Insert into PERSISTIENDO.Rol (Rol_habilitado,Rol_nombre) values (1,'Cliente')
Insert into PERSISTIENDO.Rol (Rol_habilitado,Rol_nombre) values (1,'Empresa')

Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('ABM ROL')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('ABM USUARIO')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('ABM RUBRO')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('ABM VISIBILIDAD DE PUBLICACION')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('PUBLICACIONES')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('COMPRAR/OFERTAR')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('HISTORIAL DE CLIENTES')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('CALIFICAR AL VENDEDOR')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('CONSULTA DE FACTURAS')
Insert into PERSISTIENDO.Funcionalidad (Func_nombre) values ('LISTADO ESTADISTICO')


INSERT INTO PERSISTIENDO.Usuario (Usuario_username, Usuario_password, Usuario_nuevo,Usuario_habilitado,Usuario_administrador,Usuario_intentos)
SELECT DISTINCT CAST(Cli_Dni as nvarchar(30)), HASHBYTES('SHA','1234'), 0,1,0,0
FROM GD1C2016.gd_esquema.Maestra
where Cli_Dni IS NOT NULL

INSERT INTO PERSISTIENDO.Usuario (Usuario_username, Usuario_password, Usuario_nuevo,Usuario_habilitado,Usuario_administrador,Usuario_intentos)
SELECT DISTINCT CAST(Publ_Cli_Dni as nvarchar(30)), HASHBYTES('SHA','1234'), 0,1,0,0
FROM GD1C2016.gd_esquema.Maestra
where Publ_Cli_Dni IS NOT NULL AND (Publ_Cli_Dni NOT IN (select Usuario_username from PERSISTIENDO.Usuario))

INSERT INTO PERSISTIENDO.Usuario (Usuario_username, Usuario_password, Usuario_nuevo,Usuario_habilitado,Usuario_administrador,Usuario_intentos)
SELECT DISTINCT CAST(Publ_Empresa_Cuit as nvarchar(30)), HASHBYTES('SHA','1234'),0,1,0,0
from GD1C2016.gd_esquema.Maestra
where Publ_Empresa_Cuit IS NOT NULL

INSERT INTO PERSISTIENDO.Empresa (Empresa_cod_postal,Empresa_cuil,Empresa_depto,Empresa_dom_calle,Empresa_fecha_creacion,Empresa_mail,Empresa_nro_calle,Empresa_piso,Empresa_razon_social,Empresa_username)
SELECT DISTINCT Publ_Empresa_Cod_Postal,Publ_Empresa_Cuit,Publ_Empresa_Depto,Publ_Empresa_Dom_Calle,Publ_Empresa_Fecha_Creacion,Publ_Empresa_Mail,Publ_Empresa_Nro_Calle,Publ_Empresa_Piso,Publ_Empresa_Razon_Social, Usuario_username
from GD1C2016.gd_esquema.Maestra, PERSISTIENDO.Usuario
WHERE Publ_Empresa_Cuit IS NOT NULL and Usuario_username = CAST(Publ_Empresa_Cuit as nvarchar(30))

INSERT INTO PERSISTIENDO.Cliente (Cliente_apellido, Cliente_nombre, Cliente_codigo_postal, Cliente_depto, Cliente_dni, Cliente_dom_calle, Cliente_fecha_nacimiento, Cliente_mail, Cliente_piso, Cliente_nro_calle, Cliente_Username,Cliente_tipo_documento)
SELECT DISTINCT Cli_Apeliido, Cli_Nombre,Cli_Cod_Postal,Cli_Depto, Cli_Dni, Cli_Dom_Calle, Cli_Fecha_Nac, Cli_Mail, Cli_Piso, Cli_Nro_Calle, Usuario_username,'DNI'
from GD1C2016.gd_esquema.Maestra, PERSISTIENDO.Usuario
where Cli_Dni IS NOT NULL and Usuario_username = CAST(Cli_Dni as nvarchar(30))

INSERT INTO	PERSISTIENDO.Estado_Publicacion(Estado_Publicacion_descripcion) values('Activa')
INSERT INTO	PERSISTIENDO.Estado_Publicacion(Estado_Publicacion_descripcion) values('Pausada')
INSERT INTO	PERSISTIENDO.Estado_Publicacion(Estado_Publicacion_descripcion) values('Borrador')
INSERT INTO	PERSISTIENDO.Estado_Publicacion(Estado_Publicacion_descripcion) values('Finalizada')

INSERT INTO	PERSISTIENDO.Tipo_publicacion (Tipo_publicacion_descripcion,Tipo_publicacion_envio_habilitado) values ('Subasta',1)
INSERT INTO	PERSISTIENDO.Tipo_publicacion (Tipo_publicacion_descripcion,Tipo_publicacion_envio_habilitado) values('Compra Inmediata',1)

INSERT INTO PERSISTIENDO.Publicacion(Publicacion_codigo,Publicacion_descripcion,Publicacion_envio,Publicacion_estado,Publicacion_fecha,Publicacion_fecha_vencimiento,Publicacion_precio,Publicacion_rubro,Publicacion_stock,Publicacion_vendedor,Publicacion_visibilidad,Publicacion_tipo,Publicacion_preguntas)
Select distinct Publicacion_Cod,Publicacion_Descripcion,1,1,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,(Select Rubro_codigo from PERSISTIENDO.Rubro where Rubro_descripcion=Publicacion_Rubro_Descripcion),Publicacion_Stock,CAST(Publ_Cli_Dni as nvarchar(30)),(Select Visibilidad_cod from PERSISTIENDO.Visibilidad Where Visibilidad_descripcion=Publicacion_Visibilidad_Desc),(Select Tipo_publicacion_codigo from PERSISTIENDO.Tipo_publicacion where Tipo_publicacion_descripcion=Publicacion_Tipo),1
From GD1C2016.gd_esquema.Maestra 
where Publ_Cli_Dni is not null

INSERT INTO PERSISTIENDO.Publicacion(Publicacion_codigo,Publicacion_descripcion,Publicacion_envio,Publicacion_estado,Publicacion_fecha,Publicacion_fecha_vencimiento,Publicacion_precio,Publicacion_rubro,Publicacion_stock,Publicacion_vendedor,Publicacion_visibilidad,Publicacion_tipo,Publicacion_preguntas)
Select distinct Publicacion_Cod,Publicacion_Descripcion,1,1,Publicacion_Fecha,Publicacion_Fecha_Venc,Publicacion_Precio,(Select Rubro_codigo from PERSISTIENDO.Rubro where Rubro_descripcion=Publicacion_Rubro_Descripcion),Publicacion_Stock,CAST(Publ_Empresa_Cuit as nvarchar(30)),(Select Visibilidad_cod from PERSISTIENDO.Visibilidad Where Visibilidad_descripcion=Publicacion_Visibilidad_Desc),(Select Tipo_publicacion_codigo from PERSISTIENDO.Tipo_publicacion where Tipo_publicacion_descripcion=Publicacion_Tipo),1 
From GD1C2016.gd_esquema.Maestra 
where Publ_Empresa_Cuit is not null And Publicacion_Tipo is not null

Insert into PERSISTIENDO.Factura(Factura_numero,Factura_total,Factura_fecha,Factura_forma_de_pago,Factura_codigo_publicacion)
Select distinct Factura_Nro,Factura_Total,Factura_Fecha,Forma_Pago_Desc,Publicacion_Cod
From GD1C2016.gd_esquema.Maestra
Where Factura_Nro is not null

Insert into PERSISTIENDO.Item_Factura(Item_Factura_monto,Item_Factura_cantidad,Item_factura_codigo_factura)
select Item_Factura_Monto,Item_Factura_Cantidad,Factura_Nro
From GD1C2016.gd_esquema.Maestra
Where Factura_Nro is not null


Insert into PERSISTIENDO.Compra(Compra_comprador,Compra_codigo_publicacion,Compra_fecha,Compra_cantidad)
select distinct Cast(Cli_Dni as nvarchar(30)),Publicacion_Cod,Compra_Fecha,Compra_Cantidad
From GD1C2016.gd_esquema.Maestra
Where Compra_Fecha is not null

Insert into PERSISTIENDO.Calificacion (Calificacion_codigo,Calificacion_cant_estrellas,Calificacion_descripcion,Calificacion_codigo_compra)
select distinct m.Calificacion_Codigo,m.Calificacion_Cant_Estrellas,m.Calificacion_Descripcion,c.Compra_codigo
From GD1C2016.gd_esquema.Maestra as m,PERSISTIENDO.Compra as c
where Calificacion_Codigo is not null and Compra_comprador =cast(Cli_Dni as varchar) 
and Compra_codigo_publicacion = Publicacion_Cod
and c.Compra_fecha = m.Compra_Fecha
and c.Compra_cantidad = m.Compra_Cantidad

Insert into PERSISTIENDO.Oferta(Oferta_fecha,Oferta_monto,Oferta_ofertante,Oferta_publicacion,Oferta_envio)
select distinct Oferta_Fecha,Oferta_Monto,Cast(Cli_Dni as nvarchar(30)),Publicacion_Cod,0
From GD1C2016.gd_esquema.Maestra
Where Oferta_Fecha is not null

Insert into PERSISTIENDO.Usuario(Usuario_username,Usuario_password,Usuario_nuevo,Usuario_habilitado,Usuario_administrador,Usuario_intentos)
values ('admin',HASHBYTES('SHA', 'w23e'),0,1,1,0)


Insert into PERSISTIENDO.Rol_Por_Usuario(RPU_username,RPU_codigo_rol)
Select distinct Cliente_dni,(select Rol.Rol_codigo From PERSISTIENDO.Rol Where Rol.Rol_nombre='Cliente')
From PERSISTIENDO.Cliente

Insert into PERSISTIENDO.Rol_Por_Usuario(RPU_username,RPU_codigo_rol)
Select distinct Empresa_cuil,(select Rol.Rol_codigo From PERSISTIENDO.Rol Where Rol.Rol_nombre='Empresa')
From PERSISTIENDO.Empresa


Insert into PERSISTIENDO.Funcionalidad_por_rol(FPR_codigo_func,FPR_codigo_rol) values(5,1)
Insert into PERSISTIENDO.Funcionalidad_por_rol(FPR_codigo_func,FPR_codigo_rol) values(6,1)
Insert into PERSISTIENDO.Funcionalidad_por_rol(FPR_codigo_func,FPR_codigo_rol) values(7,1)
Insert into PERSISTIENDO.Funcionalidad_por_rol(FPR_codigo_func,FPR_codigo_rol) values(8,1)

Insert into PERSISTIENDO.Funcionalidad_por_rol(FPR_codigo_func,FPR_codigo_rol) values(5,2)

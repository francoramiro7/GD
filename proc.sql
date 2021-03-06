USE [GD1C2016]
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[actualizarContra]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[actualizarContra] (@user nvarchar(30), @pass nvarchar(30))
as
begin
update PERSISTIENDO.Usuario 
set Usuario_password = (HASHBYTES('SHA',cast (@pass as varchar)))
where Usuario_username = @user
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[agregarIntentoFallidos]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[agregarIntentoFallidos] (@Username nvarchar(30))
as 
begin
update PERSISTIENDO.Usuario
set Usuario_intentos = (select Usuario_intentos from PERSISTIENDO.Usuario where Usuario_username = @Username) + 1
where Usuario_username = @Username
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[bloquearUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[bloquearUsuario] (@Username nvarchar(30))
as 
begin
update PERSISTIENDO.Usuario
set Usuario_habilitado = 0
where Usuario_username = @Username
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[borrarRoles]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[borrarRoles](@Username nvarchar(30))
as
begin
delete from PERSISTIENDO.Rol_Por_Usuario
where RPU_username = @Username
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[calificadas]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[calificadas](@Usuario nvarchar(30))
as
begin
select TOP 5 Publicacion_descripcion,Calificacion_cant_estrellas,Calificacion_descripcion
From PERSISTIENDO.Publicacion,PERSISTIENDO.Compra,PERSISTIENDO.Calificacion
where Compra.Compra_comprador = @Usuario And Calificacion_codigo_compra=Compra_codigo and Compra_codigo_publicacion = Publicacion_codigo
order by Calificacion_codigo desc
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[calificarCompra]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[calificarCompra](@Codigo numeric(18,0),@Compra int,@Estrellas numeric(18,0),@Comentario nvarchar(255))
AS
BEGIN
insert into PERSISTIENDO.Calificacion(Calificacion_codigo,Calificacion_codigo_compra,Calificacion_cant_estrellas,Calificacion_descripcion)
values (@Codigo,@Compra,@Estrellas,@Comentario)
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cantidadDeEstrellas]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[cantidadDeEstrellas](@Usuario nvarchar(30))
as
begin
declare @cant int
set @cant =( select SUM(Calificacion_cant_estrellas)
from PERSISTIENDO.Calificacion JOIN PERSISTIENDO.Compra on Compra_codigo = Calificacion_codigo_compra
where Compra_comprador = @Usuario)
return @cant
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cantidadOfertas]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[cantidadOfertas] (@Codigo numeric(18,0))
as
begin
declare @Stock float
set @Stock = (select  COUNT(*)
from PERSISTIENDO.Oferta 
where Oferta_publicacion = @Codigo)
return @Stock
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[CantidadRoles]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[CantidadRoles](@Username nvarchar(30))
as
begin
declare @cantidad int
set @cantidad = (select count (RPU_username) from PERSISTIENDO.Rol_Por_Usuario where RPU_username = @Username)
return @cantidad
end










GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarClientes]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[cargarClientes]
as
begin
select Cliente_dni, Cliente_apellido, Cliente_nombre, Cliente_mail
from PERSISTIENDO.Cliente
END








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarClientesEliminar]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[cargarClientesEliminar]
as
begin
select Cliente_dni, Cliente_apellido, Cliente_nombre, Cliente_mail
from PERSISTIENDO.Cliente join PERSISTIENDO.Usuario on Usuario_username = Cliente_Username
where Usuario.Usuario_habilitado = 1
end
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarEmpresas]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[cargarEmpresas]
as
begin
select Empresa_cuil, Empresa_razon_social, Empresa_mail
from PERSISTIENDO.Empresa
END








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarEmpresasEliminar]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[cargarEmpresasEliminar]
as
begin
select Empresa_cuil, Empresa_razon_social, Empresa_mail
from PERSISTIENDO.Empresa join PERSISTIENDO.Usuario on Usuario_username = Empresa_username
where Usuario_habilitado = 1
END
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarPublicaciones]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[cargarPublicaciones]
as
begin
select Publicacion_codigo,Publicacion_descripcion,Publicacion_precio,Publicacion_stock,Estado_Publicacion_descripcion
from PERSISTIENDO.Publicacion
join PERSISTIENDO.Estado_Publicacion ON PERSISTIENDO.Publicacion.Publicacion_estado = Estado_Publicacion_codigo
END








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarPublicacionesPorUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[cargarPublicacionesPorUsuario](@Username nvarchar(30))
as
begin
select Publicacion_codigo,Publicacion_descripcion,Publicacion_precio,Publicacion_stock,Estado_Publicacion_descripcion
from PERSISTIENDO.Publicacion
join PERSISTIENDO.Estado_Publicacion ON Publicacion_estado = Estado_Publicacion_codigo
where Publicacion.Publicacion_vendedor = @Username
END







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarRoles]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[cargarRoles] 
as
begin
select Rol_nombre from PERSISTIENDO.Rol
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarRolesHabilitados]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[cargarRolesHabilitados] 
as
begin
select Rol_nombre from PERSISTIENDO.Rol where Rol_habilitado = 1
end

GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoEstado]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoEstado] (@Estado nvarchar(255))
as
begin
declare @codigo int
set @codigo = (select Estado_Publicacion_codigo from PERSISTIENDO.Estado_Publicacion where Estado_Publicacion_descripcion = @Estado)
return @codigo
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoFuncionalidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoFuncionalidad] (@nombre nvarchar(30))
as
begin
declare @cod int
set @cod = (select Func_codigo from PERSISTIENDO.Funcionalidad where Func_nombre = @nombre)
return @cod
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoRol] (@nombre nvarchar(30))
as
begin
declare @cod int
set @cod = (select Rol_codigo from PERSISTIENDO.rol where Rol_nombre = @nombre)
return @cod
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoRubro]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoRubro] (@Rubro nvarchar(255))
as
begin
declare @codigo int
set @codigo = (select Rubro_codigo from PERSISTIENDO.Rubro where Rubro_descripcion = @Rubro)
return @codigo
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoTipoPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoTipoPublicacion] (@Tipo nvarchar(255))
as
begin
declare @codigo int
set @codigo = (select Tipo_publicacion_codigo from PERSISTIENDO.Tipo_publicacion where Tipo_publicacion_descripcion = @Tipo)
return @codigo
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[codigoVisibilidad] (@Visibilidad nvarchar(255))
as
begin
declare @codigo int
set @codigo = (select Visibilidad_cod from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @Visibilidad)
return @codigo
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[comprasRealizadas]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[comprasRealizadas](@Usuario nvarchar(30))
as
begin
declare @cant int
set @cant =( select Count(*)
from PERSISTIENDO.Compra
where Compra_comprador = @Usuario)
return @cant
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[costoVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[costoVisibilidad](@Detalle nvarchar)
as
begin
select Visibilidad_precio,Visibilidad_precio_envio
from PERSISTIENDO.Visibilidad
where Visibilidad_descripcion = @Detalle 
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearCliente]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[crearCliente] (@Username nvarchar(30), @apellido nvarchar(255), @nombre nvarchar(255), @mail nvarchar(255), 
@calle nvarchar(255), @cp nvarchar(50), @depto nvarchar(50), @piso numeric(18,0), @dni numeric(18,0), @tipo nvarchar(255), @nro numeric(18,0),
@fechanac datetime, @fechacreac datetime, @localidad nvarchar(255))
as
begin
insert into PERSISTIENDO.Cliente (Cliente_apellido, Cliente_codigo_postal, Cliente_depto, Cliente_dni, Cliente_dom_calle, Cliente_fecha_nacimiento, 
Cliente_feche_creacion, Cliente_localidad, Cliente_mail, Cliente_nombre, Cliente_nro_calle, Cliente_piso, Cliente_tipo_documento, Cliente_Username)
values (@apellido, @cp, @depto, @dni, @calle, @fechanac, @fechacreac, @localidad, @mail, @nombre, @nro, @piso, @tipo, @Username)
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearEmpresa]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[crearEmpresa] (@user nvarchar(30), @razon nvarchar(255), @cuit nvarchar(50), @creacion datetime, @mail nvarchar(50),
@calle nvarchar(100), @num numeric(18,0), @piso numeric(18,0), @depto nvarchar(50), @cp nvarchar(50), @rubro int, @contacto nvarchar(255), 
@localidad nvarchar(255), @ciudad nvarchar(255), @tel numeric(18,0))
as
begin
insert into PERSISTIENDO.Empresa (Empresa_ciudad,Empresa_cod_postal,Empresa_cuil,Empresa_depto,Empresa_dom_calle,
Empresa_fecha_creacion, Empresa_localidad, Empresa_mail, Empresa_nombre_contacto, Empresa_nro_calle, Empresa_piso, Empresa_razon_social,
Empresa_rubro, Empresa_telefono,Empresa_username)
values (@ciudad,@cp,@cuit,@depto,@calle,@creacion,@localidad,@mail,@contacto,@num,@piso,@razon,@rubro,@tel,@user)
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearFuncionalidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[crearFuncionalidad] (@codigoRol int, @codigoFunc int)
as
begin
insert into PERSISTIENDO.Funcionalidad_por_rol (FPR_codigo_rol, FPR_codigo_func)
values(@codigoRol, @codigoFunc)
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[crearPublicacion](@CodigoPublicacion numeric(18,0),@Descripcion nvarchar(255), @Stock numeric(18,0), @Fecha datetime, @Venci datetime, @Precio numeric(18,2),
@Rubro int, @Envio bit, @Vendedor nvarchar(30), @Tipo int, @Preguntas bit, @Visibilidad numeric(18,0), @Estado int)
AS
BEGIN
insert into PERSISTIENDO.Publicacion (Publicacion_codigo,Publicacion_descripcion, Publicacion_stock, Publicacion_fecha, Publicacion_fecha_vencimiento, Publicacion_precio,
Publicacion_rubro, Publicacion_envio, Publicacion_vendedor, Publicacion_tipo, Publicacion_preguntas, Publicacion_visibilidad, Publicacion_estado)
values (@CodigoPublicacion,@Descripcion, @Stock, @Fecha,@Venci,@Precio,@Rubro,@Envio,@Vendedor,@Tipo,@Preguntas,@Visibilidad,@Estado)
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[crearRol] (@Username nvarchar(30), @rol int)
as
begin
insert into PERSISTIENDO.Rol_Por_Usuario (RPU_codigo_rol, RPU_username)
values (@rol, @Username)
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearRolNuevo]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[crearRolNuevo] (@nombre nvarchar(30))
as
begin
insert into PERSISTIENDO.Rol(Rol_nombre, Rol_habilitado)
values(@nombre,1)
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[crearUsuario] (@Username nvarchar(30), @Password nvarchar(30))
as
begin
insert into PERSISTIENDO.Usuario (Usuario_username, Usuario_password, Usuario_habilitado, Usuario_nuevo, Usuario_administrador, Usuario_intentos)
values (@Username, HASHBYTES('SHA',cast (@Password as varchar)),1,1,0,0)
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[cuantasPorCalificar]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[cuantasPorCalificar](@Usuario nvarchar(30))
as
begin
select Count(Compra_codigo)
From PERSISTIENDO.Publicacion,PERSISTIENDO.Compra
where Compra_comprador = @Usuario And (select count(*) from PERSISTIENDO.Calificacion Where Calificacion_codigo_compra =Compra_codigo)=0 and Compra_codigo_publicacion = Publicacion_codigo
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[datosEmpresa]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[datosEmpresa] (@cuit nvarchar(50))
as
begin
select Empresa_ciudad, Empresa_cod_postal, Empresa_depto, Empresa_dom_calle, Empresa_localidad, Empresa_mail, Empresa_nombre_contacto, Empresa_nro_calle,
Empresa_piso, Empresa_rubro, Empresa_telefono, Empresa_username
from PERSISTIENDO.Empresa 
where Empresa_cuil = @cuit
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[datosPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[datosPublicacion](@Codigo numeric(18,0))
as
begin
select  Publicacion_preguntas,Publicacion_envio,Rubro_descripcion,Visibilidad_descripcion,Tipo_publicacion_descripcion,Estado_Publicacion_descripcion,Publicacion_vendedor
from PERSISTIENDO.Publicacion 
JOIN PERSISTIENDO.Rubro on Rubro_codigo = Publicacion_rubro 
JOIN PERSISTIENDO.Visibilidad on Visibilidad_cod = Publicacion_visibilidad
JOIN PERSISTIENDO.Tipo_publicacion on Tipo_publicacion_codigo = Publicacion_tipo
JOIN PERSISTIENDO.Estado_Publicacion on Estado_Publicacion_codigo = Publicacion_estado
where Publicacion_codigo = @Codigo
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[datosString]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[datosString] (@dni numeric(18,0))
as
begin
select Cliente_codigo_postal, Cliente_depto, Cliente_dom_calle, Cliente_fecha_nacimiento, Cliente_localidad, Cliente_nro_calle, Cliente_piso, Cliente_tipo_documento, Cliente_Username
from PERSISTIENDO.Cliente
where Cliente_dni = @dni
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[datosVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[datosVisibilidad] (@nombre nvarchar(255))
as
begin
select Visibilidad_cod, Visibilidad_descripcion, Visibilidad_porcentaje, Visibilidad_precio, Visibilidad_precio_envio from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @nombre
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[desbloquearUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[desbloquearUsuario](@Username nvarchar(30))
as 
begin
update PERSISTIENDO.Usuario
set Usuario_habilitado = 1
where Usuario_username = @Username
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarCliente]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarCliente](@Username nvarchar(30))
as
begin
delete from PERSISTIENDO.Cliente
where Cliente_Username = @Username
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarEmpresa]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarEmpresa](@Username nvarchar(30))
as
begin
delete from PERSISTIENDO.Empresa
where Empresa_username = @Username
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarFuncionalidades]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarFuncionalidades] (@rol int)
as
begin
delete from PERSISTIENDO.Funcionalidad_por_rol
where FPR_codigo_rol = @rol
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [PERSISTIENDO].[eliminarRol] (@nombre nvarchar(30))
as
begin
delete from PERSISTIENDO.Rol
where Rol_nombre = @nombre
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarRPU]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarRPU](@Username nvarchar(30))
as
begin
delete from PERSISTIENDO.Rol_Por_Usuario
where RPU_username = @Username
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarUsuario](@Username nvarchar(30))
as
begin
delete from PERSISTIENDO.Usuario
where Usuario_username = @Username
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[eliminarVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[eliminarVisibilidad] (@codigo numeric(18,0))
as
begin
delete from PERSISTIENDO.Visibilidad
where Visibilidad_cod = @codigo
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[envioPorVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[envioPorVisibilidad](@Detalle nvarchar(255))
as
begin
declare @cantidad numeric(18,2)
set @cantidad = (select Visibilidad_precio_envio from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @Detalle )
return @cantidad
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[envioValido]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[envioValido] (@Tipo nvarchar(255))
as
begin
declare @res bit
set @res = (select Tipo_publicacion_envio_habilitado from PERSISTIENDO.Tipo_publicacion where Tipo_publicacion_descripcion = @Tipo)
return @res
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[esAdministrador]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[esAdministrador] (@Username nvarchar(30))
as
begin
declare @resultado bit
set @resultado = (select Usuario_administrador from PERSISTIENDO.Usuario where Usuario_username = @Username)
return @resultado
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[estaBloqueado]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[estaBloqueado] (@Username nvarchar(30))
as 
begin
declare @bloqueado bit
set @bloqueado = (select Usuario_habilitado from PERSISTIENDO.Usuario where Usuario_username = @Username)
return @bloqueado
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeCuit]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[existeCuit] (@Cuit nvarchar(50))
as
begin
declare @resultado int
set @resultado = (select count (*) from PERSISTIENDO.Empresa where Empresa_cuil = @Cuit)
return @resultado
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeDni]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeDni] (@Dni numeric(18,0))
as
begin
declare @resultado int
set @resultado = (select count (Cliente_dni) from PERSISTIENDO.Cliente where Cliente_dni = @Dni)
return @resultado
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeRazon]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeRazon] (@Razon nvarchar(255))
as
begin
declare @resultado int
set @resultado = (select count (Empresa_razon_social) from PERSISTIENDO.Empresa where Empresa_razon_social = @Razon)
return @resultado
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeRol] (@nombre nvarchar(30))
as
begin
declare @existe int
set @existe = (select count(Rol_codigo) from PERSISTIENDO.Rol where Rol_nombre = @nombre) 
return @existe
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeUsuario] (@Username nvarchar(30))
as
begin
declare @resultado int
set @resultado = (select count (Usuario_username) from PERSISTIENDO.Usuario where Usuario_username = @Username)
return @resultado
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeVisibilidad] (@nombre nvarchar(255))
as
begin
declare @existe int
set @existe = (select count (*) from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @nombre)
return @existe
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[facturaPorPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[facturaPorPublicacion](@Codigo numeric(18,0))
as
begin
declare @valor numeric(18,2)
set @valor = (select Factura_numero from PERSISTIENDO.Factura where Factura_codigo_publicacion = @Codigo)
return @valor
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[facturarPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[facturarPublicacion](@CodigoPublicacion numeric(18,0),@CodigoFactura numeric(18,2),
@Precio numeric(18,2), @Fecha datetime,@Pago nvarchar(255))
AS
BEGIN
insert into PERSISTIENDO.Factura(Factura_codigo_publicacion,Factura_numero,Factura_total,Factura_fecha,Factura_forma_de_pago)
values (@CodigoPublicacion,@CodigoFactura, @Precio, @Fecha,@Pago)
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[FuncionalidadesPorRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[FuncionalidadesPorRol] (@Rol nvarchar(30))
as
begin
select Func_nombre from PERSISTIENDO.Funcionalidad_por_rol join PERSISTIENDO.Rol ON FPR_codigo_rol = Rol_codigo join PERSISTIENDO.Funcionalidad on
Func_codigo = FPR_codigo_func
where Rol_nombre = @Rol
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[getPublicaciones]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[getPublicaciones]
as
begin
select Publicacion_codigo,Publicacion_descripcion,Publicacion_precio,Rubro_descripcion,Tipo_publicacion_descripcion
from PERSISTIENDO.Publicacion
JOIN PERSISTIENDO.Visibilidad ON Visibilidad_cod = Publicacion_visibilidad
JOIN PERSISTIENDO.Tipo_publicacion ON Tipo_publicacion_codigo = Publicacion_tipo
JOIN PERSISTIENDO.Rubro ON Rubro_codigo = Publicacion_rubro
JOIN PERSISTIENDO.Estado_Publicacion ON Estado_Publicacion_codigo = Publicacion_estado
JOIN PERSISTIENDO.Usuario ON Publicacion_vendedor = Usuario_username
Where (Estado_Publicacion_descripcion like 'Activa' or Estado_Publicacion_descripcion like 'Pausada') and Usuario_habilitado=1
Order by Visibilidad_precio desc,Publicacion_precio desc
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[habilitarRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[habilitarRol] (@nombre nvarchar(30))
as
begin
update PERSISTIENDO.Rol
set Rol_habilitado = 1
where Rol_nombre = @nombre
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[hayPublicacionConVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[hayPublicacionConVisibilidad] (@codigo numeric(18,0))
as
begin
declare @existe int
set @existe = (select count(*) from PERSISTIENDO.Publicacion where Publicacion_visibilidad = @codigo)
return @existe
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[inhabilitarRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[inhabilitarRol] (@codigo int)
as
begin
update PERSISTIENDO.Rol
set Rol_habilitado = 0
where Rol_codigo = @codigo
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[inhabilitarRolPorUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[inhabilitarRolPorUsuario] (@codigo int)
as
begin
delete from PERSISTIENDO.Rol_Por_Usuario
where RPU_codigo_rol = @codigo
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[intentosFallidos]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[intentosFallidos] (@Username nvarchar(30))
as 
begin
declare @intentos int
set @intentos = (select Usuario_intentos from PERSISTIENDO.Usuario where Usuario_username = @Username)
return @intentos
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[itemFactura]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[itemFactura](@CodigoFactura numeric(18,2),
@Precio numeric(18,2),@Detalle nvarchar(255),@Cantidad int)
AS
BEGIN
insert into PERSISTIENDO.Item_Factura(Item_Factura_codigo_factura,Item_Factura_monto,Item_Factura_detalle,Item_Factura_cantidad)
values (@CodigoFactura, @Precio, @Detalle,@Cantidad)
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[listarFuncionalidades]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[listarFuncionalidades]
as begin
select Func_nombre from PERSISTIENDO.Funcionalidad
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[listarRubros]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [PERSISTIENDO].[listarRubros] 
as
begin
select Rubro_descripcion from PERSISTIENDO.Rubro
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[modificarMontoFactura]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[modificarMontoFactura](@Numero numeric(18,2),@Monto numeric (18,2))
AS
BEGIN
UPDATE PERSISTIENDO.Factura
SET Factura_total=(@Monto + Factura_total)
WHERE Factura_numero = @Numero
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[modificarPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[modificarPublicacion](@CodigoPublicacion numeric(18,0),@Descripcion nvarchar(255), @Stock numeric(18,0),@Precio numeric(18,2),
@Rubro int, @Envio bit, @Tipo int, @Preguntas bit, @Visibilidad numeric(18,0), @Estado int)
AS
BEGIN
UPDATE PERSISTIENDO.Publicacion
SET Publicacion_descripcion=@Descripcion,Publicacion_stock = @Stock,Publicacion_precio=@Precio,Publicacion_rubro=@Rubro,
Publicacion_envio=@Envio,Publicacion_tipo=@Tipo,Publicacion_preguntas=@Preguntas,
Publicacion_visibilidad = @Visibilidad,Publicacion_estado=@Estado
WHERE Publicacion_codigo = @CodigoPublicacion
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[modificarRol]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[modificarRol] (@nombre nvarchar(30), @anterior nvarchar(30))
as
begin
update PERSISTIENDO.Rol set Rol_nombre = @nombre
where Rol_nombre = @anterior
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[modificarStockEstadoPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[modificarStockEstadoPublicacion](@CodigoPublicacion numeric(18,0),@Stock numeric(18,0),@Estado int)
AS
BEGIN
UPDATE PERSISTIENDO.Publicacion
SET Publicacion_stock = @Stock,Publicacion_estado=@Estado
WHERE Publicacion_codigo = @CodigoPublicacion
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[newCompra]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[newCompra] (@Codigo numeric(18,0),@Comprador nvarchar(30),@Fecha datetime,@Cant numeric(18,0))
AS
BEGIN
insert into PERSISTIENDO.Compra(Compra_codigo_publicacion,Compra_comprador,Compra_fecha,Compra_cantidad)
values (@Codigo, @Comprador,@Fecha,@Cant)
end



GO
/****** Object:  StoredProcedure [PERSISTIENDO].[newItemFactura]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[newItemFactura](@CodigoFactura numeric(18,2),
@Precio numeric(18,2),@Detalle nvarchar(255))
AS
BEGIN
insert into PERSISTIENDO.Item_Factura(Item_Factura_codigo_factura,Item_Factura_monto,Item_Factura_detalle,Item_Factura_cantidad)
values (@CodigoFactura, @Precio, @Detalle,1)
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[newVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [PERSISTIENDO].[newVisibilidad] (@cod numeric(18,0), @desc nvarchar(255), @precio numeric(18,2), @porc numeric(18,2), @envio numeric(18,2))
as
begin
insert into PERSISTIENDO.Visibilidad (Visibilidad_cod, Visibilidad_descripcion, Visibilidad_porcentaje, Visibilidad_precio, Visibilidad_precio_envio)
values(@cod, @desc, @porc, @precio, @envio)
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[Nombreroles]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[Nombreroles] (@Username nvarchar(30))
as
begin
select Rol_nombre
from PERSISTIENDO.Rol_Por_Usuario join PERSISTIENDO.Rol on RPU_codigo_rol = Rol_codigo
where RPU_username = @Username and Rol_habilitado = 1
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[nombreRubro]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [PERSISTIENDO].[nombreRubro] (@codigo int)
as
begin
select Rubro_descripcion from PERSISTIENDO.Rubro where Rubro_codigo = @codigo
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[nombreVisibilidades]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[nombreVisibilidades]
as
begin
select Visibilidad_descripcion
from PERSISTIENDO.Visibilidad
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ofertar]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[ofertar](@CodigoPublicacion numeric(18,0),@Fecha datetime,@Monto numeric(18,2),
@Envio bit, @Ofertante nvarchar(30))
AS
BEGIN
insert into PERSISTIENDO.Oferta(Oferta_publicacion,Oferta_fecha,Oferta_monto,Oferta_envio,Oferta_ofertante)
values (@CodigoPublicacion,@Fecha,@Monto,@Envio,@Ofertante)
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ofertarYCompras]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[ofertarYCompras](@Usuario nvarchar(30))
as
begin
select Compra_codigo as Codigo, Tipo_publicacion_descripcion,Publicacion_descripcion,Publicacion_precio as Monto
from PERSISTIENDO.Publicacion 
JOIN PERSISTIENDO.Compra on Compra_codigo_publicacion = Publicacion_codigo 
JOIN PERSISTIENDO.Tipo_publicacion on Tipo_publicacion_codigo = Publicacion_tipo
where Compra_comprador = @Usuario
union all
select Oferta_codigo as Codigo,Tipo_publicacion_descripcion,Publicacion_descripcion,Oferta_monto as Monto
from PERSISTIENDO.Publicacion
JOIN PERSISTIENDO.Tipo_publicacion on Tipo_publicacion_codigo = Publicacion_tipo
JOIN PERSISTIENDO.Oferta on Oferta_publicacion = Publicacion_codigo
where Oferta_ofertante = @Usuario
order by Codigo desc
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[porCalificar]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[porCalificar](@Usuario nvarchar(30))
as
begin
select Compra_codigo,Publicacion_descripcion,Publicacion_precio
From PERSISTIENDO.Publicacion,PERSISTIENDO.Compra
where Compra_comprador = @Usuario And (select count(*) from PERSISTIENDO.Calificacion Where Calificacion_codigo_compra =Compra_codigo)=0 and Compra_codigo_publicacion = Publicacion_codigo
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[porcentajeVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[porcentajeVisibilidad] (@Visibilidad nvarchar(255))
as
begin
select Visibilidad_porcentaje,Visibilidad_precio_envio from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @Visibilidad
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[porcentajeVisibilidadCodigo]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[porcentajeVisibilidadCodigo] (@Visibilidad numeric(18,0))
as
begin
select Visibilidad_porcentaje,Visibilidad_precio_envio,Visibilidad_descripcion from PERSISTIENDO.Visibilidad where Visibilidad_cod = @Visibilidad
end

GO
/****** Object:  StoredProcedure [PERSISTIENDO].[precioPorVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[precioPorVisibilidad](@Detalle nvarchar(255))
as
begin
declare @cantidad numeric(18,2)
set @cantidad = (select Visibilidad_precio from PERSISTIENDO.Visibilidad where Visibilidad_descripcion = @Detalle )
return @cantidad
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[resetearIntentoFallidos]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[resetearIntentoFallidos] (@Username nvarchar(30))
as 
begin
update PERSISTIENDO.Usuario
set Usuario_intentos = 0
where Usuario_username = @Username
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[rolHabilitado]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [PERSISTIENDO].[rolHabilitado] (@nombre nvarchar(30))
as
begin
declare @resultado bit
set @resultado = (select Rol_habilitado from PERSISTIENDO.Rol where Rol_nombre = @nombre)
return @resultado
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[rolPorUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[rolPorUsuario](@Username nvarchar(30))
as
begin
select Rol_nombre from PERSISTIENDO.Rol_Por_Usuario join PERSISTIENDO.Rol ON RPU_codigo_rol = Rol_codigo
where RPU_username = @Username
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[stockPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[stockPublicacion](@Codigo numeric(18,0))
as
begin
declare @Stock int
set @Stock = (select  Publicacion_stock
from PERSISTIENDO.Publicacion 
where Publicacion_codigo = @Codigo)
return @Stock
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[tieneFactura]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [PERSISTIENDO].[tieneFactura](@CodigoPublicacion numeric(18,0))
AS
BEGIN
declare @Cantidad int
set @Cantidad = (select count(*) from PERSISTIENDO.Factura where Factura_codigo_publicacion = @CodigoPublicacion)
return @Cantidad
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaCalificacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[ultimaCalificacion]
AS
BEGIN
select MAX(Calificacion_codigo)
From PERSISTIENDO.Calificacion
end






GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaFactura]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[ultimaFactura]
as
begin
declare @valor numeric(18,2)
set @valor = (select MAX(Factura_numero) from PERSISTIENDO.Factura)
return @valor
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaOferta]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[ultimaOferta] (@Codigo numeric(18,0))
as
begin
select  Max(Oferta_monto)
from PERSISTIENDO.Oferta 
where Oferta_publicacion = @Codigo
end







GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaPublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[ultimaPublicacion]
as
begin
declare @valor numeric(18,0)
set @valor = (select MAX(Publicacion_codigo) from PERSISTIENDO.Publicacion)
return @valor
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[ultimaVisibilidad]
as
begin
declare @valor numeric(18,0)
set @valor = (select MAX(Visibilidad_cod) from PERSISTIENDO.Visibilidad)
return @valor
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[updateCliente]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[updateCliente] (@Username nvarchar(30), @apellido nvarchar(255), @nombre nvarchar(255), @mail nvarchar(255), 
@calle nvarchar(255), @cp nvarchar(50), @depto nvarchar(50), @piso numeric(18,0), @dni numeric(18,0), @tipo nvarchar(255), @nro numeric(18,0),
@fechanac datetime, @localidad nvarchar(255))
as
begin
update PERSISTIENDO.Cliente 
SET Cliente_apellido = @apellido, Cliente_nombre = @nombre, Cliente_mail = @mail, Cliente_dom_calle = @calle, Cliente_codigo_postal = @cp,
Cliente_depto = @depto, Cliente_piso = @piso, Cliente_dni = @dni, Cliente_tipo_documento = @tipo, Cliente_nro_calle = @nro, Cliente_fecha_nacimiento = @fechanac,
Cliente_localidad = @localidad
where Cliente_Username = @Username
end








GO
/****** Object:  StoredProcedure [PERSISTIENDO].[updateEmpresa]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[updateEmpresa] (@Username nvarchar(30), @razon nvarchar(255), @cuil nvarchar(50), @mail nvarchar(50), 
@calle nvarchar(100), @cp nvarchar(50), @depto nvarchar(50), @piso numeric(18,0), @rubro int, @nro numeric(18,0),
@localidad nvarchar(255), @ciudad nvarchar(255), @tel numeric(18,0), @contacto nvarchar(255))
as
begin
update PERSISTIENDO.Empresa 
SET Empresa_ciudad = @ciudad, Empresa_cod_postal = @cp, Empresa_cuil = @cuil, Empresa_depto = @depto, Empresa_dom_calle = @calle, Empresa_localidad = @localidad,
Empresa_mail = @mail, Empresa_nombre_contacto = @contacto, Empresa_nro_calle = @nro, Empresa_piso = @piso, Empresa_razon_social = @razon,
Empresa_rubro = @rubro, Empresa_telefono = @tel
where Empresa_username = @Username
end





GO
/****** Object:  StoredProcedure [PERSISTIENDO].[updateVisibilidad]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[updateVisibilidad] (@codigo numeric(18,0), @desc nvarchar(255), @precio numeric(18,2), @porc numeric(18,2), @envio numeric(18,2))
as
begin
update PERSISTIENDO.Visibilidad
set Visibilidad_descripcion = @desc, Visibilidad_porcentaje = @porc, Visibilidad_precio = @precio, Visibilidad_precio_envio = @envio
where Visibilidad_cod = @codigo
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[usernameCliente]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [PERSISTIENDO].[usernameCliente](@dni numeric(18,0))
as
begin
select Cliente_Username from PERSISTIENDO.Cliente where Cliente_dni = @dni
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[usernameEmpresa]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[usernameEmpresa] (@cuit nvarchar(50))
as
begin
select Empresa_username from PERSISTIENDO.Empresa where Empresa_cuil = @cuit
end




GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarContra]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[ValidarContra] (@Username nvarchar(30), @Password nvarchar(30))
as
begin
declare @Resultado int
declare @pre varbinary(100)
set @pre = HASHBYTES('SHA',cast(@Password as varchar))
set @Resultado =CAST(
   CASE WHEN EXISTS(SELECT Usuario_username FROM PERSISTIENDO.Usuario where Usuario_username like @Username and Usuario_password like @pre)
    THEN 1 
   ELSE 0 
   END 
AS int)
return @Resultado
end









GO
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarUsuario]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[ValidarUsuario] (@Username nvarchar(30))
as
begin
declare @Resultado int

set @Resultado =CAST(
   CASE WHEN EXISTS(SELECT Usuario_username FROM PERSISTIENDO.Usuario where Usuario_username like @Username)
    THEN 1 
   ELSE 0 
   END 
AS int)
return @Resultado
end











GO
/****** Object:  StoredProcedure [PERSISTIENDO].[vencePublicacion]    Script Date: 26/06/2016 19:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[vencePublicacion] (@Codigo numeric(18,0))
as
begin
update PERSISTIENDO.Publicacion
set Publicacion_estado = 4
where Publicacion_codigo = @Codigo
end

GO

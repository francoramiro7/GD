USE [GD1C2016]
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[agregarIntentoFallidos]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[bloquearUsuario]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[CantidadRoles]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarClientes]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarEmpresas]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarPublicaciones]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarPublicacionesPorUsuario]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoEstado]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoRol]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoRubro]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoTipoPublicacion]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoVisibilidad]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[costoVisibilidad]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearCliente]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearEmpresa]    Script Date: 16/06/2016 18:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[crearEmpresa] (@user nvarchar(30), @razon nvarchar(255), @cuit nvarchar(50), @creacion datetime, @mail nvarchar(50),
@calle nvarchar(100), @num numeric(18,0), @piso numeric(18,0), @depto nvarchar(50), @cp numeric(18,0), @rubro int, @contacto nvarchar(255), 
@localidad nvarchar(255), @ciudad nvarchar(255), @tel numeric(18,0))
as
begin
insert into PERSISTIENDO.Empresa (Empresa_ciudad,Empresa_cod_postal,Empresa_cuil,Empresa_depto,Empresa_dom_calle,
Empresa_fecha_creacion, Empresa_localidad, Empresa_mail, Empresa_nombre_contacto, Empresa_nro_calle, Empresa_piso, Empresa_razon_social,
Empresa_rubro, Empresa_telefono,Empresa_username)
values (@ciudad,@cp,@cuit,@depto,@calle,@creacion,@localidad,@mail,@contacto,@num,@piso,@razon,@rubro,@tel,@user)
end


GO
/****** Object:  StoredProcedure [PERSISTIENDO].[crearFuncionalidad]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearPublicacion]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearRol]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearRolNuevo]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearUsuario]    Script Date: 16/06/2016 18:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[crearUsuario] (@Username nvarchar(30), @Password nvarchar(30))
as
begin
insert into PERSISTIENDO.Usuario (Usuario_username, Usuario_password, Usuario_habilitado, Usuario_nuevo, Usuario_administrador, Usuario_intentos)
values (@Username, HASHBYTES('SHA',@Password),1,1,0,0)
end
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[envioPorVisibilidad]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[envioValido]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[esAdministrador]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[estaBloqueado]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeCuit]    Script Date: 16/06/2016 18:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[existeCuit] (@Cuit nvarchar(50))
as
begin
declare @resultado int
set @resultado = (select count (Empresa_cuil) from PERSISTIENDO.Empresa where Empresa_razon_social = @Cuit)
return @resultado
end


GO
/****** Object:  StoredProcedure [PERSISTIENDO].[existeDni]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeRazon]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeRol]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeUsuario]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[filter]    Script Date: 16/06/2016 18:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [PERSISTIENDO].[filter] (@nombre nvarchar(255), @apellido nvarchar(255), @mail nvarchar(255), @dni numeric(18,0))
as 
begin

select Cliente_dni, Cliente_apellido, Cliente_nombre ,Cliente_mail from PERSISTIENDO.Cliente
where Cliente_nombre = case
when (@nombre IS NULL OR @nombre = ' ') then '%' else @nombre end
and Cliente_apellido = case
when (@apellido IS NULL OR @apellido = ' ') then '%' else @apellido end
and Cliente_dni = case
when (@dni IS NULL OR @dni = ' ') then '%' else @dni end
and Cliente_mail = case
when (@mail IS NULL OR @mail = ' ') then '%' else @mail end
end



GO
/****** Object:  StoredProcedure [PERSISTIENDO].[FuncionalidadesPorRol]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[intentosFallidos]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[listarFuncionalidades]    Script Date: 16/06/2016 18:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[listarFuncionalidades]
as begin
select Func_nombre from PERSISTIENDO.Funcionalidad
end


GO
/****** Object:  StoredProcedure [PERSISTIENDO].[listarRubros]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[Nombreroles]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[nombreVisibilidades]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[precioPorVisibilidad]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[resetearIntentoFallidos]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaPublicacion]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarContra]    Script Date: 16/06/2016 18:41:11 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarUsuario]    Script Date: 16/06/2016 18:41:11 ******/
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

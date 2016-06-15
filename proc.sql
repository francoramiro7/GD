USE [GD1C2016]
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[agregarIntentoFallidos]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[bloquearUsuario]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[CantidadRoles]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarClientes]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[cargarEmpresas]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoEstado]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoRubro]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoTipoPublicacion]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[codigoVisibilidad]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[crearPublicacion]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[envioPorVisibilidad]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[envioValido]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[esAdministrador]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[estaBloqueado]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeCuit]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeDni]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeRazon]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[existeUsuario]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[intentosFallidos]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[listarFuncionalidades]    Script Date: 14/06/2016 23:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[listarFuncionalidades]
as begin
select Func_nombre from PERSISTIENDO.Funcionalidad
end
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[listarRubros]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[Nombreroles]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[nombreVisibilidades]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[precioPorVisibilidad]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[resetearIntentoFallidos]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ultimaPublicacion]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarContra]    Script Date: 14/06/2016 23:50:09 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarUsuario]    Script Date: 14/06/2016 23:50:09 ******/
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

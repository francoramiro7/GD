USE [GD1C2016]
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[agregarIntentoFallidos]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[bloquearUsuario]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[CantidadRoles]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[costoVisibilidad]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[esAdministrador]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[estaBloqueado]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[intentosFallidos]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[listarFuncionalidades]    Script Date: 12/06/2016 22:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PERSISTIENDO].[listarFuncionalidades]
as begin
select Func_nombre from PERSISTIENDO.Funcionalidad
end
GO
/****** Object:  StoredProcedure [PERSISTIENDO].[listarRubros]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[Nombreroles]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[nombreVisibilidades]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[resetearIntentoFallidos]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarContra]    Script Date: 12/06/2016 22:28:12 ******/
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
/****** Object:  StoredProcedure [PERSISTIENDO].[ValidarUsuario]    Script Date: 12/06/2016 22:28:12 ******/
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


-- poner numero de nomina
ALTER TABLE dbo.choferes alter column nomina FLOAT NOT NULL;
GO

-- llave primaria
ALTER TABLE Dbo.choferes ADD CONSTRAINT PK_CHOFERES PRIMARY KEY (NOMINA);
GO

-- esto tabla no sirve
ALTER TABLE dbo.servidor_enlazado
ADD CONSTRAINT PK_SERVIDOR_ENLAZADO PRIMARY KEY (CVE_SERVIDOR);
GO

-- oficinas
alter table dbo.oficina ADD activo bit default 1;
GO
update dbo.oficina set activo = 1
GO

-- empleados
alter table dbo.empleado ADD activo bit default 1;
GO
update dbo.empleado set activo = 1
GO

-- control de acceso
alter table dbo.control_acceso ADD activo bit default 1;
GO

-- quitarle el trigger
DROP TRIGGER [dbo].[tu_control_acceso]
GO

-- activarlo dependiendo de la habilitación del registro
UPDATE [dbo].[control_acceso] SET  [activo] = 1  WHERE habilitado = 1
GO

UPDATE [dbo].[control_acceso]  SET  [activo] = 0  WHERE habilitado = 0
GO

UPDATE control_acceso SET habilitado = 1
GO

-- zonas
alter table zona add habilitado bit default 1
GO
alter table zona add activo bit default 1
GO

update zona set habilitado =1, activo=1
GO


-- regiones
alter table region add habilitado bit default 1
GO
alter table region add activo bit default 1
GO

update region set habilitado =1, activo=1
GO

-- plazas
alter table plaza add habilitado bit default 1
GO
alter table plaza add activo bit default 1
GO


update plaza set habilitado =1, activo=1
GO

-- perfiles
alter table perfil add habilitado bit default 1
GO
alter table perfil add activo bit default 1
GO

update perfil set habilitado =1, activo=1
GO
 
-- permisos de perfiles
alter table perfil add  Permiso_Catalogo_Perfil bit default 0
GO
alter table perfil add  Permiso_Catalogo_Usuario bit default 0
GO
alter table perfil add  Permiso_Catalogo_Oficina bit default 0
GO
alter table perfil add  Permiso_Catalogo_Empleado bit default 0
GO
alter table perfil add  Permiso_Catalogo_Control_Acceso bit default 0
GO
alter table perfil add  Permiso_Proceso_Carga_Masiva_Empleado bit default 0
GO
alter table perfil add  Permiso_Proceso_Carga_Masiva_Plantilla bit default 0
GO
alter table perfil add  Permiso_Proceso_Plantilla bit default 0
GO
alter table perfil add  Permiso_Proceso_Empleado_En_Control bit default 0
GO
alter table perfil add  Permiso_Proceso_Carga_Plantilla_En_Control bit default 0
GO
alter table perfil add  Permiso_Proceso_Carga_Asistencia bit default 0
GO
alter table perfil add  Permiso_Proceso_Actualizar_Fecha_En_Control bit default 0
GO
alter table perfil add  Permiso_Reporte_Asistencia bit default 0
GO
alter table perfil add  Permiso_Reporte_Empleado_No_Registrado bit default 0
GO

-- todos los permisos al usaurio admin
update perfil set Permiso_Reporte_Empleado_No_Registrado=1, Permiso_Reporte_Asistencia=1,
Permiso_Proceso_Actualizar_Fecha_En_Control=1,Permiso_Proceso_Carga_Asistencia=1,Permiso_Proceso_Carga_Plantilla_En_Control=1,
Permiso_Proceso_Empleado_En_Control=1,Permiso_Proceso_Plantilla=1,Permiso_Proceso_Carga_Masiva_Plantilla=1,
Permiso_Proceso_Carga_Masiva_Empleado=1,Permiso_Catalogo_Control_Acceso=1,Permiso_Catalogo_Empleado=1,Permiso_Catalogo_Oficina=1,
Permiso_Catalogo_Usuario=1,Permiso_Catalogo_Perfil=1 WHERE nombre='administrador'
GO


-- columnas para la table de empleado
ALTER TABLE empleado ALTER COLUMN cve_area INT NULL
GO
ALTER TABLE empleado ALTER COLUMN cve_departamento INT NULL
GO
ALTER TABLE empleado ALTER COLUMN cve_plaza INT NULL
GO
ALTER TABLE empleado ALTER COLUMN cve_puesto INT NULL
GO
ALTER TABLE empleado ALTER COLUMN cve_tipo_empleado INT NULL
GO

-- columnas para la tabla de control de acceso
alter table control_acceso add cve_servidor int references control_acceso(cve_Control_acceso)
go
alter table control_acceso alter column cve_servidor int null
GO

-- columnas para la tabla de colector de movimientos
alter table colector_movimientos alter column cve_servidor int null;
GO


alter table incidencia alter column cve_usuario_alta int null;
GO


alter table plantilla add  cve_control_acceso int references control_acceso(cve_Control_acceso);
GO
update plantilla set cve_control_acceso = (Select cve_control_acceso from control_acceso where control_acceso.direccion_ip = plantilla.ip_control_template );
GO



WITH tblTemp as
(
SELECT ROW_NUMBER() Over(PARTITION BY cve_empleado, fecha_hora_incidencia, cve_control_acceso ORDER BY fecha_hora_incidencia)
   As RowNumber,* FROM incidencia
)
DELETE FROM tblTemp where RowNumber >1

CREATE UNIQUE INDEX incidencia_unique_ix ON INCIDENCIA (cve_empleado, fecha_hora_incidencia, cve_control_acceso, inoutmode)



 -- 14 de enero
 
 ALTER TABLE control_acceso add  fecha_ultima_conexion datetime;

 -- 27 de enero

DROP TABLE [dbo].[ci_sessions]
GO
 
DROP VIEW [dbo].[v_colector_movimientos]
GO

DROP TABLE [dbo].[empleado_control_acceso]
GO

ALTER TABLE [dbo].[control_acceso] DROP CONSTRAINT [FK_control_acceso_servidor]
GO

ALTER TABLE [dbo].[control_acceso] DROP COLUMN [cve_servidor]
GO

DROP TABLE [dbo].[servidor]
GO

DROP TABLE [dbo].[usuario_area]
GO

DROP TABLE [dbo].[usuario_departamento]
GO

DROP TRIGGER [dbo].[tu_empleado]
GO

DROP TABLE [dbo].[choferes]
GO

DROP TABLE [dbo].[empleado_huella]
GO

DROP TABLE [dbo].[menu]
GO

DROP TABLE [dbo].[menu_web]
GO

DROP TABLE [dbo].[parametros]
GO

DROP TABLE [dbo].[parametros_correo]
GO

DROP TABLE [dbo].[permiso]
GO

DROP TABLE [dbo].[servidor_enlazado]
GO

DROP TABLE [dbo].[tmp_carga_asistencia]
GO

DROP TABLE [dbo].[usuario_menu]
GO


CREATE TABLE [dbo].[colector_movimientos_incidencia](
	[id] [dbo].[primary_key] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[cve_control_acceso] [dbo].[primary_key] REFERENCES control_acceso(Cve_Control_Acceso) ,
	[fecha] datetime NOT NULL,
	[cve_empleado] varchar (255) ,
	[detalles] varchar(255) 
)


GO

create index colector_movs_inci_cve_emp_ix on colector_movimientos_incidencia(cve_empleado);
GO
create index colector_movs_inci_fecha_ix on colector_movimientos_incidencia(fecha);
GO



 ALTER TABLE empleado add  ultima_coleccion varchar(255);


DROP PROCEDURE [dbo].[spd_empleado_control_acceso]
GO
DROP PROCEDURE [dbo].[spi_colector_movimientos]
GO
DROP PROCEDURE [dbo].[spi_empleado_control_acceso]
GO
DROP PROCEDURE [dbo].[spiu_plantilla]
GO
DROP PROCEDURE [dbo].[spp_empleado_control_acceso]
GO
DROP PROCEDURE [dbo].[spp_procesa_movimientos]
GO
DROP PROCEDURE [dbo].[sps_col_usuario_notificacion]
GO
DROP PROCEDURE [dbo].[sps_controles_acceso_servidor]
GO
DROP PROCEDURE [dbo].[sps_controles_InterfazControl]
GO

DROP PROCEDURE [dbo].[sps_fecha_hora_actual]
GO

DROP PROCEDURE [dbo].[sps_incidencias_pendientes_enviar_a_ws]
GO

DROP PROCEDURE [dbo].[sps_parametros]
GO

DROP PROCEDURE [dbo].[sps_parametros_correo]
GO 

DROP PROCEDURE [dbo].[sps_plantilla_x_empleado]
GO

DROP PROCEDURE [dbo].[sps_plantilla_x_ip]
GO

DROP PROCEDURE [dbo].[sps_usuario]
GO

DROP PROCEDURE [dbo].[spu_incidencia_enviado_ws]
GO







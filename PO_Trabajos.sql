/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     29/6/2024 17:39:53                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('AE_SUBDIVISION') and o.name = 'FK_AE_SUBDI_FK_SECTOR_AE_SECTO')
alter table AE_SUBDIVISION
   drop constraint FK_AE_SUBDI_FK_SECTOR_AE_SECTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CIUDADES') and o.name = 'FK_CIUDADES_FK_PROVIN_PROVINCI')
alter table CIUDADES
   drop constraint FK_CIUDADES_FK_PROVIN_PROVINCI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPRESAS') and o.name = 'FK_EMPRESAS_FK_SECTOR_AE_SECTO')
alter table EMPRESAS
   drop constraint FK_EMPRESAS_FK_SECTOR_AE_SECTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_EMPRES_EMPRESAS')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_EMPRES_EMPRESAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_OFERTA_AREAS_LA')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_OFERTA_AREAS_LA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_OFERTA_CIUDADES')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_OFERTA_CIUDADES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_OFERTA_OFERTA_M')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_OFERTA_OFERTA_M
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_OFERTA_PUESTOS_')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_OFERTA_PUESTOS_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('POSTULACIONES') and o.name = 'FK_POSTULAC_FK_ESTADO_ESTADO_P')
alter table POSTULACIONES
   drop constraint FK_POSTULAC_FK_ESTADO_ESTADO_P
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('POSTULACIONES') and o.name = 'FK_POSTULAC_FK_OFERTA_OFERTAS')
alter table POSTULACIONES
   drop constraint FK_POSTULAC_FK_OFERTA_OFERTAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('POSTULACIONES') and o.name = 'FK_POSTULAC_FK_USUARI_USUARIOS')
alter table POSTULACIONES
   drop constraint FK_POSTULAC_FK_USUARI_USUARIOS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUESTOS_LABORALES') and o.name = 'FK_PUESTOS__FK_AREAS__AREAS_LA')
alter table PUESTOS_LABORALES
   drop constraint FK_PUESTOS__FK_AREAS__AREAS_LA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIOS') and o.name = 'FK_USUARIOS_FK_TIPO_U_TIPO_USU')
alter table USUARIOS
   drop constraint FK_USUARIOS_FK_TIPO_U_TIPO_USU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO_DETALLES') and o.name = 'FK_USUARIO__FK_DETALL_USUARIOS')
alter table USUARIO_DETALLES
   drop constraint FK_USUARIO__FK_DETALL_USUARIOS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO_EDUCACION') and o.name = 'FK_USUARIO__FK_EDUCAC_USUARIOS')
alter table USUARIO_EDUCACION
   drop constraint FK_USUARIO__FK_EDUCAC_USUARIOS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO_EXPERIENCIA_LABORAL') and o.name = 'FK_USUARIO__FK_EXPERI_USUARIOS')
alter table USUARIO_EXPERIENCIA_LABORAL
   drop constraint FK_USUARIO__FK_EXPERI_USUARIOS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO_PERFIL') and o.name = 'FK_USUARIO__FK_PERFIL_USUARIOS')
alter table USUARIO_PERFIL
   drop constraint FK_USUARIO__FK_PERFIL_USUARIOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ADMINISTRADORES')
            and   type = 'U')
   drop table ADMINISTRADORES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AE_SECTORES_PRINCIPALES')
            and   type = 'U')
   drop table AE_SECTORES_PRINCIPALES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AE_SUBDIVISION')
            and   name  = 'FK_SECTORES_PRINCIPALES_SUBDIVISION_FK'
            and   indid > 0
            and   indid < 255)
   drop index AE_SUBDIVISION.FK_SECTORES_PRINCIPALES_SUBDIVISION_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AE_SUBDIVISION')
            and   type = 'U')
   drop table AE_SUBDIVISION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AREAS_LABORALES')
            and   type = 'U')
   drop table AREAS_LABORALES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CIUDADES')
            and   name  = 'FK_PROVINCIAS_CIUDADES_FK'
            and   indid > 0
            and   indid < 255)
   drop index CIUDADES.FK_PROVINCIAS_CIUDADES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CIUDADES')
            and   type = 'U')
   drop table CIUDADES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EMPRESAS')
            and   name  = 'FK_SECTORES_PRINCIPALES_EMPRESAS_FK'
            and   indid > 0
            and   indid < 255)
   drop index EMPRESAS.FK_SECTORES_PRINCIPALES_EMPRESAS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EMPRESAS')
            and   type = 'U')
   drop table EMPRESAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ESTADO_POSTULACION')
            and   type = 'U')
   drop table ESTADO_POSTULACION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OFERTAS')
            and   name  = 'FK_OFERTAS_AREAS_LABORALES_FK'
            and   indid > 0
            and   indid < 255)
   drop index OFERTAS.FK_OFERTAS_AREAS_LABORALES_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OFERTAS')
            and   name  = 'FK_OFERTAS_CIUDAD_FK'
            and   indid > 0
            and   indid < 255)
   drop index OFERTAS.FK_OFERTAS_CIUDAD_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OFERTAS')
            and   name  = 'FK_OFERTAS_PUESTOS_LABORALES_FK'
            and   indid > 0
            and   indid < 255)
   drop index OFERTAS.FK_OFERTAS_PUESTOS_LABORALES_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OFERTAS')
            and   name  = 'FK_OFERTAS_OFERTA_MODALIDAD_FK'
            and   indid > 0
            and   indid < 255)
   drop index OFERTAS.FK_OFERTAS_OFERTA_MODALIDAD_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OFERTAS')
            and   name  = 'FK_EMPRESAS_OFERTAS_FK'
            and   indid > 0
            and   indid < 255)
   drop index OFERTAS.FK_EMPRESAS_OFERTAS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OFERTAS')
            and   type = 'U')
   drop table OFERTAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OFERTA_MODALIDAD')
            and   type = 'U')
   drop table OFERTA_MODALIDAD
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('POSTULACIONES')
            and   name  = 'FK_ESTADO_POSTULACION_FK'
            and   indid > 0
            and   indid < 255)
   drop index POSTULACIONES.FK_ESTADO_POSTULACION_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('POSTULACIONES')
            and   name  = 'FK_USUARIOS_POSTULACIONES_FK'
            and   indid > 0
            and   indid < 255)
   drop index POSTULACIONES.FK_USUARIOS_POSTULACIONES_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('POSTULACIONES')
            and   name  = 'FK_OFERTAS_POSTULACIONES_FK'
            and   indid > 0
            and   indid < 255)
   drop index POSTULACIONES.FK_OFERTAS_POSTULACIONES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('POSTULACIONES')
            and   type = 'U')
   drop table POSTULACIONES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PROVINCIAS')
            and   type = 'U')
   drop table PROVINCIAS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PUESTOS_LABORALES')
            and   name  = 'FK_AREAS_PUESTOS_LABORALES_FK'
            and   indid > 0
            and   indid < 255)
   drop index PUESTOS_LABORALES.FK_AREAS_PUESTOS_LABORALES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUESTOS_LABORALES')
            and   type = 'U')
   drop table PUESTOS_LABORALES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TIPO_USUARIO')
            and   type = 'U')
   drop table TIPO_USUARIO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USUARIOS')
            and   name  = 'FK_TIPO_USUARIO_FK'
            and   indid > 0
            and   indid < 255)
   drop index USUARIOS.FK_TIPO_USUARIO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIOS')
            and   type = 'U')
   drop table USUARIOS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USUARIO_DETALLES')
            and   name  = 'FK_DETALLES_USUARIO_FK'
            and   indid > 0
            and   indid < 255)
   drop index USUARIO_DETALLES.FK_DETALLES_USUARIO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO_DETALLES')
            and   type = 'U')
   drop table USUARIO_DETALLES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USUARIO_EDUCACION')
            and   name  = 'FK_EDUCACION_USUARIO_FK'
            and   indid > 0
            and   indid < 255)
   drop index USUARIO_EDUCACION.FK_EDUCACION_USUARIO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO_EDUCACION')
            and   type = 'U')
   drop table USUARIO_EDUCACION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USUARIO_EXPERIENCIA_LABORAL')
            and   name  = 'FK_EXPERIENCIA_LABORAL_FK'
            and   indid > 0
            and   indid < 255)
   drop index USUARIO_EXPERIENCIA_LABORAL.FK_EXPERIENCIA_LABORAL_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO_EXPERIENCIA_LABORAL')
            and   type = 'U')
   drop table USUARIO_EXPERIENCIA_LABORAL
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USUARIO_PERFIL')
            and   name  = 'FK_PERFIL_USUARIO_FK'
            and   indid > 0
            and   indid < 255)
   drop index USUARIO_PERFIL.FK_PERFIL_USUARIO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO_PERFIL')
            and   type = 'U')
   drop table USUARIO_PERFIL
go

/*==============================================================*/
/* Table: ADMINISTRADORES                                       */
/*==============================================================*/
create table ADMINISTRADORES (
   ADM_ID               int                  not null,
   ADM_NOMBRE           varchar(64)          not null,
   ADM_APELLIDO         varchar(64)          not null,
   ADM_CORREO           varchar(128)         not null,
   ADM_PASSWORD         varchar(128)         not null,
   ADM_DIRECCION        varchar(128)         null,
   constraint PK_ADMINISTRADORES primary key nonclustered (ADM_ID)
)
go

/*==============================================================*/
/* Table: AE_SECTORES_PRINCIPALES                               */
/*==============================================================*/
create table AE_SECTORES_PRINCIPALES (
   AEP_ID               int                  not null,
   AEP_NOMBRE           varchar(128)         not null,
   constraint PK_AE_SECTORES_PRINCIPALES primary key nonclustered (AEP_ID)
)
go

/*==============================================================*/
/* Table: AE_SUBDIVISION                                        */
/*==============================================================*/
create table AE_SUBDIVISION (
   AED_ID               int                  not null,
   AEP_ID               int                  not null,
   AED_NOMBRE           varchar(128)         not null,
   constraint PK_AE_SUBDIVISION primary key nonclustered (AED_ID)
)
go

/*==============================================================*/
/* Index: FK_SECTORES_PRINCIPALES_SUBDIVISION_FK                */
/*==============================================================*/
create index FK_SECTORES_PRINCIPALES_SUBDIVISION_FK on AE_SUBDIVISION (
AEP_ID ASC
)
go

/*==============================================================*/
/* Table: AREAS_LABORALES                                       */
/*==============================================================*/
create table AREAS_LABORALES (
   ARL_ID               int                  not null,
   ARL_NOMBRE           varchar(64)          not null,
   constraint PK_AREAS_LABORALES primary key nonclustered (ARL_ID)
)
go

/*==============================================================*/
/* Table: CIUDADES                                              */
/*==============================================================*/
create table CIUDADES (
   CID_ID               int                  not null,
   PRO_ID               int                  not null,
   CID_NOMBRE           varchar(64)          not null,
   constraint PK_CIUDADES primary key nonclustered (CID_ID)
)
go

/*==============================================================*/
/* Index: FK_PROVINCIAS_CIUDADES_FK                             */
/*==============================================================*/
create index FK_PROVINCIAS_CIUDADES_FK on CIUDADES (
PRO_ID ASC
)
go

/*==============================================================*/
/* Table: EMPRESAS                                              */
/*==============================================================*/
create table EMPRESAS (
   EMP_ID               int                  not null,
   AEP_ID               int                  not null,
   EMP_NOMBRE_EMPRESA   varchar(64)          not null,
   EMP_EMAIL_REGISTRO   varchar(128)         not null,
   EMP_EMAIL_ACCESO     varchar(128)         not null,
   EMP_PASSWORD         varchar(128)         not null,
   EMP_RUC              varchar(20)          not null,
   EMP_RAZON_SOCIAL     varchar(128)         not null,
   EMP_CIUDAD           varchar(128)         null,
   EMP_TELEFONO         numeric              not null,
   EMP_NUMERO_TRABAJADORES numeric              null,
   EMP_VACANTES_ANUALES numeric              null,
   constraint PK_EMPRESAS primary key nonclustered (EMP_ID)
)
go

/*==============================================================*/
/* Index: FK_SECTORES_PRINCIPALES_EMPRESAS_FK                   */
/*==============================================================*/
create index FK_SECTORES_PRINCIPALES_EMPRESAS_FK on EMPRESAS (
AEP_ID ASC
)
go

/*==============================================================*/
/* Table: ESTADO_POSTULACION                                    */
/*==============================================================*/
create table ESTADO_POSTULACION (
   ESP_ID               int                  not null,
   ESP_NOMBRE           varchar(64)          not null,
   ESP_DESCRIPCION      varchar(128)         not null,
   constraint PK_ESTADO_POSTULACION primary key nonclustered (ESP_ID)
)
go

/*==============================================================*/
/* Table: OFERTAS                                               */
/*==============================================================*/
create table OFERTAS (
   OFE_ID               int                  not null,
   EMP_ID               int                  not null,
   OFM_ID               int                  not null,
   PUL_ID               int                  not null,
   CID_ID               int                  not null,
   ARL_ID               int                  not null,
   OFE_TITULO           varchar(64)          not null,
   OFE_DESCRIPCION      varchar(128)         not null,
   OFE_TIPO_CONTRATO    varchar(64)          not null,
   OFE_SALARIO          decimal(10,2)        not null,
   OFE_FECHA_PUBLICACION datetime             not null,
   OFE_CANTIDAD_VACANTES numeric              not null,
   OFE_TIEMPO_EXPERIENCIA int                  not null,
   OFE_EDUCACION_MINIMA varchar(64)          not null,
   OFE_LICENCIA         bit                  not null,
   OFE_DISPONIBILIDAD_VIAJAR bit                  not null,
   OFE_DISPONIBILIDAD_CAMBIO_RESIDENCIA bit                  not null,
   OFE_DISCAPACIDAD     bit                  not null,
   OFE_EDAD_MINIMA      int                  null,
   OFE_AREA_LABORAL     varchar(64)          null,
   constraint PK_OFERTAS primary key nonclustered (OFE_ID)
)
go

/*==============================================================*/
/* Index: FK_EMPRESAS_OFERTAS_FK                                */
/*==============================================================*/
create index FK_EMPRESAS_OFERTAS_FK on OFERTAS (
EMP_ID ASC
)
go

/*==============================================================*/
/* Index: FK_OFERTAS_OFERTA_MODALIDAD_FK                        */
/*==============================================================*/
create index FK_OFERTAS_OFERTA_MODALIDAD_FK on OFERTAS (
OFM_ID ASC
)
go

/*==============================================================*/
/* Index: FK_OFERTAS_PUESTOS_LABORALES_FK                       */
/*==============================================================*/
create index FK_OFERTAS_PUESTOS_LABORALES_FK on OFERTAS (
PUL_ID ASC
)
go

/*==============================================================*/
/* Index: FK_OFERTAS_CIUDAD_FK                                  */
/*==============================================================*/
create index FK_OFERTAS_CIUDAD_FK on OFERTAS (
CID_ID ASC
)
go

/*==============================================================*/
/* Index: FK_OFERTAS_AREAS_LABORALES_FK                         */
/*==============================================================*/
create index FK_OFERTAS_AREAS_LABORALES_FK on OFERTAS (
ARL_ID ASC
)
go

/*==============================================================*/
/* Table: OFERTA_MODALIDAD                                      */
/*==============================================================*/
create table OFERTA_MODALIDAD (
   OFM_ID               int                  not null,
   OFM_NOMBRE           varchar(64)          not null,
   constraint PK_OFERTA_MODALIDAD primary key nonclustered (OFM_ID)
)
go

/*==============================================================*/
/* Table: POSTULACIONES                                         */
/*==============================================================*/
create table POSTULACIONES (
   POS_ID               int                  not null,
   OFE_ID               int                  not null,
   USU_ID               int                  not null,
   ESP_ID               int                  not null,
   POS_FECHA_POSTULACION datetime             not null,
   constraint PK_POSTULACIONES primary key nonclustered (POS_ID)
)
go

/*==============================================================*/
/* Index: FK_OFERTAS_POSTULACIONES_FK                           */
/*==============================================================*/
create index FK_OFERTAS_POSTULACIONES_FK on POSTULACIONES (
OFE_ID ASC
)
go

/*==============================================================*/
/* Index: FK_USUARIOS_POSTULACIONES_FK                          */
/*==============================================================*/
create index FK_USUARIOS_POSTULACIONES_FK on POSTULACIONES (
USU_ID ASC
)
go

/*==============================================================*/
/* Index: FK_ESTADO_POSTULACION_FK                              */
/*==============================================================*/
create index FK_ESTADO_POSTULACION_FK on POSTULACIONES (
ESP_ID ASC
)
go

/*==============================================================*/
/* Table: PROVINCIAS                                            */
/*==============================================================*/
create table PROVINCIAS (
   PRO_ID               int                  not null,
   PRO_NOMBRE           varchar(64)          not null,
   constraint PK_PROVINCIAS primary key nonclustered (PRO_ID)
)
go

/*==============================================================*/
/* Table: PUESTOS_LABORALES                                     */
/*==============================================================*/
create table PUESTOS_LABORALES (
   PUL_ID               int                  not null,
   ARL_ID               int                  not null,
   PUL_NOMBRE           varchar(64)          not null,
   constraint PK_PUESTOS_LABORALES primary key nonclustered (PUL_ID)
)
go

/*==============================================================*/
/* Index: FK_AREAS_PUESTOS_LABORALES_FK                         */
/*==============================================================*/
create index FK_AREAS_PUESTOS_LABORALES_FK on PUESTOS_LABORALES (
ARL_ID ASC
)
go

/*==============================================================*/
/* Table: TIPO_USUARIO                                          */
/*==============================================================*/
create table TIPO_USUARIO (
   TIP_ID               int                  not null,
   TIP_NOMBRE           varchar(64)          not null,
   TIP_DESCRIPCION      varchar(128)         not null,
   constraint PK_TIPO_USUARIO primary key nonclustered (TIP_ID)
)
go

/*==============================================================*/
/* Table: USUARIOS                                              */
/*==============================================================*/
create table USUARIOS (
   USU_ID               int                  not null,
   TIP_ID               int                  not null,
   USU_NOMBRE           varchar(64)          not null,
   USU_APELLIDO         varchar(64)          not null,
   USU_CORREO           varchar(128)         not null,
   USU_PASSWORD         varchar(128)         not null,
   USU_TELEFONO         numeric              not null,
   USU_CEDULA           varchar(20)          not null,
   constraint PK_USUARIOS primary key nonclustered (USU_ID)
)
go

/*==============================================================*/
/* Index: FK_TIPO_USUARIO_FK                                    */
/*==============================================================*/
create index FK_TIPO_USUARIO_FK on USUARIOS (
TIP_ID ASC
)
go

/*==============================================================*/
/* Table: USUARIO_DETALLES                                      */
/*==============================================================*/
create table USUARIO_DETALLES (
   USD_ID               int                  not null,
   USU_ID               int                  not null,
   USD_FECHA_NACIMIENTO datetime             not null,
   USD_ESTADO_CIVIL     varchar(64)          not null,
   USD_FOTO             varchar(128)         null,
   USD_CIUDAD           varchar(64)          null,
   USD_GENERO           varchar(64)          not null,
   constraint PK_USUARIO_DETALLES primary key nonclustered (USD_ID)
)
go

/*==============================================================*/
/* Index: FK_DETALLES_USUARIO_FK                                */
/*==============================================================*/
create index FK_DETALLES_USUARIO_FK on USUARIO_DETALLES (
USU_ID ASC
)
go

/*==============================================================*/
/* Table: USUARIO_EDUCACION                                     */
/*==============================================================*/
create table USUARIO_EDUCACION (
   USE_ID               int                  not null,
   USU_ID               int                  not null,
   USE_TIPO_FORMACION   varchar(64)          not null,
   USE_DOCUMENTO        varchar(128)         not null,
   USE_DESCRIPCION      varchar(128)         not null,
   constraint PK_USUARIO_EDUCACION primary key nonclustered (USE_ID)
)
go

/*==============================================================*/
/* Index: FK_EDUCACION_USUARIO_FK                               */
/*==============================================================*/
create index FK_EDUCACION_USUARIO_FK on USUARIO_EDUCACION (
USU_ID ASC
)
go

/*==============================================================*/
/* Table: USUARIO_EXPERIENCIA_LABORAL                           */
/*==============================================================*/
create table USUARIO_EXPERIENCIA_LABORAL (
   USX_ID               int                  not null,
   USU_ID               int                  not null,
   USX_EMPRESA          varchar(128)         not null,
   USX_AREA             varchar(64)          not null,
   USX_PUESTO           varchar(64)          not null,
   USX_FECHA_INICIO     datetime             not null,
   USX_FECHA_FIN        datetime             null,
   USX_NIVEL_EXPERIENCIA varchar(32)          not null,
   constraint PK_USUARIO_EXPERIENCIA_LABORAL primary key nonclustered (USX_ID)
)
go

/*==============================================================*/
/* Index: FK_EXPERIENCIA_LABORAL_FK                             */
/*==============================================================*/
create index FK_EXPERIENCIA_LABORAL_FK on USUARIO_EXPERIENCIA_LABORAL (
USU_ID ASC
)
go

/*==============================================================*/
/* Table: USUARIO_PERFIL                                        */
/*==============================================================*/
create table USUARIO_PERFIL (
   USP_ID               int                  not null,
   USU_ID               int                  not null,
   USP_ASPIRACION_LARBORAL varchar(128)         not null,
   USP_PREFERENCIA_SALARIAL decimal(10,2)        not null,
   USP_HOJA_VIDA        varchar(128)         null,
   USP_DISCAPACIDAD     varchar(32)          null,
   constraint PK_USUARIO_PERFIL primary key nonclustered (USP_ID)
)
go

/*==============================================================*/
/* Index: FK_PERFIL_USUARIO_FK                                  */
/*==============================================================*/
create index FK_PERFIL_USUARIO_FK on USUARIO_PERFIL (
USU_ID ASC
)
go

alter table AE_SUBDIVISION
   add constraint FK_AE_SUBDI_FK_SECTOR_AE_SECTO foreign key (AEP_ID)
      references AE_SECTORES_PRINCIPALES (AEP_ID)
go

alter table CIUDADES
   add constraint FK_CIUDADES_FK_PROVIN_PROVINCI foreign key (PRO_ID)
      references PROVINCIAS (PRO_ID)
go

alter table EMPRESAS
   add constraint FK_EMPRESAS_FK_SECTOR_AE_SECTO foreign key (AEP_ID)
      references AE_SECTORES_PRINCIPALES (AEP_ID)
go

alter table OFERTAS
   add constraint FK_OFERTAS_FK_EMPRES_EMPRESAS foreign key (EMP_ID)
      references EMPRESAS (EMP_ID)
go

alter table OFERTAS
   add constraint FK_OFERTAS_FK_OFERTA_AREAS_LA foreign key (ARL_ID)
      references AREAS_LABORALES (ARL_ID)
go

alter table OFERTAS
   add constraint FK_OFERTAS_FK_OFERTA_CIUDADES foreign key (CID_ID)
      references CIUDADES (CID_ID)
go

alter table OFERTAS
   add constraint FK_OFERTAS_FK_OFERTA_OFERTA_M foreign key (OFM_ID)
      references OFERTA_MODALIDAD (OFM_ID)
go

alter table OFERTAS
   add constraint FK_OFERTAS_FK_OFERTA_PUESTOS_ foreign key (PUL_ID)
      references PUESTOS_LABORALES (PUL_ID)
go

alter table POSTULACIONES
   add constraint FK_POSTULAC_FK_ESTADO_ESTADO_P foreign key (ESP_ID)
      references ESTADO_POSTULACION (ESP_ID)
go

alter table POSTULACIONES
   add constraint FK_POSTULAC_FK_OFERTA_OFERTAS foreign key (OFE_ID)
      references OFERTAS (OFE_ID)
go

alter table POSTULACIONES
   add constraint FK_POSTULAC_FK_USUARI_USUARIOS foreign key (USU_ID)
      references USUARIOS (USU_ID)
go

alter table PUESTOS_LABORALES
   add constraint FK_PUESTOS__FK_AREAS__AREAS_LA foreign key (ARL_ID)
      references AREAS_LABORALES (ARL_ID)
go

alter table USUARIOS
   add constraint FK_USUARIOS_FK_TIPO_U_TIPO_USU foreign key (TIP_ID)
      references TIPO_USUARIO (TIP_ID)
go

alter table USUARIO_DETALLES
   add constraint FK_USUARIO__FK_DETALL_USUARIOS foreign key (USU_ID)
      references USUARIOS (USU_ID)
go

alter table USUARIO_EDUCACION
   add constraint FK_USUARIO__FK_EDUCAC_USUARIOS foreign key (USU_ID)
      references USUARIOS (USU_ID)
go

alter table USUARIO_EXPERIENCIA_LABORAL
   add constraint FK_USUARIO__FK_EXPERI_USUARIOS foreign key (USU_ID)
      references USUARIOS (USU_ID)
go

alter table USUARIO_PERFIL
   add constraint FK_USUARIO__FK_PERFIL_USUARIOS foreign key (USU_ID)
      references USUARIOS (USU_ID)
go

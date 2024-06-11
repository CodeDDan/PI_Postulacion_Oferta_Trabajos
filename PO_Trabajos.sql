/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     10/6/2024 20:08:49                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OFERTAS') and o.name = 'FK_OFERTAS_FK_EMPRES_EMPRESAS')
alter table OFERTAS
   drop constraint FK_OFERTAS_FK_EMPRES_EMPRESAS
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
   where r.fkeyid = object_id('USUARIOS') and o.name = 'FK_USUARIOS_FK_TIPO_U_TIPO_USU')
alter table USUARIOS
   drop constraint FK_USUARIOS_FK_TIPO_U_TIPO_USU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ADMINISTRADORES')
            and   type = 'U')
   drop table ADMINISTRADORES
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
/* Table: EMPRESAS                                              */
/*==============================================================*/
create table EMPRESAS (
   EMP_ID               int                  not null,
   EMP_NOMBRE           varchar(64)          not null,
   EMP_RUC              varchar(20)          not null,
   EMP_RAZON_SOCIAL     varchar(128)         not null,
   EMP_DIRECCION        varchar(128)         not null,
   EMP_CORREO           varchar(128)         not null,
   EMP_TELEFONO         numeric              not null,
   constraint PK_EMPRESAS primary key nonclustered (EMP_ID)
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
   OFE_TITULO           varchar(64)          not null,
   OFE_DESCRIPCION      varchar(128)         not null,
   OFE_REQUISITOS       varchar(128)         not null,
   OFE_SALARIO          numeric              not null,
   OFE_FECHA_PUBLICACION datetime             not null,
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
   USU_DIRECCION        varchar(128)         not null,
   USU_TELEFONO         numeric              not null,
   USU_CEDULA           varchar(20)          not null,
   USU_FECHA_NACIMIENTO datetime             not null,
   USU_CURRICULUM_VITAE varchar(128)         not null,
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

alter table OFERTAS
   add constraint FK_OFERTAS_FK_EMPRES_EMPRESAS foreign key (EMP_ID)
      references EMPRESAS (EMP_ID)
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

alter table USUARIOS
   add constraint FK_USUARIOS_FK_TIPO_U_TIPO_USU foreign key (TIP_ID)
      references TIPO_USUARIO (TIP_ID)
go


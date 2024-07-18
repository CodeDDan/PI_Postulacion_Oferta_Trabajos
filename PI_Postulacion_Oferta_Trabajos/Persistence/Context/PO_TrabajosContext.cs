using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PI_Postulacion_Oferta_Trabajos.Models;

namespace PI_Postulacion_Oferta_Trabajos.Persistence.Context
{
    public partial class PO_TrabajosContext : IdentityDbContext<Usuario>
    {
        public PO_TrabajosContext()
        {
        }

        public PO_TrabajosContext(DbContextOptions<PO_TrabajosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradores { get; set; } = null!;
        public virtual DbSet<AeSectorPrincipal> AeSectoresPrincipales { get; set; } = null!;
        public virtual DbSet<AeSubdivision> AeSubdivisions { get; set; } = null!;
        public virtual DbSet<AreaLaboral> AreasLaborales { get; set; } = null!;
        public virtual DbSet<Ciudad> Ciudades { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<EstadoPostulacion> EstadoPostulacions { get; set; } = null!;
        public virtual DbSet<Oferta> Ofertas { get; set; } = null!;
        public virtual DbSet<OfertaModalidad> OfertaModalidads { get; set; } = null!;
        public virtual DbSet<Postulacion> Postulaciones { get; set; } = null!;
        public virtual DbSet<Provincia> Provincias { get; set; } = null!;
        public virtual DbSet<PuestoLaboral> PuestosLaborales { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioDetalle> UsuarioDetalles { get; set; } = null!;
        public virtual DbSet<UsuarioEducacion> UsuarioEducacions { get; set; } = null!;
        public virtual DbSet<UsuarioExperienciaLaboral> UsuarioExperienciaLaborals { get; set; } = null!;
        public virtual DbSet<UsuarioPerfil> UsuarioPerfils { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-ACCD3TS\\SQLEXPRESS;Initial Catalog=PO_Trabajos;Persist Security Info=True;User ID=Daniel;Password=1234;Trust Server Certificate=True;Encrypt=false;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Lo siguiente permite definir como se creara la base
            // Si no definimos nada, debemos usar correctamente los Data Annotations en cada clase

            base.OnModelCreating(modelBuilder);
            // Crearemos los roles
            // Las siguientes instrucciones crearán la migración con inserciones al usar Add-Migration

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var trabajador = new IdentityRole("trabajador");
            trabajador.NormalizedName = "trabajador";

            var empleador = new IdentityRole("empleador");
            empleador.NormalizedName = "empleador";

            modelBuilder.Entity<IdentityRole>().HasData(admin, trabajador, empleador);

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.AdmId)
                    .IsClustered(false);

                entity.ToTable("ADMINISTRADORES");

                entity.Property(e => e.AdmId).HasColumnName("ADM_ID");

                entity.Property(e => e.AdmApellido)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("ADM_APELLIDO");

                entity.Property(e => e.AdmCorreo)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ADM_CORREO");

                entity.Property(e => e.AdmDireccion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ADM_DIRECCION");

                entity.Property(e => e.AdmNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("ADM_NOMBRE");

                entity.Property(e => e.AdmPassword)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ADM_PASSWORD");
            });

            modelBuilder.Entity<AeSectorPrincipal>(entity =>
            {
                entity.HasKey(e => e.AepId)
                    .IsClustered(false);

                entity.ToTable("AE_SECTORES_PRINCIPALES");

                entity.Property(e => e.AepId).HasColumnName("AEP_ID");

                entity.Property(e => e.AepNombre)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("AEP_NOMBRE");
            });

            modelBuilder.Entity<AeSubdivision>(entity =>
            {
                entity.HasKey(e => e.AedId)
                    .IsClustered(false);

                entity.ToTable("AE_SUBDIVISION");

                entity.HasIndex(e => e.AepId, "FK_SECTORES_PRINCIPALES_SUBDIVISION_FK");

                entity.Property(e => e.AedId).HasColumnName("AED_ID");

                entity.Property(e => e.AedNombre)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("AED_NOMBRE");

                entity.Property(e => e.AepId).HasColumnName("AEP_ID");

                entity.HasOne(d => d.Aep)
                    .WithMany(p => p.AeSubdivisions)
                    .HasForeignKey(d => d.AepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AE_SUBDI_FK_SECTOR_AE_SECTO");
            });

            modelBuilder.Entity<AreaLaboral>(entity =>
            {
                entity.HasKey(e => e.ArlId)
                    .IsClustered(false);

                entity.ToTable("AREAS_LABORALES");

                entity.Property(e => e.ArlId).HasColumnName("ARL_ID");

                entity.Property(e => e.ArlNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("ARL_NOMBRE");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.CidId)
                    .IsClustered(false);

                entity.ToTable("CIUDADES");

                entity.HasIndex(e => e.ProId, "FK_PROVINCIAS_CIUDADES_FK");

                entity.Property(e => e.CidId).HasColumnName("CID_ID");

                entity.Property(e => e.CidNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("CID_NOMBRE");

                entity.Property(e => e.ProId).HasColumnName("PRO_ID");

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.Ciudades)
                    .HasForeignKey(d => d.ProId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIUDADES_FK_PROVIN_PROVINCI");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .IsClustered(false);

                entity.ToTable("EMPRESAS");

                entity.HasIndex(e => e.AepId, "FK_SECTORES_PRINCIPALES_EMPRESAS_FK");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.AepId).HasColumnName("AEP_ID");

                entity.Property(e => e.EmpCiudad)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_CIUDAD");

                entity.Property(e => e.EmpEmailAcceso)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_EMAIL_ACCESO");

                entity.Property(e => e.EmpEmailRegistro)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_EMAIL_REGISTRO");

                entity.Property(e => e.EmpNombreEmpresa)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("EMP_NOMBRE_EMPRESA");

                entity.Property(e => e.EmpNumeroTrabajadores)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("EMP_NUMERO_TRABAJADORES");

                entity.Property(e => e.EmpPassword)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_PASSWORD");

                entity.Property(e => e.EmpRazonSocial)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_RAZON_SOCIAL");

                entity.Property(e => e.EmpRuc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMP_RUC");

                entity.Property(e => e.EmpTelefono)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("EMP_TELEFONO");

                entity.Property(e => e.EmpVacantesAnuales)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("EMP_VACANTES_ANUALES");

                entity.HasOne(d => d.Aep)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.AepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPRESAS_FK_SECTOR_AE_SECTO");
            });

            modelBuilder.Entity<EstadoPostulacion>(entity =>
            {
                entity.HasKey(e => e.EspId)
                    .IsClustered(false);

                entity.ToTable("ESTADO_POSTULACION");

                entity.Property(e => e.EspId).HasColumnName("ESP_ID");

                entity.Property(e => e.EspDescripcion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ESP_DESCRIPCION");

                entity.Property(e => e.EspNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("ESP_NOMBRE");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(e => e.OfeId)
                    .IsClustered(false);

                entity.ToTable("OFERTAS");

                entity.HasIndex(e => e.EmpId, "FK_EMPRESAS_OFERTAS_FK");

                entity.HasIndex(e => e.ArlId, "FK_OFERTAS_AREAS_LABORALES_FK");

                entity.HasIndex(e => e.CidId, "FK_OFERTAS_CIUDAD_FK");

                entity.HasIndex(e => e.OfmId, "FK_OFERTAS_OFERTA_MODALIDAD_FK");

                entity.HasIndex(e => e.PulId, "FK_OFERTAS_PUESTOS_LABORALES_FK");

                entity.Property(e => e.OfeId).HasColumnName("OFE_ID");

                entity.Property(e => e.ArlId).HasColumnName("ARL_ID");

                entity.Property(e => e.CidId).HasColumnName("CID_ID");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.OfeAreaLaboral)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFE_AREA_LABORAL");

                entity.Property(e => e.OfeCantidadVacantes)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("OFE_CANTIDAD_VACANTES");

                entity.Property(e => e.OfeDescripcion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("OFE_DESCRIPCION");

                entity.Property(e => e.OfeDiscapacidad).HasColumnName("OFE_DISCAPACIDAD");

                entity.Property(e => e.OfeDisponibilidadCambioResidencia).HasColumnName("OFE_DISPONIBILIDAD_CAMBIO_RESIDENCIA");

                entity.Property(e => e.OfeDisponibilidadViajar).HasColumnName("OFE_DISPONIBILIDAD_VIAJAR");

                entity.Property(e => e.OfeEdadMinima).HasColumnName("OFE_EDAD_MINIMA");

                entity.Property(e => e.OfeEducacionMinima)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFE_EDUCACION_MINIMA");

                entity.Property(e => e.OfeFechaPublicacion)
                    .HasColumnType("datetime")
                    .HasColumnName("OFE_FECHA_PUBLICACION");

                entity.Property(e => e.OfeLicencia).HasColumnName("OFE_LICENCIA");

                entity.Property(e => e.OfeSalario)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("OFE_SALARIO");

                entity.Property(e => e.OfeTiempoExperiencia).HasColumnName("OFE_TIEMPO_EXPERIENCIA");

                entity.Property(e => e.OfeTipoContrato)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFE_TIPO_CONTRATO");

                entity.Property(e => e.OfeTitulo)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFE_TITULO");

                entity.Property(e => e.OfmId).HasColumnName("OFM_ID");

                entity.Property(e => e.PulId).HasColumnName("PUL_ID");

                entity.HasOne(d => d.Arl)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.ArlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_OFERTA_AREAS_LA");

                entity.HasOne(d => d.Cid)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.CidId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_OFERTA_CIUDADES");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_EMPRES_EMPRESAS");

                entity.HasOne(d => d.Ofm)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.OfmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_OFERTA_OFERTA_M");

                entity.HasOne(d => d.Pul)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.PulId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_OFERTA_PUESTOS_");
            });

            modelBuilder.Entity<OfertaModalidad>(entity =>
            {
                entity.HasKey(e => e.OfmId)
                    .IsClustered(false);

                entity.ToTable("OFERTA_MODALIDAD");

                entity.Property(e => e.OfmId).HasColumnName("OFM_ID");

                entity.Property(e => e.OfmNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFM_NOMBRE");
            });

            modelBuilder.Entity<Postulacion>(entity =>
            {
                entity.HasKey(e => e.PosId)
                    .IsClustered(false);

                entity.ToTable("POSTULACIONES");

                entity.HasIndex(e => e.EspId, "FK_ESTADO_POSTULACION_FK");

                entity.HasIndex(e => e.OfeId, "FK_OFERTAS_POSTULACIONES_FK");

                entity.HasIndex(e => e.UsuarioId, "FK_USUARIOS_POSTULACIONES_FK");

                entity.Property(e => e.PosId).HasColumnName("POS_ID");

                entity.Property(e => e.EspId).HasColumnName("ESP_ID");

                entity.Property(e => e.OfeId).HasColumnName("OFE_ID");

                entity.Property(e => e.PosFechaPostulacion)
                    .HasColumnType("datetime")
                    .HasColumnName("POS_FECHA_POSTULACION");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Esp)
                    .WithMany(p => p.Postulaciones)
                    .HasForeignKey(d => d.EspId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTULAC_FK_ESTADO_ESTADO_P");

                entity.HasOne(d => d.Ofe)
                    .WithMany(p => p.Postulaciones)
                    .HasForeignKey(d => d.OfeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTULAC_FK_OFERTA_OFERTAS");

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.Postulaciones)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTULAC_FK_USUARI_USUARIOS");
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .IsClustered(false);

                entity.ToTable("PROVINCIAS");

                entity.Property(e => e.ProId).HasColumnName("PRO_ID");

                entity.Property(e => e.ProNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("PRO_NOMBRE");
            });

            modelBuilder.Entity<PuestoLaboral>(entity =>
            {
                entity.HasKey(e => e.PulId)
                    .IsClustered(false);

                entity.ToTable("PUESTOS_LABORALES");

                entity.HasIndex(e => e.ArlId, "FK_AREAS_PUESTOS_LABORALES_FK");

                entity.Property(e => e.PulId).HasColumnName("PUL_ID");

                entity.Property(e => e.ArlId).HasColumnName("ARL_ID");

                entity.Property(e => e.PulNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("PUL_NOMBRE");

                entity.HasOne(d => d.Arl)
                    .WithMany(p => p.PuestosLaborales)
                    .HasForeignKey(d => d.ArlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PUESTOS__FK_AREAS__AREAS_LA");
            });

            modelBuilder.Entity<UsuarioDetalle>(entity =>
            {
                entity.HasKey(e => e.UsdId)
                    .IsClustered(false);

                entity.ToTable("USUARIO_DETALLES");

                entity.HasIndex(e => e.UsuarioId, "FK_DETALLES_USUARIO_FK");

                entity.Property(e => e.UsdId).HasColumnName("USD_ID");

                entity.Property(e => e.UsdCiudad)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USD_CIUDAD");

                entity.Property(e => e.UsdEstadoCivil)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USD_ESTADO_CIVIL");

                entity.Property(e => e.UsdFechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("USD_FECHA_NACIMIENTO");

                entity.Property(e => e.UsdFoto)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USD_FOTO");

                entity.Property(e => e.UsdGenero)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USD_GENERO");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.UsuarioDetalles)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO__FK_DETALL_USUARIOS");
            });

            modelBuilder.Entity<UsuarioEducacion>(entity =>
            {
                entity.HasKey(e => e.UseId)
                    .IsClustered(false);

                entity.ToTable("USUARIO_EDUCACION");

                entity.HasIndex(e => e.UsuarioId, "FK_EDUCACION_USUARIO_FK");

                entity.Property(e => e.UseId).HasColumnName("USE_ID");

                entity.Property(e => e.UseDescripcion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USE_DESCRIPCION");

                entity.Property(e => e.UseDocumento)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USE_DOCUMENTO");

                entity.Property(e => e.UseTipoFormacion)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USE_TIPO_FORMACION");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.UsuarioEducacions)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO__FK_EDUCAC_USUARIOS");
            });

            modelBuilder.Entity<UsuarioExperienciaLaboral>(entity =>
            {
                entity.HasKey(e => e.UsxId)
                    .IsClustered(false);

                entity.ToTable("USUARIO_EXPERIENCIA_LABORAL");

                entity.HasIndex(e => e.UsuarioId, "FK_EXPERIENCIA_LABORAL_FK");

                entity.Property(e => e.UsxId).HasColumnName("USX_ID");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.Property(e => e.UsxArea)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USX_AREA");

                entity.Property(e => e.UsxEmpresa)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USX_EMPRESA");

                entity.Property(e => e.UsxFechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("USX_FECHA_FIN");

                entity.Property(e => e.UsxFechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("USX_FECHA_INICIO");

                entity.Property(e => e.UsxNivelExperiencia)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("USX_NIVEL_EXPERIENCIA");

                entity.Property(e => e.UsxPuesto)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USX_PUESTO");

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.UsuarioExperienciaLaborals)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO__FK_EXPERI_USUARIOS");
            });

            modelBuilder.Entity<UsuarioPerfil>(entity =>
            {
                entity.HasKey(e => e.UspId)
                    .IsClustered(false);

                entity.ToTable("USUARIO_PERFIL");

                entity.HasIndex(e => e.UsuarioId, "FK_PERFIL_USUARIO_FK");

                entity.Property(e => e.UspId).HasColumnName("USP_ID");

                entity.Property(e => e.UspAspiracionLarboral)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USP_ASPIRACION_LARBORAL");

                entity.Property(e => e.UspDiscapacidad)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("USP_DISCAPACIDAD");

                entity.Property(e => e.UspHojaVida)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USP_HOJA_VIDA");

                entity.Property(e => e.UspPreferenciaSalarial)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("USP_PREFERENCIA_SALARIAL");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usu)
                    .WithMany(p => p.UsuarioPerfils)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO__FK_PERFIL_USUARIOS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

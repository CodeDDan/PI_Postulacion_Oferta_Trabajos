using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PI_Postulacion_Oferta_Trabajos.Models;

namespace PI_Postulacion_Oferta_Trabajos.Persistence.Context
{
    public partial class PO_TrabajosContext : DbContext
    {
        public PO_TrabajosContext()
        {
        }

        public PO_TrabajosContext(DbContextOptions<PO_TrabajosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradores { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<EstadoPostulacion> EstadoPostulacions { get; set; } = null!;
        public virtual DbSet<Oferta> Ofertas { get; set; } = null!;
        public virtual DbSet<Postulacion> Postulaciones { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Cadena de conexion");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.AdmId)
                    .IsClustered(false);

                entity.ToTable("ADMINISTRADORES");

                entity.Property(e => e.AdmId)
                    .ValueGeneratedNever()
                    .HasColumnName("ADM_ID");

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

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .IsClustered(false);

                entity.ToTable("EMPRESAS");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("EMP_ID");

                entity.Property(e => e.EmpCorreo)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_CORREO");

                entity.Property(e => e.EmpDireccion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("EMP_DIRECCION");

                entity.Property(e => e.EmpNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("EMP_NOMBRE");

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
            });

            modelBuilder.Entity<EstadoPostulacion>(entity =>
            {
                entity.HasKey(e => e.EspId)
                    .IsClustered(false);

                entity.ToTable("ESTADO_POSTULACION");

                entity.Property(e => e.EspId)
                    .ValueGeneratedNever()
                    .HasColumnName("ESP_ID");

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

                entity.Property(e => e.OfeId)
                    .ValueGeneratedNever()
                    .HasColumnName("OFE_ID");

                entity.Property(e => e.EmpId).HasColumnName("EMP_ID");

                entity.Property(e => e.OfeDescripcion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("OFE_DESCRIPCION");

                entity.Property(e => e.OfeFechaPublicacion)
                    .HasColumnType("datetime")
                    .HasColumnName("OFE_FECHA_PUBLICACION");

                entity.Property(e => e.OfeRequisitos)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("OFE_REQUISITOS");

                entity.Property(e => e.OfeSalario)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("OFE_SALARIO");

                entity.Property(e => e.OfeTitulo)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("OFE_TITULO");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OFERTAS_FK_EMPRES_EMPRESAS");
            });

            modelBuilder.Entity<Postulacion>(entity =>
            {
                entity.HasKey(e => e.PosId)
                    .IsClustered(false);

                entity.ToTable("POSTULACIONES");

                entity.HasIndex(e => e.EspId, "FK_ESTADO_POSTULACION_FK");

                entity.HasIndex(e => e.OfeId, "FK_OFERTAS_POSTULACIONES_FK");

                entity.HasIndex(e => e.UsuId, "FK_USUARIOS_POSTULACIONES_FK");

                entity.Property(e => e.PosId)
                    .ValueGeneratedNever()
                    .HasColumnName("POS_ID");

                entity.Property(e => e.EspId).HasColumnName("ESP_ID");

                entity.Property(e => e.OfeId).HasColumnName("OFE_ID");

                entity.Property(e => e.PosFechaPostulacion)
                    .HasColumnType("datetime")
                    .HasColumnName("POS_FECHA_POSTULACION");

                entity.Property(e => e.UsuId).HasColumnName("USU_ID");

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
                    .HasForeignKey(d => d.UsuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTULAC_FK_USUARI_USUARIOS");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.TipId)
                    .IsClustered(false);

                entity.ToTable("TIPO_USUARIO");

                entity.Property(e => e.TipId)
                    .ValueGeneratedNever()
                    .HasColumnName("TIP_ID");

                entity.Property(e => e.TipDescripcion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("TIP_DESCRIPCION");

                entity.Property(e => e.TipNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TIP_NOMBRE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuId)
                    .IsClustered(false);

                entity.ToTable("USUARIOS");

                entity.HasIndex(e => e.TipId, "FK_TIPO_USUARIO_FK");

                entity.Property(e => e.UsuId)
                    .ValueGeneratedNever()
                    .HasColumnName("USU_ID");

                entity.Property(e => e.TipId).HasColumnName("TIP_ID");

                entity.Property(e => e.UsuApellido)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USU_APELLIDO");

                entity.Property(e => e.UsuCedula)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USU_CEDULA");

                entity.Property(e => e.UsuCorreo)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USU_CORREO");

                entity.Property(e => e.UsuCurriculumVitae)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USU_CURRICULUM_VITAE");

                entity.Property(e => e.UsuDireccion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USU_DIRECCION");

                entity.Property(e => e.UsuFechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("USU_FECHA_NACIMIENTO");

                entity.Property(e => e.UsuNombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USU_NOMBRE");

                entity.Property(e => e.UsuPassword)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("USU_PASSWORD");

                entity.Property(e => e.UsuTelefono)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("USU_TELEFONO");

                entity.HasOne(d => d.Tip)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.TipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_FK_TIPO_U_TIPO_USU");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

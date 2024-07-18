using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI_Postulacion_Oferta_Trabajos.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADMINISTRADORES",
                columns: table => new
                {
                    ADM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADM_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    ADM_APELLIDO = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    ADM_CORREO = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ADM_PASSWORD = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ADM_DIRECCION = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINISTRADORES", x => x.ADM_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AE_SECTORES_PRINCIPALES",
                columns: table => new
                {
                    AEP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AEP_NOMBRE = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AE_SECTORES_PRINCIPALES", x => x.AEP_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AREAS_LABORALES",
                columns: table => new
                {
                    ARL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ARL_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AREAS_LABORALES", x => x.ARL_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsuNombre = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UsuApellido = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UsuCedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO_POSTULACION",
                columns: table => new
                {
                    ESP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ESP_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    ESP_DESCRIPCION = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO_POSTULACION", x => x.ESP_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "OFERTA_MODALIDAD",
                columns: table => new
                {
                    OFM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OFM_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFERTA_MODALIDAD", x => x.OFM_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "PROVINCIAS",
                columns: table => new
                {
                    PRO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRO_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVINCIAS", x => x.PRO_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AE_SUBDIVISION",
                columns: table => new
                {
                    AED_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AEP_ID = table.Column<int>(type: "int", nullable: false),
                    AED_NOMBRE = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AE_SUBDIVISION", x => x.AED_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_AE_SUBDI_FK_SECTOR_AE_SECTO",
                        column: x => x.AEP_ID,
                        principalTable: "AE_SECTORES_PRINCIPALES",
                        principalColumn: "AEP_ID");
                });

            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    EMP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AEP_ID = table.Column<int>(type: "int", nullable: false),
                    EMP_NOMBRE_EMPRESA = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    EMP_EMAIL_REGISTRO = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    EMP_EMAIL_ACCESO = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    EMP_PASSWORD = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    EMP_RUC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EMP_RAZON_SOCIAL = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    EMP_CIUDAD = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true),
                    EMP_TELEFONO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    EMP_NUMERO_TRABAJADORES = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    EMP_VACANTES_ANUALES = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.EMP_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EMPRESAS_FK_SECTOR_AE_SECTO",
                        column: x => x.AEP_ID,
                        principalTable: "AE_SECTORES_PRINCIPALES",
                        principalColumn: "AEP_ID");
                });

            migrationBuilder.CreateTable(
                name: "PUESTOS_LABORALES",
                columns: table => new
                {
                    PUL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ARL_ID = table.Column<int>(type: "int", nullable: false),
                    PUL_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PUESTOS_LABORALES", x => x.PUL_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PUESTOS__FK_AREAS__AREAS_LA",
                        column: x => x.ARL_ID,
                        principalTable: "AREAS_LABORALES",
                        principalColumn: "ARL_ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_DETALLES",
                columns: table => new
                {
                    USD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USD_FECHA_NACIMIENTO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USD_ESTADO_CIVIL = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    USD_FOTO = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true),
                    USD_CIUDAD = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    USD_GENERO = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_DETALLES", x => x.USD_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USUARIO__FK_DETALL_USUARIOS",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_EDUCACION",
                columns: table => new
                {
                    USE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USE_TIPO_FORMACION = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    USE_DOCUMENTO = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    USE_DESCRIPCION = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_EDUCACION", x => x.USE_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USUARIO__FK_EDUCAC_USUARIOS",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_EXPERIENCIA_LABORAL",
                columns: table => new
                {
                    USX_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USX_EMPRESA = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    USX_AREA = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    USX_PUESTO = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    USX_FECHA_INICIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USX_FECHA_FIN = table.Column<DateTime>(type: "datetime", nullable: true),
                    USX_NIVEL_EXPERIENCIA = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_EXPERIENCIA_LABORAL", x => x.USX_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USUARIO__FK_EXPERI_USUARIOS",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_PERFIL",
                columns: table => new
                {
                    USP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USP_ASPIRACION_LARBORAL = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    USP_PREFERENCIA_SALARIAL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    USP_HOJA_VIDA = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: true),
                    USP_DISCAPACIDAD = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_PERFIL", x => x.USP_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_USUARIO__FK_PERFIL_USUARIOS",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CIUDADES",
                columns: table => new
                {
                    CID_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRO_ID = table.Column<int>(type: "int", nullable: false),
                    CID_NOMBRE = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIUDADES", x => x.CID_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CIUDADES_FK_PROVIN_PROVINCI",
                        column: x => x.PRO_ID,
                        principalTable: "PROVINCIAS",
                        principalColumn: "PRO_ID");
                });

            migrationBuilder.CreateTable(
                name: "OFERTAS",
                columns: table => new
                {
                    OFE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMP_ID = table.Column<int>(type: "int", nullable: false),
                    OFM_ID = table.Column<int>(type: "int", nullable: false),
                    PUL_ID = table.Column<int>(type: "int", nullable: false),
                    CID_ID = table.Column<int>(type: "int", nullable: false),
                    ARL_ID = table.Column<int>(type: "int", nullable: false),
                    OFE_TITULO = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    OFE_DESCRIPCION = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    OFE_TIPO_CONTRATO = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    OFE_SALARIO = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OFE_FECHA_PUBLICACION = table.Column<DateTime>(type: "datetime", nullable: false),
                    OFE_CANTIDAD_VACANTES = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    OFE_TIEMPO_EXPERIENCIA = table.Column<int>(type: "int", nullable: false),
                    OFE_EDUCACION_MINIMA = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    OFE_LICENCIA = table.Column<bool>(type: "bit", nullable: false),
                    OFE_DISPONIBILIDAD_VIAJAR = table.Column<bool>(type: "bit", nullable: false),
                    OFE_DISPONIBILIDAD_CAMBIO_RESIDENCIA = table.Column<bool>(type: "bit", nullable: false),
                    OFE_DISCAPACIDAD = table.Column<bool>(type: "bit", nullable: false),
                    OFE_EDAD_MINIMA = table.Column<int>(type: "int", nullable: true),
                    OFE_AREA_LABORAL = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFERTAS", x => x.OFE_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_OFERTAS_FK_EMPRES_EMPRESAS",
                        column: x => x.EMP_ID,
                        principalTable: "EMPRESAS",
                        principalColumn: "EMP_ID");
                    table.ForeignKey(
                        name: "FK_OFERTAS_FK_OFERTA_AREAS_LA",
                        column: x => x.ARL_ID,
                        principalTable: "AREAS_LABORALES",
                        principalColumn: "ARL_ID");
                    table.ForeignKey(
                        name: "FK_OFERTAS_FK_OFERTA_CIUDADES",
                        column: x => x.CID_ID,
                        principalTable: "CIUDADES",
                        principalColumn: "CID_ID");
                    table.ForeignKey(
                        name: "FK_OFERTAS_FK_OFERTA_OFERTA_M",
                        column: x => x.OFM_ID,
                        principalTable: "OFERTA_MODALIDAD",
                        principalColumn: "OFM_ID");
                    table.ForeignKey(
                        name: "FK_OFERTAS_FK_OFERTA_PUESTOS_",
                        column: x => x.PUL_ID,
                        principalTable: "PUESTOS_LABORALES",
                        principalColumn: "PUL_ID");
                });

            migrationBuilder.CreateTable(
                name: "POSTULACIONES",
                columns: table => new
                {
                    POS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OFE_ID = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ESP_ID = table.Column<int>(type: "int", nullable: false),
                    POS_FECHA_POSTULACION = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTULACIONES", x => x.POS_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_POSTULAC_FK_ESTADO_ESTADO_P",
                        column: x => x.ESP_ID,
                        principalTable: "ESTADO_POSTULACION",
                        principalColumn: "ESP_ID");
                    table.ForeignKey(
                        name: "FK_POSTULAC_FK_OFERTA_OFERTAS",
                        column: x => x.OFE_ID,
                        principalTable: "OFERTAS",
                        principalColumn: "OFE_ID");
                    table.ForeignKey(
                        name: "FK_POSTULAC_FK_USUARI_USUARIOS",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09524f16-ab53-4b7f-bce4-a7d35d4f83f5", "443ee3c4-da4e-4c24-8b48-39b5510744f6", "empleador", "empleador" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1962c77e-b509-4b1c-a377-70e0eaf9dd3d", "473b9bb0-a8f8-49b8-9ffb-67d34a7bf277", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cf341e3-f214-4372-8897-dcea283ae777", "3d1fecca-52a7-42c8-bfa2-a34d68e1c0bd", "trabajador", "trabajador" });

            migrationBuilder.CreateIndex(
                name: "FK_SECTORES_PRINCIPALES_SUBDIVISION_FK",
                table: "AE_SUBDIVISION",
                column: "AEP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "FK_PROVINCIAS_CIUDADES_FK",
                table: "CIUDADES",
                column: "PRO_ID");

            migrationBuilder.CreateIndex(
                name: "FK_SECTORES_PRINCIPALES_EMPRESAS_FK",
                table: "EMPRESAS",
                column: "AEP_ID");

            migrationBuilder.CreateIndex(
                name: "FK_EMPRESAS_OFERTAS_FK",
                table: "OFERTAS",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "FK_OFERTAS_AREAS_LABORALES_FK",
                table: "OFERTAS",
                column: "ARL_ID");

            migrationBuilder.CreateIndex(
                name: "FK_OFERTAS_CIUDAD_FK",
                table: "OFERTAS",
                column: "CID_ID");

            migrationBuilder.CreateIndex(
                name: "FK_OFERTAS_OFERTA_MODALIDAD_FK",
                table: "OFERTAS",
                column: "OFM_ID");

            migrationBuilder.CreateIndex(
                name: "FK_OFERTAS_PUESTOS_LABORALES_FK",
                table: "OFERTAS",
                column: "PUL_ID");

            migrationBuilder.CreateIndex(
                name: "FK_ESTADO_POSTULACION_FK",
                table: "POSTULACIONES",
                column: "ESP_ID");

            migrationBuilder.CreateIndex(
                name: "FK_OFERTAS_POSTULACIONES_FK",
                table: "POSTULACIONES",
                column: "OFE_ID");

            migrationBuilder.CreateIndex(
                name: "FK_USUARIOS_POSTULACIONES_FK",
                table: "POSTULACIONES",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "FK_AREAS_PUESTOS_LABORALES_FK",
                table: "PUESTOS_LABORALES",
                column: "ARL_ID");

            migrationBuilder.CreateIndex(
                name: "FK_DETALLES_USUARIO_FK",
                table: "USUARIO_DETALLES",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "FK_EDUCACION_USUARIO_FK",
                table: "USUARIO_EDUCACION",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "FK_EXPERIENCIA_LABORAL_FK",
                table: "USUARIO_EXPERIENCIA_LABORAL",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "FK_PERFIL_USUARIO_FK",
                table: "USUARIO_PERFIL",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMINISTRADORES");

            migrationBuilder.DropTable(
                name: "AE_SUBDIVISION");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "POSTULACIONES");

            migrationBuilder.DropTable(
                name: "USUARIO_DETALLES");

            migrationBuilder.DropTable(
                name: "USUARIO_EDUCACION");

            migrationBuilder.DropTable(
                name: "USUARIO_EXPERIENCIA_LABORAL");

            migrationBuilder.DropTable(
                name: "USUARIO_PERFIL");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ESTADO_POSTULACION");

            migrationBuilder.DropTable(
                name: "OFERTAS");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EMPRESAS");

            migrationBuilder.DropTable(
                name: "CIUDADES");

            migrationBuilder.DropTable(
                name: "OFERTA_MODALIDAD");

            migrationBuilder.DropTable(
                name: "PUESTOS_LABORALES");

            migrationBuilder.DropTable(
                name: "AE_SECTORES_PRINCIPALES");

            migrationBuilder.DropTable(
                name: "PROVINCIAS");

            migrationBuilder.DropTable(
                name: "AREAS_LABORALES");
        }
    }
}

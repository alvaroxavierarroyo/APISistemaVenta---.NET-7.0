using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVenta.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModeloInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__8A3D240CF4AA4D15", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    idMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    icono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    url = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__C26AF48349C94CFA", x => x.idMenu);
                });

            migrationBuilder.CreateTable(
                name: "NumeroDocumento",
                columns: table => new
                {
                    idNumeroDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ultimo_Numero = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NumeroDo__471E421AA9C2B8EC", x => x.idNumeroDocumento);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__3C872F76C7AF975C", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    idVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroDocumento = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    tipoPago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__077D561436430922", x => x.idVenta);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    idCategoria = table.Column<int>(type: "int", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__07F4A132415D227F", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK__Producto__idCate__48CFD27E",
                        column: x => x.idCategoria,
                        principalTable: "Categorias",
                        principalColumn: "idCategoria");
                });

            migrationBuilder.CreateTable(
                name: "MenuRol",
                columns: table => new
                {
                    idMenuRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMenu = table.Column<int>(type: "int", nullable: true),
                    idRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuRol__9D6D61A41CB02165", x => x.idMenuRol);
                    table.ForeignKey(
                        name: "FK__MenuRol__idMenu__3C69FB99",
                        column: x => x.idMenu,
                        principalTable: "Menu",
                        principalColumn: "idMenu");
                    table.ForeignKey(
                        name: "FK__MenuRol__idRol__3D5E1FD2",
                        column: x => x.idRol,
                        principalTable: "Rol",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCompleto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    correo = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    idRol = table.Column<int>(type: "int", nullable: true),
                    clave = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__645723A632082C5C", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK__Usuario__idRol__403A8C7D",
                        column: x => x.idRol,
                        principalTable: "Rol",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    idDetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVenta = table.Column<int>(type: "int", nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__BFE2843FD810071C", x => x.idDetalleVenta);
                    table.ForeignKey(
                        name: "FK__DetalleVe__idPro__5441852A",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto");
                    table.ForeignKey(
                        name: "FK__DetalleVe__idVen__534D60F1",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_idProducto",
                table: "DetalleVentas",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_idVenta",
                table: "DetalleVentas",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRol_idMenu",
                table: "MenuRol",
                column: "idMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRol_idRol",
                table: "MenuRol",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idCategoria",
                table: "Producto",
                column: "idCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idRol",
                table: "Usuario",
                column: "idRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "MenuRol");

            migrationBuilder.DropTable(
                name: "NumeroDocumento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}

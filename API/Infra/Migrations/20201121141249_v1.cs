using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Escolaridade",
                schema: "dbo",
                columns: table => new
                {
                    CodEscolaridade = table.Column<int>(nullable: false),
                    Nivel = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolaridade", x => x.CodEscolaridade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "dbo",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 90, nullable: true),
                    SobreNome = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    DtNascimento = table.Column<DateTime>(nullable: true),
                    CodEscolaridade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Escolaridade_CodEscolaridade",
                        column: x => x.CodEscolaridade,
                        principalSchema: "dbo",
                        principalTable: "Escolaridade",
                        principalColumn: "CodEscolaridade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Escolaridade",
                columns: new[] { "CodEscolaridade", "Nivel" },
                values: new object[,]
                {
                    { 1, "Infantil" },
                    { 2, "Fundamental" },
                    { 3, "Médio" },
                    { 4, "Superior" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CodEscolaridade",
                schema: "dbo",
                table: "Usuarios",
                column: "CodEscolaridade");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                schema: "dbo",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Escolaridade",
                schema: "dbo");
        }
    }
}

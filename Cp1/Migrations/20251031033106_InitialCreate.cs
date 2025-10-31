using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESPECIALIDADES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESPECIALIDADES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEDICOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CRM = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONSULTAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false),
                    OBSERVACOES = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    PACIENTE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MEDICO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ESPECIALIDADE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSULTAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CONSULTAS_ESPECIALIDADES_ESPECIALIDADE_ID",
                        column: x => x.ESPECIALIDADE_ID,
                        principalTable: "ESPECIALIDADES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CONSULTAS_MEDICOS_MEDICO_ID",
                        column: x => x.MEDICO_ID,
                        principalTable: "MEDICOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CONSULTAS_PACIENTES_PACIENTE_ID",
                        column: x => x.PACIENTE_ID,
                        principalTable: "PACIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_DATA_HORA",
                table: "CONSULTAS",
                column: "DATA_HORA");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_ESPECIALIDADE_ID",
                table: "CONSULTAS",
                column: "ESPECIALIDADE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_MEDICO_ID",
                table: "CONSULTAS",
                column: "MEDICO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_PACIENTE_ID",
                table: "CONSULTAS",
                column: "PACIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_STATUS",
                table: "CONSULTAS",
                column: "STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_ESPECIALIDADES_NOME",
                table: "ESPECIALIDADES",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MEDICOS_CRM",
                table: "MEDICOS",
                column: "CRM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_CPF",
                table: "PACIENTES",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONSULTAS");

            migrationBuilder.DropTable(
                name: "ESPECIALIDADES");

            migrationBuilder.DropTable(
                name: "MEDICOS");

            migrationBuilder.DropTable(
                name: "PACIENTES");
        }
    }
}

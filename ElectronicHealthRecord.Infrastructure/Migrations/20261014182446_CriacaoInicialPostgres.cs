using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicHealthRecord.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RegistroCrm = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Especialidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Perfil = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosClinicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Queixa = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Diagnostico = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosClinicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosClinicos_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescricoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistroClinicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Medicamento = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Dosagem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Instrucoes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescricoes_RegistrosClinicos_RegistroClinicoId",
                        column: x => x.RegistroClinicoId,
                        principalTable: "RegistrosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_PacienteId",
                table: "Atendimentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProfissionalId",
                table: "Atendimentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Cpf",
                table: "Pacientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_RegistroClinicoId",
                table: "Prescricoes",
                column: "RegistroClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosClinicos_AtendimentoId",
                table: "RegistrosClinicos",
                column: "AtendimentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescricoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "RegistrosClinicos");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Profissionais");
        }
    }
}

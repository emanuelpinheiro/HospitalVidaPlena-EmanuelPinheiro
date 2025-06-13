using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalVidaPlena_EmanuelPinheiro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoSanguineo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroCartaoSUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PossuiPlanoSaude = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroConselho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspecialidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CargaHorariaSemanal = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissionais_Especialidades_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prontuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservacoesGerais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prontuarios_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProntuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Prontuarios_ProntuarioId",
                        column: x => x.ProntuarioId,
                        principalTable: "Prontuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Internacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevisaoAlta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoInternacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Leito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quarto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanoSaudeUtilizado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObservacoesClinicas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusInternacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internacoes_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Internacoes_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescricoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Medicamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViaAdministracao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusPrescricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReacoesAdversas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AltasHospitalares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CondicaoPaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instrucoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltasHospitalares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AltasHospitalares_Internacoes_InternacaoId",
                        column: x => x.InternacaoId,
                        principalTable: "Internacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Cardiologia" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Pediatria" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Ortopedia" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Neurologia" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Ginecologia" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Oncologia" },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Dermatologia" },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Psiquiatria" },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Endocrinologia" },
                    { new Guid("10000000-0000-0000-0000-00000000000a"), "Urologia" }
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "CPF", "DataNascimento", "Email", "EnderecoCompleto", "EstadoCivil", "NomeCompleto", "NumeroCartaoSUS", "PossuiPlanoSaude", "Sexo", "Telefone", "TipoSanguineo" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "98765432101", new DateTime(1985, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.oliveira@email.com", "Rua das Flores, 123", "Casada", "Maria Oliveira", "12345678901", true, "Feminino", "11988880001", "A+" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "98765432102", new DateTime(1978, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "jose.santos@email.com", "Av. Brasil, 456", "Solteiro", "José Santos", "12345678902", false, "Masculino", "11988880002", "O-" },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "98765432103", new DateTime(1992, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "carla.souza@email.com", "Rua Verde, 789", "Solteira", "Carla Souza", "12345678903", true, "Feminino", "11988880003", "B+" },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "98765432104", new DateTime(1980, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "paulo.lima@email.com", "Rua Azul, 321", "Divorciado", "Paulo Lima", "12345678904", false, "Masculino", "11988880004", "AB-" },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "98765432105", new DateTime(1995, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "fernanda.costa@email.com", "Av. Central, 654", "Casada", "Fernanda Costa", "12345678905", true, "Feminino", "11988880005", "O+" },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "98765432106", new DateTime(1988, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucas.martins@email.com", "Rua Amarela, 987", "Solteiro", "Lucas Martins", "12345678906", false, "Masculino", "11988880006", "A-" },
                    { new Guid("30000000-0000-0000-0000-000000000007"), "98765432107", new DateTime(1975, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "patricia.mendes@email.com", "Rua das Palmeiras, 159", "Viúva", "Patrícia Mendes", "12345678907", true, "Feminino", "11988880007", "B-" },
                    { new Guid("30000000-0000-0000-0000-000000000008"), "98765432108", new DateTime(1990, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ricardo.alves@email.com", "Av. Paulista, 753", "Casado", "Ricardo Alves", "12345678908", false, "Masculino", "11988880008", "AB+" },
                    { new Guid("30000000-0000-0000-0000-000000000009"), "98765432109", new DateTime(1982, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "juliana.rocha@email.com", "Rua do Sol, 852", "Solteira", "Juliana Rocha", "12345678909", true, "Feminino", "11988880009", "A+" },
                    { new Guid("30000000-0000-0000-0000-00000000000a"), "98765432110", new DateTime(1970, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "andre.barbosa@email.com", "Rua das Acácias, 951", "Casado", "André Barbosa", "12345678910", false, "Masculino", "11988880010", "O-" }
                });

            migrationBuilder.InsertData(
                table: "Profissionais",
                columns: new[] { "Id", "Ativo", "CPF", "CargaHorariaSemanal", "DataAdmissao", "Email", "EspecialidadeId", "NomeCompleto", "RegistroConselho", "Telefone", "TipoRegistro", "Turno" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), true, "12345678901", 40, new DateTime(2015, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.silva@hospital.com", new Guid("10000000-0000-0000-0000-000000000001"), "Dr. Carlos Silva", "CRM12345", "11999990001", "CRM", "Manhã" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), true, "12345678902", 36, new DateTime(2017, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana.souza@hospital.com", new Guid("10000000-0000-0000-0000-000000000002"), "Dra. Ana Souza", "CRM23456", "11999990002", "CRM", "Tarde" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), true, "12345678903", 40, new DateTime(2018, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.pereira@hospital.com", new Guid("10000000-0000-0000-0000-000000000003"), "Dr. João Pereira", "CRM34567", "11999990003", "CRM", "Noite" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), true, "12345678904", 30, new DateTime(2016, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "beatriz.lima@hospital.com", new Guid("10000000-0000-0000-0000-000000000004"), "Dra. Beatriz Lima", "CRM45678", "11999990004", "CRM", "Manhã" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), true, "12345678905", 40, new DateTime(2019, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedro.ramos@hospital.com", new Guid("10000000-0000-0000-0000-000000000005"), "Dr. Pedro Ramos", "CRM56789", "11999990005", "CRM", "Tarde" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), true, "12345678906", 36, new DateTime(2020, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "marina.castro@hospital.com", new Guid("10000000-0000-0000-0000-000000000006"), "Dra. Marina Castro", "CRM67890", "11999990006", "CRM", "Noite" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), true, "12345678907", 40, new DateTime(2014, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucas.alves@hospital.com", new Guid("10000000-0000-0000-0000-000000000007"), "Dr. Lucas Alves", "CRM78901", "11999990007", "CRM", "Manhã" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), true, "12345678908", 36, new DateTime(2013, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "paula.mendes@hospital.com", new Guid("10000000-0000-0000-0000-000000000008"), "Dra. Paula Mendes", "CRM89012", "11999990008", "CRM", "Tarde" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), true, "12345678909", 40, new DateTime(2012, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "rafael.costa@hospital.com", new Guid("10000000-0000-0000-0000-000000000009"), "Dr. Rafael Costa", "CRM90123", "11999990009", "CRM", "Noite" },
                    { new Guid("20000000-0000-0000-0000-00000000000a"), true, "12345678910", 36, new DateTime(2021, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "fernanda.dias@hospital.com", new Guid("10000000-0000-0000-0000-00000000000a"), "Dra. Fernanda Dias", "CRM01234", "11999990010", "CRM", "Manhã" }
                });

            migrationBuilder.InsertData(
                table: "Prontuarios",
                columns: new[] { "Id", "DataAbertura", "Numero", "ObservacoesGerais", "PacienteId" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-001", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000001") },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-002", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000002") },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-003", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000003") },
                    { new Guid("40000000-0000-0000-0000-000000000004"), new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-004", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000004") },
                    { new Guid("40000000-0000-0000-0000-000000000005"), new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-005", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000005") },
                    { new Guid("40000000-0000-0000-0000-000000000006"), new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-006", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000006") },
                    { new Guid("40000000-0000-0000-0000-000000000007"), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-007", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000007") },
                    { new Guid("40000000-0000-0000-0000-000000000008"), new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-008", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000008") },
                    { new Guid("40000000-0000-0000-0000-000000000009"), new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-009", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-000000000009") },
                    { new Guid("40000000-0000-0000-0000-000000000010"), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRNT-2023-010", "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", new Guid("30000000-0000-0000-0000-00000000000a") }
                });

            migrationBuilder.InsertData(
                table: "Atendimentos",
                columns: new[] { "Id", "DataHora", "Local", "ProfissionalId", "ProntuarioId", "Status", "Tipo" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2024, 4, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), "Sala 1", new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), "Realizado", "Emergência" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2024, 4, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Sala 2", new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002"), "Em andamento", "Consulta" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2024, 4, 3, 10, 30, 0, 0, DateTimeKind.Unspecified), "Sala 3", new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000003"), "Realizado", "Internação" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2024, 4, 4, 11, 30, 0, 0, DateTimeKind.Unspecified), "Sala 4", new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004"), "Em andamento", "Emergência" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2024, 4, 5, 12, 30, 0, 0, DateTimeKind.Unspecified), "Sala 5", new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000005"), "Realizado", "Consulta" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(2024, 4, 6, 13, 30, 0, 0, DateTimeKind.Unspecified), "Sala 6", new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000006"), "Em andamento", "Internação" },
                    { new Guid("50000000-0000-0000-0000-000000000007"), new DateTime(2024, 4, 7, 14, 30, 0, 0, DateTimeKind.Unspecified), "Sala 7", new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000007"), "Realizado", "Emergência" },
                    { new Guid("50000000-0000-0000-0000-000000000008"), new DateTime(2024, 4, 8, 15, 30, 0, 0, DateTimeKind.Unspecified), "Sala 8", new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000008"), "Em andamento", "Consulta" },
                    { new Guid("50000000-0000-0000-0000-000000000009"), new DateTime(2024, 4, 9, 16, 30, 0, 0, DateTimeKind.Unspecified), "Sala 9", new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000009"), "Realizado", "Internação" },
                    { new Guid("50000000-0000-0000-0000-000000000010"), new DateTime(2024, 4, 10, 17, 30, 0, 0, DateTimeKind.Unspecified), "Sala 10", new Guid("20000000-0000-0000-0000-00000000000a"), new Guid("40000000-0000-0000-0000-000000000010"), "Em andamento", "Emergência" }
                });

            migrationBuilder.InsertData(
                table: "Exames",
                columns: new[] { "Id", "AtendimentoId", "DataRealizacao", "DataSolicitacao", "Resultado", "Tipo" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentro da normalidade", "Hemograma" },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteração detectada", "Raio-X" },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentro da normalidade", "Hemograma" },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteração detectada", "Raio-X" },
                    { new Guid("70000000-0000-0000-0000-000000000005"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentro da normalidade", "Hemograma" },
                    { new Guid("70000000-0000-0000-0000-000000000006"), new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteração detectada", "Raio-X" },
                    { new Guid("70000000-0000-0000-0000-000000000007"), new Guid("50000000-0000-0000-0000-000000000007"), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentro da normalidade", "Hemograma" },
                    { new Guid("70000000-0000-0000-0000-000000000008"), new Guid("50000000-0000-0000-0000-000000000008"), new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteração detectada", "Raio-X" },
                    { new Guid("70000000-0000-0000-0000-000000000009"), new Guid("50000000-0000-0000-0000-000000000009"), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentro da normalidade", "Hemograma" },
                    { new Guid("70000000-0000-0000-0000-000000000010"), new Guid("50000000-0000-0000-0000-000000000010"), new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteração detectada", "Raio-X" }
                });

            migrationBuilder.InsertData(
                table: "Internacoes",
                columns: new[] { "Id", "AtendimentoId", "DataEntrada", "Leito", "MotivoInternacao", "ObservacoesClinicas", "PacienteId", "PlanoSaudeUtilizado", "PrevisaoAlta", "Quarto", "Setor", "StatusInternacao" },
                values: new object[,]
                {
                    { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2024, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "L1", "Pós-operatório", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000001"), "Unimed", new DateTime(2024, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q1", "Clínica Geral", "Ativa" },
                    { new Guid("80000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2024, 4, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "L2", "Infecção", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000002"), null, new DateTime(2024, 4, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q2", "UTI", "Alta concedida" },
                    { new Guid("80000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2024, 4, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), "L3", "Pós-operatório", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000003"), "Unimed", new DateTime(2024, 4, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q3", "Clínica Geral", "Ativa" },
                    { new Guid("80000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2024, 4, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "L4", "Infecção", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000004"), null, new DateTime(2024, 4, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q4", "UTI", "Alta concedida" },
                    { new Guid("80000000-0000-0000-0000-000000000005"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2024, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "L5", "Pós-operatório", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000005"), "Unimed", new DateTime(2024, 4, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q5", "Clínica Geral", "Ativa" },
                    { new Guid("80000000-0000-0000-0000-000000000006"), new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(2024, 4, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "L6", "Infecção", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000006"), null, new DateTime(2024, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q6", "UTI", "Alta concedida" },
                    { new Guid("80000000-0000-0000-0000-000000000007"), new Guid("50000000-0000-0000-0000-000000000007"), new DateTime(2024, 4, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), "L7", "Pós-operatório", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000007"), "Unimed", new DateTime(2024, 4, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q7", "Clínica Geral", "Ativa" },
                    { new Guid("80000000-0000-0000-0000-000000000008"), new Guid("50000000-0000-0000-0000-000000000008"), new DateTime(2024, 4, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "L8", "Infecção", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000008"), null, new DateTime(2024, 4, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q8", "UTI", "Alta concedida" },
                    { new Guid("80000000-0000-0000-0000-000000000009"), new Guid("50000000-0000-0000-0000-000000000009"), new DateTime(2024, 4, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), "L9", "Pós-operatório", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-000000000009"), "Unimed", new DateTime(2024, 4, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q9", "Clínica Geral", "Ativa" },
                    { new Guid("80000000-0000-0000-0000-000000000010"), new Guid("50000000-0000-0000-0000-000000000010"), new DateTime(2024, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "L10", "Infecção", "Paciente estável, monitoramento contínuo.", new Guid("30000000-0000-0000-0000-00000000000a"), null, new DateTime(2024, 4, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Q10", "UTI", "Alta concedida" }
                });

            migrationBuilder.InsertData(
                table: "Prescricoes",
                columns: new[] { "Id", "AtendimentoId", "DataFim", "DataInicio", "Dosagem", "Frequencia", "Medicamento", "Observacoes", "ProfissionalId", "ReacoesAdversas", "StatusPrescricao", "ViaAdministracao" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "500mg", "8/8h", "Dipirona", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000001"), null, "Ativa", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000002"), new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "875mg", "12/12h", "Amoxicilina", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000002"), "Nenhuma", "Encerrada", "Intravenosa" },
                    { new Guid("60000000-0000-0000-0000-000000000003"), new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "750mg", "6/6h", "Paracetamol", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000003"), null, "Ativa", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000004"), new Guid("50000000-0000-0000-0000-000000000004"), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "400mg", "8/8h", "Ibuprofeno", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000004"), "Nenhuma", "Encerrada", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000005"), new Guid("50000000-0000-0000-0000-000000000005"), new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "500mg", "6/6h", "Cefalexina", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000005"), null, "Ativa", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000006"), new Guid("50000000-0000-0000-0000-000000000006"), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "500mg", "24/24h", "Azitromicina", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000006"), "Nenhuma", "Encerrada", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000007"), new Guid("50000000-0000-0000-0000-000000000007"), new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "850mg", "12/12h", "Metformina", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000007"), null, "Ativa", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000008"), new Guid("50000000-0000-0000-0000-000000000008"), new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "50mg", "24/24h", "Losartana", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000008"), "Nenhuma", "Encerrada", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000009"), new Guid("50000000-0000-0000-0000-000000000009"), new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "10mg", "12/12h", "Enalapril", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-000000000009"), null, "Ativa", "Oral" },
                    { new Guid("60000000-0000-0000-0000-000000000010"), new Guid("50000000-0000-0000-0000-000000000010"), new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "20mg", "24/24h", "Omeprazol", "Administrar conforme prescrição médica.", new Guid("20000000-0000-0000-0000-00000000000a"), "Nenhuma", "Encerrada", "Oral" }
                });

            migrationBuilder.InsertData(
                table: "AltasHospitalares",
                columns: new[] { "Id", "CondicaoPaciente", "Data", "Instrucoes", "InternacaoId" },
                values: new object[,]
                {
                    { new Guid("90000000-0000-0000-0000-000000000001"), "Recuperado", new DateTime(2024, 4, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000001") },
                    { new Guid("90000000-0000-0000-0000-000000000002"), "Em acompanhamento", new DateTime(2024, 4, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000002") },
                    { new Guid("90000000-0000-0000-0000-000000000003"), "Recuperado", new DateTime(2024, 4, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000003") },
                    { new Guid("90000000-0000-0000-0000-000000000004"), "Em acompanhamento", new DateTime(2024, 4, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000004") },
                    { new Guid("90000000-0000-0000-0000-000000000005"), "Recuperado", new DateTime(2024, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000005") },
                    { new Guid("90000000-0000-0000-0000-000000000006"), "Em acompanhamento", new DateTime(2024, 4, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000006") },
                    { new Guid("90000000-0000-0000-0000-000000000007"), "Recuperado", new DateTime(2024, 4, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000007") },
                    { new Guid("90000000-0000-0000-0000-000000000008"), "Em acompanhamento", new DateTime(2024, 4, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000008") },
                    { new Guid("90000000-0000-0000-0000-000000000009"), "Recuperado", new DateTime(2024, 4, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000009") },
                    { new Guid("90000000-0000-0000-0000-000000000010"), "Em acompanhamento", new DateTime(2024, 4, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Retornar em 7 dias para reavaliação.", new Guid("80000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltasHospitalares_InternacaoId",
                table: "AltasHospitalares",
                column: "InternacaoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProfissionalId",
                table: "Atendimentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProntuarioId",
                table: "Atendimentos",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_AtendimentoId",
                table: "Exames",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Internacoes_AtendimentoId",
                table: "Internacoes",
                column: "AtendimentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Internacoes_PacienteId",
                table: "Internacoes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_AtendimentoId",
                table: "Prescricoes",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_ProfissionalId",
                table: "Prescricoes",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_EspecialidadeId",
                table: "Profissionais",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltasHospitalares");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Prescricoes");

            migrationBuilder.DropTable(
                name: "Internacoes");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "Prontuarios");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}

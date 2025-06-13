using Microsoft.EntityFrameworkCore;
using Hospisim.Models;

namespace Hospisim.Data
{
    public class HospisimContext : DbContext
    {
        public HospisimContext(DbContextOptions<HospisimContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamentos
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Internacoes)
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Atendimento)
                .WithOne(a => a.Internacao)
                .HasForeignKey<Internacao>(i => i.AtendimentoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.AltaHospitalar)
                .WithOne(a => a.Internacao)
                .HasForeignKey<AltaHospitalar>(a => a.InternacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Profissional)
                .WithMany(pr => pr.Prescricoes)
                .HasForeignKey(p => p.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Torna PlanoSaudeUtilizado opcional
            modelBuilder.Entity<Internacao>()
                .Property(i => i.PlanoSaudeUtilizado)
                .IsRequired(false);

            // ESPECIALIDADES
            var especialidades = new[]
            {
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Nome = "Cardiologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Nome = "Pediatria" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Nome = "Ortopedia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Nome = "Neurologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Nome = "Ginecologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Nome = "Oncologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Nome = "Dermatologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Nome = "Psiquiatria" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Nome = "Endocrinologia" },
                new Especialidade { Id = Guid.Parse("10000000-0000-0000-0000-00000000000a"), Nome = "Urologia" }
            };
            modelBuilder.Entity<Especialidade>().HasData(especialidades);

            // PROFISSIONAIS
            var profissionais = new[]
            {
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), NomeCompleto = "Dr. Carlos Silva", CPF = "12345678901", Email = "carlos.silva@hospital.com", Telefone = "11999990001", RegistroConselho = "CRM12345", TipoRegistro = "CRM", EspecialidadeId = especialidades[0].Id, DataAdmissao = new DateTime(2015, 3, 10), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), NomeCompleto = "Dra. Ana Souza", CPF = "12345678902", Email = "ana.souza@hospital.com", Telefone = "11999990002", RegistroConselho = "CRM23456", TipoRegistro = "CRM", EspecialidadeId = especialidades[1].Id, DataAdmissao = new DateTime(2017, 5, 20), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), NomeCompleto = "Dr. João Pereira", CPF = "12345678903", Email = "joao.pereira@hospital.com", Telefone = "11999990003", RegistroConselho = "CRM34567", TipoRegistro = "CRM", EspecialidadeId = especialidades[2].Id, DataAdmissao = new DateTime(2018, 8, 15), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), NomeCompleto = "Dra. Beatriz Lima", CPF = "12345678904", Email = "beatriz.lima@hospital.com", Telefone = "11999990004", RegistroConselho = "CRM45678", TipoRegistro = "CRM", EspecialidadeId = especialidades[3].Id, DataAdmissao = new DateTime(2016, 2, 1), CargaHorariaSemanal = 30, Turno = "Manhã", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), NomeCompleto = "Dr. Pedro Ramos", CPF = "12345678905", Email = "pedro.ramos@hospital.com", Telefone = "11999990005", RegistroConselho = "CRM56789", TipoRegistro = "CRM", EspecialidadeId = especialidades[4].Id, DataAdmissao = new DateTime(2019, 11, 10), CargaHorariaSemanal = 40, Turno = "Tarde", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000006"), NomeCompleto = "Dra. Marina Castro", CPF = "12345678906", Email = "marina.castro@hospital.com", Telefone = "11999990006", RegistroConselho = "CRM67890", TipoRegistro = "CRM", EspecialidadeId = especialidades[5].Id, DataAdmissao = new DateTime(2020, 1, 5), CargaHorariaSemanal = 36, Turno = "Noite", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000007"), NomeCompleto = "Dr. Lucas Alves", CPF = "12345678907", Email = "lucas.alves@hospital.com", Telefone = "11999990007", RegistroConselho = "CRM78901", TipoRegistro = "CRM", EspecialidadeId = especialidades[6].Id, DataAdmissao = new DateTime(2014, 7, 22), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000008"), NomeCompleto = "Dra. Paula Mendes", CPF = "12345678908", Email = "paula.mendes@hospital.com", Telefone = "11999990008", RegistroConselho = "CRM89012", TipoRegistro = "CRM", EspecialidadeId = especialidades[7].Id, DataAdmissao = new DateTime(2013, 9, 30), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-000000000009"), NomeCompleto = "Dr. Rafael Costa", CPF = "12345678909", Email = "rafael.costa@hospital.com", Telefone = "11999990009", RegistroConselho = "CRM90123", TipoRegistro = "CRM", EspecialidadeId = especialidades[8].Id, DataAdmissao = new DateTime(2012, 4, 18), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new Profissional { Id = Guid.Parse("20000000-0000-0000-0000-00000000000a"), NomeCompleto = "Dra. Fernanda Dias", CPF = "12345678910", Email = "fernanda.dias@hospital.com", Telefone = "11999990010", RegistroConselho = "CRM01234", TipoRegistro = "CRM", EspecialidadeId = especialidades[9].Id, DataAdmissao = new DateTime(2021, 6, 12), CargaHorariaSemanal = 36, Turno = "Manhã", Ativo = true }
            };
            modelBuilder.Entity<Profissional>().HasData(profissionais);

            // PACIENTES
            var pacientes = new[]
            {
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000001"), NomeCompleto = "Maria Oliveira", CPF = "98765432101", DataNascimento = new DateTime(1985, 4, 12), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "11988880001", Email = "maria.oliveira@email.com", EnderecoCompleto = "Rua das Flores, 123", NumeroCartaoSUS = "12345678901", EstadoCivil = "Casada", PossuiPlanoSaude = true },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000002"), NomeCompleto = "José Santos", CPF = "98765432102", DataNascimento = new DateTime(1978, 9, 23), Sexo = "Masculino", TipoSanguineo = "O-", Telefone = "11988880002", Email = "jose.santos@email.com", EnderecoCompleto = "Av. Brasil, 456", NumeroCartaoSUS = "12345678902", EstadoCivil = "Solteiro", PossuiPlanoSaude = false },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000003"), NomeCompleto = "Carla Souza", CPF = "98765432103", DataNascimento = new DateTime(1992, 2, 5), Sexo = "Feminino", TipoSanguineo = "B+", Telefone = "11988880003", Email = "carla.souza@email.com", EnderecoCompleto = "Rua Verde, 789", NumeroCartaoSUS = "12345678903", EstadoCivil = "Solteira", PossuiPlanoSaude = true },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000004"), NomeCompleto = "Paulo Lima", CPF = "98765432104", DataNascimento = new DateTime(1980, 7, 19), Sexo = "Masculino", TipoSanguineo = "AB-", Telefone = "11988880004", Email = "paulo.lima@email.com", EnderecoCompleto = "Rua Azul, 321", NumeroCartaoSUS = "12345678904", EstadoCivil = "Divorciado", PossuiPlanoSaude = false },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000005"), NomeCompleto = "Fernanda Costa", CPF = "98765432105", DataNascimento = new DateTime(1995, 11, 30), Sexo = "Feminino", TipoSanguineo = "O+", Telefone = "11988880005", Email = "fernanda.costa@email.com", EnderecoCompleto = "Av. Central, 654", NumeroCartaoSUS = "12345678905", EstadoCivil = "Casada", PossuiPlanoSaude = true },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000006"), NomeCompleto = "Lucas Martins", CPF = "98765432106", DataNascimento = new DateTime(1988, 3, 14), Sexo = "Masculino", TipoSanguineo = "A-", Telefone = "11988880006", Email = "lucas.martins@email.com", EnderecoCompleto = "Rua Amarela, 987", NumeroCartaoSUS = "12345678906", EstadoCivil = "Solteiro", PossuiPlanoSaude = false },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000007"), NomeCompleto = "Patrícia Mendes", CPF = "98765432107", DataNascimento = new DateTime(1975, 6, 8), Sexo = "Feminino", TipoSanguineo = "B-", Telefone = "11988880007", Email = "patricia.mendes@email.com", EnderecoCompleto = "Rua das Palmeiras, 159", NumeroCartaoSUS = "12345678907", EstadoCivil = "Viúva", PossuiPlanoSaude = true },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000008"), NomeCompleto = "Ricardo Alves", CPF = "98765432108", DataNascimento = new DateTime(1990, 12, 25), Sexo = "Masculino", TipoSanguineo = "AB+", Telefone = "11988880008", Email = "ricardo.alves@email.com", EnderecoCompleto = "Av. Paulista, 753", NumeroCartaoSUS = "12345678908", EstadoCivil = "Casado", PossuiPlanoSaude = false },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-000000000009"), NomeCompleto = "Juliana Rocha", CPF = "98765432109", DataNascimento = new DateTime(1982, 10, 2), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "11988880009", Email = "juliana.rocha@email.com", EnderecoCompleto = "Rua do Sol, 852", NumeroCartaoSUS = "12345678909", EstadoCivil = "Solteira", PossuiPlanoSaude = true },
                new Paciente { Id = Guid.Parse("30000000-0000-0000-0000-00000000000a"), NomeCompleto = "André Barbosa", CPF = "98765432110", DataNascimento = new DateTime(1970, 1, 17), Sexo = "Masculino", TipoSanguineo = "O-", Telefone = "11988880010", Email = "andre.barbosa@email.com", EnderecoCompleto = "Rua das Acácias, 951", NumeroCartaoSUS = "12345678910", EstadoCivil = "Casado", PossuiPlanoSaude = false }
            };
            modelBuilder.Entity<Paciente>().HasData(pacientes);
            // PRONTUÁRIOS
            var prontuarios = new[]
            {
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000001"), Numero = "PRNT-2023-001", DataAbertura = new DateTime(2023, 1, 1), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[0].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000002"), Numero = "PRNT-2023-002", DataAbertura = new DateTime(2023, 1, 2), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[1].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000003"), Numero = "PRNT-2023-003", DataAbertura = new DateTime(2023, 1, 3), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[2].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000004"), Numero = "PRNT-2023-004", DataAbertura = new DateTime(2023, 1, 4), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[3].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000005"), Numero = "PRNT-2023-005", DataAbertura = new DateTime(2023, 1, 5), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[4].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000006"), Numero = "PRNT-2023-006", DataAbertura = new DateTime(2023, 1, 6), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[5].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000007"), Numero = "PRNT-2023-007", DataAbertura = new DateTime(2023, 1, 7), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[6].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000008"), Numero = "PRNT-2023-008", DataAbertura = new DateTime(2023, 1, 8), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[7].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000009"), Numero = "PRNT-2023-009", DataAbertura = new DateTime(2023, 1, 9), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[8].Id },
                new Prontuario { Id = Guid.Parse("40000000-0000-0000-0000-000000000010"), Numero = "PRNT-2023-010", DataAbertura = new DateTime(2023, 1, 10), ObservacoesGerais = "Paciente apresenta histórico de consultas regulares. Prontuário aberto em 2023.", PacienteId = pacientes[9].Id }
            };
            modelBuilder.Entity<Prontuario>().HasData(prontuarios);

            // ATENDIMENTOS
            var atendimentos = new[]
            {
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000001"), DataHora = new DateTime(2024, 4, 1, 8, 30, 0), Tipo = "Emergência", Status = "Realizado", Local = "Sala 1", ProntuarioId = prontuarios[0].Id, ProfissionalId = profissionais[0].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000002"), DataHora = new DateTime(2024, 4, 2, 9, 30, 0), Tipo = "Consulta", Status = "Em andamento", Local = "Sala 2", ProntuarioId = prontuarios[1].Id, ProfissionalId = profissionais[1].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000003"), DataHora = new DateTime(2024, 4, 3, 10, 30, 0), Tipo = "Internação", Status = "Realizado", Local = "Sala 3", ProntuarioId = prontuarios[2].Id, ProfissionalId = profissionais[2].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000004"), DataHora = new DateTime(2024, 4, 4, 11, 30, 0), Tipo = "Emergência", Status = "Em andamento", Local = "Sala 4", ProntuarioId = prontuarios[3].Id, ProfissionalId = profissionais[3].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000005"), DataHora = new DateTime(2024, 4, 5, 12, 30, 0), Tipo = "Consulta", Status = "Realizado", Local = "Sala 5", ProntuarioId = prontuarios[4].Id, ProfissionalId = profissionais[4].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000006"), DataHora = new DateTime(2024, 4, 6, 13, 30, 0), Tipo = "Internação", Status = "Em andamento", Local = "Sala 6", ProntuarioId = prontuarios[5].Id, ProfissionalId = profissionais[5].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000007"), DataHora = new DateTime(2024, 4, 7, 14, 30, 0), Tipo = "Emergência", Status = "Realizado", Local = "Sala 7", ProntuarioId = prontuarios[6].Id, ProfissionalId = profissionais[6].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000008"), DataHora = new DateTime(2024, 4, 8, 15, 30, 0), Tipo = "Consulta", Status = "Em andamento", Local = "Sala 8", ProntuarioId = prontuarios[7].Id, ProfissionalId = profissionais[7].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000009"), DataHora = new DateTime(2024, 4, 9, 16, 30, 0), Tipo = "Internação", Status = "Realizado", Local = "Sala 9", ProntuarioId = prontuarios[8].Id, ProfissionalId = profissionais[8].Id },
            new Atendimento { Id = Guid.Parse("50000000-0000-0000-0000-000000000010"), DataHora = new DateTime(2024, 4, 10, 17, 30, 0), Tipo = "Emergência", Status = "Em andamento", Local = "Sala 10", ProntuarioId = prontuarios[9].Id, ProfissionalId = profissionais[9].Id }
            };
            modelBuilder.Entity<Atendimento>().HasData(atendimentos);

            // PRESCRIÇÕES
            var prescricoes = new[]
            {
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000001"), AtendimentoId = atendimentos[0].Id, ProfissionalId = profissionais[0].Id, Medicamento = "Dipirona", Dosagem = "500mg", Frequencia = "8/8h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 1), DataFim = new DateTime(2024, 4, 5), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Ativa", ReacoesAdversas = null },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000002"), AtendimentoId = atendimentos[1].Id, ProfissionalId = profissionais[1].Id, Medicamento = "Amoxicilina", Dosagem = "875mg", Frequencia = "12/12h", ViaAdministracao = "Intravenosa", DataInicio = new DateTime(2024, 4, 2), DataFim = new DateTime(2024, 4, 6), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Encerrada", ReacoesAdversas = "Nenhuma" },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000003"), AtendimentoId = atendimentos[2].Id, ProfissionalId = profissionais[2].Id, Medicamento = "Paracetamol", Dosagem = "750mg", Frequencia = "6/6h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 3), DataFim = new DateTime(2024, 4, 7), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Ativa", ReacoesAdversas = null },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000004"), AtendimentoId = atendimentos[3].Id, ProfissionalId = profissionais[3].Id, Medicamento = "Ibuprofeno", Dosagem = "400mg", Frequencia = "8/8h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 4), DataFim = new DateTime(2024, 4, 8), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Encerrada", ReacoesAdversas = "Nenhuma" },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000005"), AtendimentoId = atendimentos[4].Id, ProfissionalId = profissionais[4].Id, Medicamento = "Cefalexina", Dosagem = "500mg", Frequencia = "6/6h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 5), DataFim = new DateTime(2024, 4, 9), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Ativa", ReacoesAdversas = null },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000006"), AtendimentoId = atendimentos[5].Id, ProfissionalId = profissionais[5].Id, Medicamento = "Azitromicina", Dosagem = "500mg", Frequencia = "24/24h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 6), DataFim = new DateTime(2024, 4, 10), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Encerrada", ReacoesAdversas = "Nenhuma" },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000007"), AtendimentoId = atendimentos[6].Id, ProfissionalId = profissionais[6].Id, Medicamento = "Metformina", Dosagem = "850mg", Frequencia = "12/12h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 7), DataFim = new DateTime(2024, 4, 11), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Ativa", ReacoesAdversas = null },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000008"), AtendimentoId = atendimentos[7].Id, ProfissionalId = profissionais[7].Id, Medicamento = "Losartana", Dosagem = "50mg", Frequencia = "24/24h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 8), DataFim = new DateTime(2024, 4, 12), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Encerrada", ReacoesAdversas = "Nenhuma" },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000009"), AtendimentoId = atendimentos[8].Id, ProfissionalId = profissionais[8].Id, Medicamento = "Enalapril", Dosagem = "10mg", Frequencia = "12/12h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 9), DataFim = new DateTime(2024, 4, 13), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Ativa", ReacoesAdversas = null },
            new Prescricao { Id = Guid.Parse("60000000-0000-0000-0000-000000000010"), AtendimentoId = atendimentos[9].Id, ProfissionalId = profissionais[9].Id, Medicamento = "Omeprazol", Dosagem = "20mg", Frequencia = "24/24h", ViaAdministracao = "Oral", DataInicio = new DateTime(2024, 4, 10), DataFim = new DateTime(2024, 4, 14), Observacoes = "Administrar conforme prescrição médica.", StatusPrescricao = "Encerrada", ReacoesAdversas = "Nenhuma" }
            };
            modelBuilder.Entity<Prescricao>().HasData(prescricoes);

            // EXAMES
            var exames = new[]
            {
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000001"), AtendimentoId = atendimentos[0].Id, Tipo = "Hemograma", DataSolicitacao = new DateTime(2024, 4, 1), DataRealizacao = new DateTime(2024, 4, 2), Resultado = "Dentro da normalidade" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000002"), AtendimentoId = atendimentos[1].Id, Tipo = "Raio-X", DataSolicitacao = new DateTime(2024, 4, 2), DataRealizacao = new DateTime(2024, 4, 3), Resultado = "Alteração detectada" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000003"), AtendimentoId = atendimentos[2].Id, Tipo = "Hemograma", DataSolicitacao = new DateTime(2024, 4, 3), DataRealizacao = new DateTime(2024, 4, 4), Resultado = "Dentro da normalidade" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000004"), AtendimentoId = atendimentos[3].Id, Tipo = "Raio-X", DataSolicitacao = new DateTime(2024, 4, 4), DataRealizacao = new DateTime(2024, 4, 5), Resultado = "Alteração detectada" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000005"), AtendimentoId = atendimentos[4].Id, Tipo = "Hemograma", DataSolicitacao = new DateTime(2024, 4, 5), DataRealizacao = new DateTime(2024, 4, 6), Resultado = "Dentro da normalidade" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000006"), AtendimentoId = atendimentos[5].Id, Tipo = "Raio-X", DataSolicitacao = new DateTime(2024, 4, 6), DataRealizacao = new DateTime(2024, 4, 7), Resultado = "Alteração detectada" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000007"), AtendimentoId = atendimentos[6].Id, Tipo = "Hemograma", DataSolicitacao = new DateTime(2024, 4, 7), DataRealizacao = new DateTime(2024, 4, 8), Resultado = "Dentro da normalidade" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000008"), AtendimentoId = atendimentos[7].Id, Tipo = "Raio-X", DataSolicitacao = new DateTime(2024, 4, 8), DataRealizacao = new DateTime(2024, 4, 9), Resultado = "Alteração detectada" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000009"), AtendimentoId = atendimentos[8].Id, Tipo = "Hemograma", DataSolicitacao = new DateTime(2024, 4, 9), DataRealizacao = new DateTime(2024, 4, 10), Resultado = "Dentro da normalidade" },
            new Exame { Id = Guid.Parse("70000000-0000-0000-0000-000000000010"), AtendimentoId = atendimentos[9].Id, Tipo = "Raio-X", DataSolicitacao = new DateTime(2024, 4, 10), DataRealizacao = new DateTime(2024, 4, 11), Resultado = "Alteração detectada" }
            };
            modelBuilder.Entity<Exame>().HasData(exames);

            // INTERNAÇÕES
            var internacoes = new[]
            {
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000001"), PacienteId = pacientes[0].Id, AtendimentoId = atendimentos[0].Id, DataEntrada = new DateTime(2024, 4, 1, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 5, 10, 0, 0), MotivoInternacao = "Pós-operatório", Leito = "L1", Quarto = "Q1", Setor = "Clínica Geral", PlanoSaudeUtilizado = "Unimed", ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Ativa" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000002"), PacienteId = pacientes[1].Id, AtendimentoId = atendimentos[1].Id, DataEntrada = new DateTime(2024, 4, 2, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 6, 10, 0, 0), MotivoInternacao = "Infecção", Leito = "L2", Quarto = "Q2", Setor = "UTI", PlanoSaudeUtilizado = null, ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Alta concedida" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000003"), PacienteId = pacientes[2].Id, AtendimentoId = atendimentos[2].Id, DataEntrada = new DateTime(2024, 4, 3, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 7, 10, 0, 0), MotivoInternacao = "Pós-operatório", Leito = "L3", Quarto = "Q3", Setor = "Clínica Geral", PlanoSaudeUtilizado = "Unimed", ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Ativa" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000004"), PacienteId = pacientes[3].Id, AtendimentoId = atendimentos[3].Id, DataEntrada = new DateTime(2024, 4, 4, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 8, 10, 0, 0), MotivoInternacao = "Infecção", Leito = "L4", Quarto = "Q4", Setor = "UTI", PlanoSaudeUtilizado = null, ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Alta concedida" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000005"), PacienteId = pacientes[4].Id, AtendimentoId = atendimentos[4].Id, DataEntrada = new DateTime(2024, 4, 5, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 9, 10, 0, 0), MotivoInternacao = "Pós-operatório", Leito = "L5", Quarto = "Q5", Setor = "Clínica Geral", PlanoSaudeUtilizado = "Unimed", ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Ativa" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000006"), PacienteId = pacientes[5].Id, AtendimentoId = atendimentos[5].Id, DataEntrada = new DateTime(2024, 4, 6, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 10, 10, 0, 0), MotivoInternacao = "Infecção", Leito = "L6", Quarto = "Q6", Setor = "UTI", PlanoSaudeUtilizado = null, ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Alta concedida" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000007"), PacienteId = pacientes[6].Id, AtendimentoId = atendimentos[6].Id, DataEntrada = new DateTime(2024, 4, 7, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 11, 10, 0, 0), MotivoInternacao = "Pós-operatório", Leito = "L7", Quarto = "Q7", Setor = "Clínica Geral", PlanoSaudeUtilizado = "Unimed", ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Ativa" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000008"), PacienteId = pacientes[7].Id, AtendimentoId = atendimentos[7].Id, DataEntrada = new DateTime(2024, 4, 8, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 12, 10, 0, 0), MotivoInternacao = "Infecção", Leito = "L8", Quarto = "Q8", Setor = "UTI", PlanoSaudeUtilizado = null, ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Alta concedida" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000009"), PacienteId = pacientes[8].Id, AtendimentoId = atendimentos[8].Id, DataEntrada = new DateTime(2024, 4, 9, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 13, 10, 0, 0), MotivoInternacao = "Pós-operatório", Leito = "L9", Quarto = "Q9", Setor = "Clínica Geral", PlanoSaudeUtilizado = "Unimed", ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Ativa" },
            new Internacao { Id = Guid.Parse("80000000-0000-0000-0000-000000000010"), PacienteId = pacientes[9].Id, AtendimentoId = atendimentos[9].Id, DataEntrada = new DateTime(2024, 4, 10, 10, 0, 0), PrevisaoAlta = new DateTime(2024, 4, 14, 10, 0, 0), MotivoInternacao = "Infecção", Leito = "L10", Quarto = "Q10", Setor = "UTI", PlanoSaudeUtilizado = null, ObservacoesClinicas = "Paciente estável, monitoramento contínuo.", StatusInternacao = "Alta concedida" }
            };
            modelBuilder.Entity<Internacao>().HasData(internacoes);

            // ALTAS HOSPITALARES
            var altas = new[]
            {
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000001"), InternacaoId = internacoes[0].Id, Data = new DateTime(2024, 4, 6, 10, 0, 0), CondicaoPaciente = "Recuperado", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000002"), InternacaoId = internacoes[1].Id, Data = new DateTime(2024, 4, 7, 10, 0, 0), CondicaoPaciente = "Em acompanhamento", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000003"), InternacaoId = internacoes[2].Id, Data = new DateTime(2024, 4, 8, 10, 0, 0), CondicaoPaciente = "Recuperado", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000004"), InternacaoId = internacoes[3].Id, Data = new DateTime(2024, 4, 9, 10, 0, 0), CondicaoPaciente = "Em acompanhamento", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000005"), InternacaoId = internacoes[4].Id, Data = new DateTime(2024, 4, 10, 10, 0, 0), CondicaoPaciente = "Recuperado", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000006"), InternacaoId = internacoes[5].Id, Data = new DateTime(2024, 4, 11, 10, 0, 0), CondicaoPaciente = "Em acompanhamento", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000007"), InternacaoId = internacoes[6].Id, Data = new DateTime(2024, 4, 12, 10, 0, 0), CondicaoPaciente = "Recuperado", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000008"), InternacaoId = internacoes[7].Id, Data = new DateTime(2024, 4, 13, 10, 0, 0), CondicaoPaciente = "Em acompanhamento", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000009"), InternacaoId = internacoes[8].Id, Data = new DateTime(2024, 4, 14, 10, 0, 0), CondicaoPaciente = "Recuperado", Instrucoes = "Retornar em 7 dias para reavaliação." },
            new AltaHospitalar { Id = Guid.Parse("90000000-0000-0000-0000-000000000010"), InternacaoId = internacoes[9].Id, Data = new DateTime(2024, 4, 15, 10, 0, 0), CondicaoPaciente = "Em acompanhamento", Instrucoes = "Retornar em 7 dias para reavaliação." }
            };
            modelBuilder.Entity<AltaHospitalar>().HasData(altas);
        }
    }
}
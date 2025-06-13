using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospisim.Models
{
    public class Internacao
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        [Required]
        public Guid AtendimentoId { get; set; }

        [ForeignKey("AtendimentoId")]
        public Atendimento Atendimento { get; set; }

        [Required]
        public DateTime DataEntrada { get; set; }

        public DateTime? PrevisaoAlta { get; set; }

        [Required]
        public string MotivoInternacao { get; set; }

        [Required]
        public string Leito { get; set; }

        [Required]
        public string Quarto { get; set; }

        [Required]
        public string Setor { get; set; }

        public string PlanoSaudeUtilizado { get; set; }

        [Required]
        public string ObservacoesClinicas { get; set; }

        [Required]
        public string StatusInternacao { get; set; }

        public AltaHospitalar AltaHospitalar { get; set; }
    }
}
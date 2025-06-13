using System;
using System.Collections.Generic;

namespace Hospisim.Models
{
    public class Especialidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Profissional> Profissionais { get; set; }
    }
}

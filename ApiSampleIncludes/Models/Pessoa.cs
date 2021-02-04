using System.Collections.Generic;

namespace ApiSampleIncludes.Models
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
       
        public virtual ICollection<ContaFinanceira> ContaFinanceira { get; set; }
    }
}

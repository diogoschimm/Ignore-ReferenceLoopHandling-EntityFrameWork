namespace ApiSampleIncludes.Models
{
    public class ContaFinanceira
    {
        public int ContaFinanceiraId { get; set; }
        public string NomeConta { get; set; }
         
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}

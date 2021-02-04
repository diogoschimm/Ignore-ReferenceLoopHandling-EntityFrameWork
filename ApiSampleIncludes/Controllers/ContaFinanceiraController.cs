using ApiSampleIncludes.Db;
using ApiSampleIncludes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiSampleIncludes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaFinanceiraController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContaFinanceiraController(ApiContext context)
        {
            this._context = context;
        }

        [HttpGet("entity")]
        public IEnumerable<ContaFinanceira> GetAll()
        {
            return this._context.ContaFinanceira.Include(c => c.Pessoa);
        }

        [HttpGet("dynamic")]
        public IEnumerable<dynamic> GetContas()
        {
            return this._context.ContaFinanceira.Include(c => c.Pessoa).Select(c => new
            {
                IdContaCorrente = c.ContaFinanceiraId,
                NomeContaCorrente = c.NomeConta,
                Pessoa = new
                {
                    IdPessoa = c.Pessoa.PessoaId,
                    NomePessoa = c.Pessoa.Nome
                }
            });
        }
    }
}

using ApiSampleIncludes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSampleIncludes.Db
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<ContaFinanceira> ContaFinanceira { get; set; }
    }
}

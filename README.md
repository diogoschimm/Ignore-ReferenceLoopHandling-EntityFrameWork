# Ignore-ReferenceLoopHandling-EntityFrameWork
Projeto de Exemplo para ignorar ReferenceLoopHandling no includes do EntityFramework

Adicione os pacotes do Entity e do JSON  

```xml
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.2" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.2" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.2" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.2" />
  <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="3.0.0" />
```

Criação do DBContext

```c#
  public class ApiContext : DbContext
  {
      public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

      public DbSet<Pessoa> Pessoa { get; set; }
      public DbSet<ContaFinanceira> ContaFinanceira { get; set; }
  }
```

Modelos

```c#
  public class ContaFinanceira
  {
      public int ContaFinanceiraId { get; set; }
      public string NomeConta { get; set; }

      public int PessoaId { get; set; }
      public virtual Pessoa Pessoa { get; set; }
  }
  public class Pessoa
  {
      public int PessoaId { get; set; }
      public string Nome { get; set; }

      public virtual ICollection<ContaFinanceira> ContaFinanceira { get; set; }
  }
```

Método ConfigureServices

```c#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddControllers();
      services.AddDbContext<ApiContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddControllersWithViews()
              .AddNewtonsoftJson(options => {
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                  options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
              });
      ...
```

Controller

```c#
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
```

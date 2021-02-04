# Ignore-ReferenceLoopHandling-EntityFrameWork
Projeto de Exemplo para ignorar ReferenceLoopHandling no includes do EntityFramework

## Erro
An unhandled exception occurred while processing the request.  
JsonException: A possible object cycle was detected which is not supported. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.  
System.Text.Json.ThrowHelper.ThrowInvalidOperationException_SerializerCycleDetected(int maxDepth)  
  
![image](https://user-images.githubusercontent.com/30643035/106830928-62c8f800-6665-11eb-8b38-f26571aaba85.png)


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

## Modelos
  
Criamos o modelo de banco de dados ContaFinanceira e Pessoa, e estamos utilizando a nomenclatura padrão do Entidade <<Entidade>>Id.

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

## Método ConfigureServices  
  
A configuração de AddNewtonsoftJson ReferenceLoopHandling é necessaário somente se devolvermos a própria entidade do banco de dados. Se devolvermos dymac conforme o modelo 2 dai não é necessário colocar essa configuração no ConfigureServices.

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

## Controller
  
A Controller possui dois exemplos, um devolvendo a própria entidade do banco de dados usando o comando Include (se não igorarmos o ReferenceLoopHandling então iremos receber um erro no momento de executar a API).  
O exemplo com o dynamic não gera o Erro de ReferenceLoopHandling pois já estamos determinando exatamente os campos que queremos.  

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

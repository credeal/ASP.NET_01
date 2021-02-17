using Alura.ListaLeitura.App.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
           

            //app.Run(LivrosParaLer);
            var builder = new RouteBuilder(app); //Configurando a rota com asp.net Core
            builder.MapRoute("Livros/ParaLer", LivroLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivroLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivroLogica.LivrosLidos);
            builder.MapRoute("Livro/ParaLer", LivroLogica.LivrosParaLer2);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivroLogica.ExibeDetalhes);//Definindo o tipo de parametro que quero receber
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer);
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }
        
    }
}
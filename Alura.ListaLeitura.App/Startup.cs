﻿using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //app.Run(LivrosParaLer);
            var builder = new RouteBuilder(app); //Configurando a rota com asp.net Core
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Livro/ParaLer", LivrosParaLer2);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);//Definindo o tipo de parametro que quero receber
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        //Fazendo o Roteamento da requisição
        //public Task Roteamento(HttpContext context)
        //{
        //    var _repo = new LivroRepositorioCSV();

        //    var caminhosAtendidos = new Dictionary<string, RequestDelegate>
        //    {
        //        //Método Request Delegate
        //        {"/Livros/ParaLer", LivrosParaLer},
        //        {"/Livros/Lendo", LivrosLendo},
        //        {"/Livros/Lidos", LivrosLidos}
        //    };

        //    if (caminhosAtendidos.ContainsKey(context.Request.Path))
        //    {
        //        var metodo = caminhosAtendidos[context.Request.Path];
        //        return metodo.Invoke(context);
        //    }

        //    context.Response.StatusCode = 404;

        //    return context.Response.WriteAsync("404 NOT FOUND: " + (context.Request.Path));
        //}

        #region Methods

        public Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHTML("Formulario");
            return context.Response.WriteAsync(html);
        }

        public Task ExibeParaLer(HttpContext context)
        {
            var html = CarregaArquivoHTML("Para-Ler");
            return context.Response.WriteAsync(html);

        }

        public string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompleto = $"..\\..\\..\\Html/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompleto))
            {
                return arquivo.ReadToEnd();
            }
        }

        public Task LivrosParaLer2(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudoArq = CarregaArquivoHTML("Para-Ler");

            foreach (var Livro in _repo.ParaLer.Livros)
            {
                conteudoArq = conteudoArq
                    .Replace("#NOVO-ITEM", $"<li>{Livro.Titulo} - {Livro.Autor}</li>#NOVO-ITEM");
            }

            conteudoArq = conteudoArq.Replace("#NOVO-ITEM", "");

            return context.Response.WriteAsync(conteudoArq);

        }

        public Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First()
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso."); 
        }

        public Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("nome").ToString(), //retorna um tipo object
                Autor = context.GetRouteValue("autor").ToString()
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso.");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.ParaLer.ToString());

        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lendo.ToString());

        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lidos.ToString());

        }

        public Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();

            var conteudoArq = CarregaArquivoHTML("Detalhes");

            var livro = repo.Todos.First(l => l.Id == id);

            conteudoArq = conteudoArq.Replace("#NOVO-ITEM", $"<h5>{livro.Titulo} - {livro.Autor}</h5>");

            return context.Response.WriteAsync(conteudoArq);
        }

        #endregion
    }
}
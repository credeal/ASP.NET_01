using Alura.ListaLeitura.App.html;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivroController
    {
        

        public static Task ExibeParaLer(HttpContext context)
        {
            var html = HtmlUtils.CarregaArquivoHTML("Para-Ler");
            return context.Response.WriteAsync(html);

        }

        public static Task LivrosParaLer2(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudoArq = HtmlUtils.CarregaArquivoHTML("Para-Ler");

            foreach (var Livro in _repo.ParaLer.Livros)
            {
                conteudoArq = conteudoArq
                    .Replace("#NOVO-ITEM", $"<li>{Livro.Titulo} - {Livro.Autor}</li>#NOVO-ITEM");
            }

            conteudoArq = conteudoArq.Replace("#NOVO-ITEM", "");

            return context.Response.WriteAsync(conteudoArq);

        }

        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();

            var conteudoArq = HtmlUtils.CarregaArquivoHTML("Detalhes");

            var livro = repo.Todos.First(l => l.Id == id);

            conteudoArq = conteudoArq.Replace("#NOVO-ITEM", $"<h5>{livro.Titulo} - {livro.Autor}</h5>");

            return context.Response.WriteAsync(conteudoArq);
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.ParaLer.ToString());

        }

        public static Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lendo.ToString());

        }

        public static Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            return context.Response.WriteAsync(_repo.Lidos.ToString());

        }

    }
}

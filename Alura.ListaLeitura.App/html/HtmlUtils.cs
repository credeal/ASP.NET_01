using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alura.ListaLeitura.App.html
{
    public class HtmlUtils
    {
        public static string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompleto = $"..\\..\\..\\Html/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompleto))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}

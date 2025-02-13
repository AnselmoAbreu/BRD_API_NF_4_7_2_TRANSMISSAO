using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class Util
    {
        public string AdicionaZeros(decimal valor, int tamanho) // Preenche com zeros a esquerda
        {
            return valor.ToString().PadLeft(tamanho, '0');
        }

        public bool VerificaExtensao(string extensao)
        {
            var extensoesPermitidas = new HashSet<string> { ".rem", ".ret", ".txt", ".rst", ".dat" };
            if (!extensoesPermitidas.Contains(extensao))
            {
                return false;
            }
            return true;
        }

        public string Espacos(int tamanho)
        {
            var branco = new string(' ', tamanho);
            return branco;
        }

        public bool EhNumerico(string valor)
        {
            return decimal.TryParse(valor, out _);
        }

        public int ContarLinhasArquivo(byte[] fileBytes)
        {
            int linhas = 0;
            using (var stream = new MemoryStream(fileBytes))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    reader.ReadLine();
                    linhas++;
                }
            }
            return linhas;
        }

    }

}
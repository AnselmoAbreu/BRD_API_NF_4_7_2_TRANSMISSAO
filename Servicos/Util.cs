using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class Util
    {
        public string AdicionaZeros(decimal valor, int tamanho) // Preenche com zeros a esquerda
        {
            return valor.ToString().PadLeft(tamanho, '0');
        }

        public async Task<int> ContarLinhas(FileInfo fileRows) // Conta a qtd de linhas do arquivo
        {
            int contador = 0;
            string linha;

            using (var reader = new StreamReader(fileRows.FullName))
            {
                while (reader.ReadLine() != null)
                {
                    contador++;
                }
            }
            return contador;
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
    }
}
using BRD_API_NF_4_7_2_TRANSMISSAO.Models;
using BRD_API_NF_4_7_2_TRANSMISSAO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraArquivoServico
    {
        public Cobranca400 cobranca400 = new Cobranca400();
        public string ExtrairConteudo(string linha, int posicaoInicial, int posicaoFinal)
        {
            if (string.IsNullOrEmpty(linha) || posicaoInicial <= 0 || posicaoFinal >= linha.Length || posicaoInicial > posicaoFinal)
            {
                return string.Empty;
            }
            return linha.Substring(posicaoInicial, posicaoFinal - posicaoInicial + 1);
        }

        public async Task<List<string>> ProcessarArquivo(FileInfo fileRows, string layout) // Conta a qtd de linhas do arquivo
        {
            List<string> listaDeRetornos = new List<string>();
            if (layout == "COB400")
            {
                listaDeRetornos = await cobranca400.ExecutaArquivoAsync(fileRows);
            }
            return listaDeRetornos;
        }
    }
}

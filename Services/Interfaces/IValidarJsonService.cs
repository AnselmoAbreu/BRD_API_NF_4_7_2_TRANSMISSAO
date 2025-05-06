using System.Collections.Generic;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Services.Interfaces
{
    public interface IValidarJsonService
    {
        Task<List<string>> ProcessarArquivos(byte[] fileRows, string layout, string jsonRegras);
        Task<List<string>> ProcessarArquivoCob400Async(byte[] linhas, string jsonRegras);
        Task<List<string>> ProcessarArquivoMtp240Async(byte[] fileRows, string jsonRegras);
        void TransferirParametros(string[] parametros);
        string RetornaErro(int parametroLinha, string parametroKey, string parametroPosicao, string parametroLeitura, string parametroMensagem, string paramManual);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Services.Interfaces
{
    public interface ILeituraArquivoService
    {
        Task<List<string>> ProcessarArquivo(byte[] fileRows, string layout, string jsonRegras);

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraArquivoService
    {
        public List<string> listaDeErros = new List<string>();
        Util util = new Util();
        const string erroNome = "Erro na linha : ";
        const string idNome = " Id Registro : ";

        public class KeyValueItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class RootItem
        {
            public string Key { get; set; }
            public List<KeyValueItem> Value { get; set; }
        }

        public string ExtrairConteudo(string linha, int posicaoInicial, int posicaoFinal)
        {
            if (string.IsNullOrEmpty(linha) || posicaoInicial <= 0 || posicaoFinal >= linha.Length || posicaoInicial > posicaoFinal)
            {
                return string.Empty;
            }
            return linha.Substring(posicaoInicial, posicaoFinal - posicaoInicial + 1);
        }

        public async Task<List<string>> ProcessarArquivo(byte[] fileRows, string layout, string jsonRegras)
        {

            listaDeErros.Clear();

            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                string linha;
                while ((linha = await reader.ReadLineAsync()) != null)
                {
                    switch (linha.Substring(0, 1))
                    {
                        case "0":
                            if (layout == "COB400")
                            {
                                List<RootItem> items = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
                                foreach (var rootItem in items)
                                {
                                    if (rootItem.Key == "RECORD0")
                                    {
                                        foreach (var keyValueItem in rootItem.Value)
                                        {
                                            string[] parametro = keyValueItem.Value.Split(':');
                                            // parametro = "0:1:N:0:U"

                                            int posicaoInicial = Convert.ToInt32(parametro[0]); // posicao 1
                                            int tamanho = Convert.ToInt32(parametro[1]); // Tamanho
                                            string tipo = parametro[2]; // Tipo = N / C
                                            string conteudo = parametro[3]; // (R = REQUERIDO / V = VAZIO)
                                            int parentesco = Convert.ToInt32(parametro[4]); // (0 = PAI / 1 = FILHO)
                                            string posicaoManual = parametro[5];//Posição no manual 
                                            var leitura = linha.Substring(posicaoInicial, tamanho);
                                            if (tipo == "N" && !util.EhNumerico(leitura))
                                            {
                                                listaDeErros.Add(erroNome + linha.Substring(0, 1) + idNome + keyValueItem.Key + " Posição : " + posicaoManual + " Conteúdo = " + leitura);
                                            }
                                            if (tipo == "N" && util.EhNumerico(leitura) && Convert.ToDecimal(leitura) == 0 && posicaoInicial != 0)
                                            {
                                                listaDeErros.Add(erroNome + linha.Substring(0, 1) + idNome + keyValueItem.Key + " Posição : " + posicaoManual + " Conteúdo = " + leitura);
                                            }
                                            if (tipo == "C" && conteudo == "R" && leitura.Trim().Length == 0)
                                            {
                                                listaDeErros.Add(erroNome + linha.Substring(0, 1) + idNome + keyValueItem.Key + " Posição : " + posicaoManual + " Conteúdo = " + leitura);
                                            }
                                            if (tipo == "C" && conteudo == "V" && leitura.Trim().Length > 0)
                                            {
                                                listaDeErros.Add(erroNome + linha.Substring(0, 1) + idNome + keyValueItem.Key + " Posição : " + posicaoManual + " Conteúdo = " + leitura);
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        case "1":
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        default:
                            break;
                    }
                }
            }
            return listaDeErros;
        }
    }
}

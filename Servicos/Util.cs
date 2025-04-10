using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class Util
    {
        public bool VerificarExtensao(string extensao)
        {
            var extensoesPermitidas = new HashSet<string> { ".rem", ".ret", ".txt", ".rst", ".dat" };
            if (!extensoesPermitidas.Contains(extensao))
            {
                return false;
            }
            return true;
        }

        public bool VerificarSeNumerico(string valor)
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

        public async Task<string> VerificarIntegridadeArquivo(byte[] fileRows, string layout)
        {
            string retornoErro = "";
            string linha;
            int linhaAtual = 0;

            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                if (layout.ToUpper() == "MTP240") // MultiPag
                {
                    int countTipo1 = 0;
                    int countTipo5 = 0;
                    bool hasTipo0 = false;
                    bool hasTipo9 = false;
                    bool erroTamanhoLinha = false;

                    while ((linha = await reader.ReadLineAsync()) != null)
                    {
                        linhaAtual++;

                        if (linha.Length < 240) // Agora verificamos até a posição 8
                        {
                            retornoErro = "Erro de integridade: Arquivo contém registro menor do que 240 caracteres na linha " + linhaAtual.ToString();
                            erroTamanhoLinha = true;
                            break;
                        }

                        string posicao8 = linha.Substring(7, 1); // Posição 8 (índice 7)

                        switch (posicao8)
                        {
                            case "0":
                                hasTipo0 = true;
                                break;
                            case "1":
                                countTipo1++;
                                break;
                            case "5":
                                countTipo5++;
                                break;
                            case "9":
                                hasTipo9 = true;
                                break;
                        }
                    }

                    // Verificação 1: Se existe "0", deve existir pelo menos um "9"
                    if (hasTipo0 && !hasTipo9 && !erroTamanhoLinha)
                    {
                        retornoErro = "Erro de integridade: Arquivo contém tipo '0' mas não contém tipo '9' na posição 8";
                    }
                    // Verificação 2: Quantidade de "1" deve ser igual a quantidade de "5"
                    else if (countTipo1 != countTipo5 && !erroTamanhoLinha)
                    {
                        retornoErro = $"Erro de integridade: Quantidade de linhas tipo '1' ({countTipo1}) diferente de tipo '5' ({countTipo5}) na posição 8";
                    }
                }
            }
            return retornoErro;
        }

        public List<string> MontarTabelaOcorrenciasMultipag()
        {
            // Criando a lista de elementos
            List<string> listaElementos = new List<string>
            {
                "00", "01", "02", "03", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH",
                "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AT", "AU",
                "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG",
                "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "CA", "CB",
                "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN",
                "CO", "CP", "HA", "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ",
                "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV",
                "HW", "HX", "HY", "HZ", "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8",
                "H9", "IA", "PA", "PB", "PC", "PD", "PE", "PF", "PG", "PH", "PI", "PJ",
                "PK", "PL", "PM", "PN", "TA", "YA", "YB", "YC", "YD", "YE", "YF", "ZA",
                "ZB", "ZC", "ZD", "ZE", "ZF", "ZG", "ZH", "ZI", "ZJ", "ZK", "5A", "5B",
                "5C", "5D", "5E", "5F", "5I", "5J", "5M", "5T"
            };
            return listaElementos;
        }

        public bool VerificarSeExisteOcorrencia(string elemento, List<string> ocorrencias)
        {
            if (ocorrencias.Contains(elemento))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraArquivoService
    {
        public List<string> listaDeErros = new List<string>();
        Util util = new Util();
        bool erro = false;
        #region Constantes
        const string registroZero = "REGISTRO_HEADER_ARQUIVO_(0)";
        const string registroUm = "RECORD1";
        const string registroDois = "RECORD2";
        const string registroTres = "RECORD3";
        const string registroSeis = "RECORD6";
        const string registroSete = "RECORD7";
        const string registroNove = "RECORD9";
        #endregion
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

            switch (layout)
            {
                case "COB400":
                    ChecarArquivoCob400(fileRows, jsonRegras);
                    break;
                case "MTP240":
                    ChecarArquivoMtp240Async(fileRows, jsonRegras);
                    break;
                default:
                    break;
            }
            return listaDeErros;
        }

        #region CHECAR ARQUIVO COB400
        public string ChecarArquivoCob400(byte[] linhas, string jsonRegras)
        {
            //List<RootItem> items = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
            //foreach (var rootItem in items) // Loop dentro do Json
            //{
            //    if (rootItem.Key == registroZero)
            //    {
            //        foreach (var keyValueItem in rootItem.Value)
            //        {
            //            erro = false;
            //            string[] parametro = keyValueItem.Value.Split(':');
            //            //------------------------------------------------------------------------
            //            // : Posição inicial
            //            // : Tamanho
            //            // : Tipo
            //            // : Conteúdo (R = REQUERIDO / V = VAZIO)
            //            // : Nível (0 = PAI / 1 = FILHO)
            //            // : Posição no manual 
            //            // : Valor fixo
            //            // : Mensagem
            //            // : Campo Data (D)
            //            //------------------------------------------------------------------------
            //            int posicaoInicial = Convert.ToInt32(parametro[0]); // Posicao inicial
            //            int tamanho = Convert.ToInt32(parametro[1]); // Tamanho
            //            string tipo = parametro[2]; // Tipo = N / C
            //            string conteudo = parametro[3]; // (R = REQUERIDO / V = VAZIO)
            //            int parentesco = Convert.ToInt32(parametro[4]); // (0 = PAI / 1 = FILHO)
            //            string posicaoManual = parametro[5]; //Posição no manual 
            //            string valorFixo = parametro[6]; //Valor Fixo
            //            string mensagem = parametro[7]; //Mensagem
            //            bool campoData = parametro[8] == "D" ? true : false; //Mensagem

            //            //------------------------------------------------------------------------
            //            //var leitura = linha.Substring(posicaoInicial, tamanho);

            //            if (!erro && !util.VerificarSeNumerico(leitura) && campoData)
            //            {
            //                bool dataValida = ValidarData(leitura);
            //                if (!dataValida)
            //                    erro = true;
            //            }
            //            if (!erro && tipo == "N" && !util.VerificarSeNumerico(leitura))
            //                erro = true;
            //            if (!erro && tipo == "N" && util.VerificarSeNumerico(leitura) && Convert.ToDecimal(leitura) == 0 && posicaoInicial != 0)
            //                erro = true;
            //            if (!erro && tipo == "C" && conteudo == "R" && leitura.Trim().Length == 0)
            //                erro = true;
            //            if (!erro && tipo == "C" && conteudo == "V" && leitura.Trim().Length > 0)
            //                erro = true;
            //            if (!erro && valorFixo != "" && leitura.Trim() != valorFixo)
            //                erro = true;
            //            if (erro)
            //                listaDeErros.Add(RetornaErro(linha.Substring(0, 1), keyValueItem.Key, posicaoManual, leitura, mensagem));
            //        }
            //        break;
            //    }
            //}
            return "";
        }
        #endregion

        #region CHECAR ARQUIVO MULTIPAG 240
        public async Task<List<string>> ChecarArquivoMtp240Async(byte[] fileRows, string jsonRegras)
        {

            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                string linha;
                while ((linha = await reader.ReadLineAsync()) != null) // Loop dentro do arquivo
                {
                    switch (linha.Substring(7, 1))
                    {
                        case "0":
                            List<RootItem> itensJson = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroZero)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        #region LEGENDAS
                                        //------------------------------------------------------------------------------
                                        // : Posição inicial
                                        // : Tamanho
                                        // : Tipo
                                        // : Obrigatório (R = REQUERIDO / V = VAZIO / Z = ZERADO)
                                        // : Parentesco (0 = PAI / 1 = FILHO)
                                        // : Posição no manual do bradesco
                                        // : Valor fixo
                                        // : Mensagem própria
                                        // : Campo Data (D)
                                        //------------------------------------------------------------------------------
                                        #endregion
                                        int posicaoInicial = Convert.ToInt32(parametro[0]) - 1; // POSICAO INICIAL
                                        int tamanho = Convert.ToInt32(parametro[1]); // TAMANHO
                                        string tipo = parametro[2]; // TIPO = N / C
                                        string obrigatorio = parametro[3]; // (R = REQUERIDO / V = VAZIO)
                                        int parentesco = Convert.ToInt32(parametro[4]); // (0 = PAI / 1 = FILHO)
                                        string posicaoManual = parametro[5]; // POSIÇÃO NO MANUAL 
                                        string valorFixo = parametro[6]; // VALOR FIXO
                                        string mensagem = parametro[7]; // MENSAGEM
                                        bool campoData = parametro[8] == "D" ? true : false; // CAMPO DE DATA
                                        //------------------------------------------------------------------------
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (keyValueItem.Key == "UsoExclusivoFebrabanCnab1")
                                        {
                                            int tst = 1;
                                        }

                                        // Valida o valor fixo do campo
                                        if (valorFixo.Trim() != "" && leitura.Trim() != valorFixo)
                                            erro = true;

                                        // Se for um campo data verifica se é valida
                                        if (campoData)
                                        {
                                            if (!erro && !util.VerificarSeNumerico(leitura))
                                            {
                                                bool dataValida = ValidarData(leitura);
                                                if (!dataValida)
                                                    erro = true;
                                            }
                                        }
                                        // Se for um campo obrigatório
                                        if (obrigatorio == "R")
                                        {
                                            if (campoData) // Valida a data
                                            {
                                                bool dataValida = ValidarData(leitura);
                                                if (!dataValida)
                                                    erro = true;
                                            }

                                            // Valida campo do tipo  Numérico
                                            if (tipo == "N")
                                            {
                                                // Valida se numero , e se tamanho é igual ao parametro tamanho
                                                if (!erro && !util.VerificarSeNumerico(leitura) && leitura.Trim().Length != tamanho)
                                                {
                                                    bool dataValida = ValidarData(leitura);
                                                    if (!dataValida)
                                                        erro = true;
                                                }
                                            }

                                            // Valida campo do tipo Alfanumérico
                                            if (tipo == "C")
                                            {
                                                // Valida se numero , e se tamanho é igual ao parametro tamanho
                                                if (!erro && leitura.Trim().Length != tamanho)
                                                {
                                                    bool dataValida = ValidarData(leitura);
                                                    if (!dataValida)
                                                        erro = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!erro && tipo == "N")
                                            {
                                                if (!util.VerificarSeNumerico(leitura))
                                                    erro = true;
                                                if (util.VerificarSeNumerico(leitura) && leitura.Trim().Length != tamanho)
                                                    erro = true;
                                            }
                                            if (!erro && tipo == "C")
                                            {
                                                if (obrigatorio == "R" && leitura.Trim().Length != tamanho)
                                                    erro = true;
                                            }
                                            if (!erro && valorFixo.Trim() != "" && leitura.Trim() != valorFixo)
                                                erro = true;
                                        }
                                        if (erro)
                                            listaDeErros.Add(RetornaErro(linha.Substring(7, 1), keyValueItem.Key, posicaoManual, leitura, mensagem));

                                    }
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return listaDeErros;


        }

        #endregion

        #region MÉTODOS AUXILIARES
        public string RetornaErro(string parametroLinha, string parametroKey, string parametroPosicao, string parametroLeitura, string parametroMensagem)
        {
            string retorno = "Erro linha : " + parametroLinha + " - Campo : " + parametroKey + " - Posição : " + parametroPosicao + " - Conteúdo = " + parametroLeitura + " - (" + parametroMensagem + ")";
            return retorno;
        }

        static bool ValidarData(string data)
        {
            if (data.Length != 6) return false; // Verifica se a string tem exatamente 6 caracteres

            // Converte para o formato "ddMMyy"
            return DateTime.TryParseExact(
                data,
                "ddMMyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }
        #endregion
    }
}

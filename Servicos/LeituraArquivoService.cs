using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BRD_API_NF_4_7_2_TRANSMISSAO.Servicos.LeituraArquivoService;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraArquivoService
    {
        public List<string> listaDeErros = new List<string>();
        Util util = new Util();
        bool erro = false;
        #region CONSTANTES
        const string registroZero = "REGISTRO_HEADER_ARQUIVO_(0)";
        const string registroNove = "REGISTRO_TRAILER_ARQUIVO_(9)";
        const string segmentoVariosA = "SEGMENTO_PGTOS_DIVERSOS_A";
        const string segmentoVariosB = "SEGMENTO_PGTOS_DIVERSOS_B";
        const string segmentoVariosC = "SEGMENTO_PGTOS_DIVERSOS_C";
        //const string segmentoVarios5 = "SEGMENTO_PGTOS_DIVERSOS_5";
        const string segmentoVariosZ = "SEGMENTO_PGTOS_DIVERSOS_Z";

        const string descricaoRegistroUm_PgVarios = "REGISTRO_PGTOS_DIVERSOS_HEADER_LOTE_(1)"; // Header de lote 045
        const string descricaoRegistroUm_PgTitulos = "REGISTRO_PGTO_TITULOS_HEADER_LOTE_(1)"; // Header de lote 040
        const string descricaoRegistroUm_PgTributos = "REGISTRO_PGTO_TRIBUTOS_HEADER_LOTE_(1)"; // Header de lote 012
        const string descricaoRegistroUm_BloquetoEletronico = "REGISTRO_BLOQUETO_ELETRONICO_HEADER_LOTE_(1)"; // Header de lote 022
        const string descricaoRegistroUm_BasesSistemas = "REGISTRO_BASES_SISTEMAS_HEADER_LOTE_(1)"; // Header de lote 010

        const string segmentoPgTit_J = "PGTO_TITULO_SEGMENTO_J";
        const string segmentoPgTit_J52 = "PGTO_TITULO_SEGMENTO_J52";
       // const string segmentoPgTit_5 = "PGTO_TITULO_SEGMENTO_5";
        const string segmentoPgTit_Z = "PGTO_TITULO_SEGMENTO_Z";

        #endregion

        #region VARIAVEIS
        public int posicaoInicial = 0; // POSICAO INICIAL
        public int tamanho = 0; // TAMANHO
        public string tipo = ""; // TIPO = N / C
        public string obrigatorio = ""; // (R = REQUERIDO / V = VAZIO)
        public int parentesco = 0; // (0 = PAI / 1 = FILHO)
        public string posicaoManual = ""; // POSIÇÃO NO MANUAL 
        public string valorFixo = ""; // VALOR FIXO
        public string mensagem = ""; // MENSAGEM
        public bool campoData = false; // CAMPO DE DATA
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
                    await ChecarArquivoMtp240Async(fileRows, jsonRegras);
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
            string linha;
            int indice = 0;
            List<RootItem> itensJson = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                while ((linha = await reader.ReadLineAsync()) != null) // Loop dentro do arquivo
                {
                    indice++;
                    var tipoRegistro = linha.Substring(7, 1); // Posição 8 (índice 7)
                    var versaoLayout = linha.Substring(13, 3); // Versão do layout
                    var idRegistro = linha.Substring(17, 2); // Id do registro opcinal
                    var filtroHeader = "";
                    switch (tipoRegistro)
                    {
                        case "0": // Header de arquivo
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroZero)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        VerificarConteudo(linha, tipoRegistro, keyValueItem.Key, indice);
                                    }
                                    break;
                                }
                            }
                            break;
                        case "9": // Trailer de arquivo
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroNove)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        VerificarConteudo(linha, tipoRegistro, keyValueItem.Key, indice);
                                    }
                                    break;
                                }
                            }
                            break;
                        case "1": // Header de lote
                            switch (versaoLayout)
                            {
                                case "045":
                                    filtroHeader = descricaoRegistroUm_PgVarios;
                                    break;
                                case "040":
                                    filtroHeader = descricaoRegistroUm_PgTitulos;
                                    break;
                                case "012":
                                    filtroHeader = descricaoRegistroUm_PgTributos;
                                    break;
                                case "022":
                                    filtroHeader = descricaoRegistroUm_BloquetoEletronico;
                                    break;
                                case "010":
                                    filtroHeader = descricaoRegistroUm_BasesSistemas;
                                    break;
                            }
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == filtroHeader)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        VerificarConteudo(linha, tipoRegistro, keyValueItem.Key, indice);
                                    }
                                    break;
                                }
                            }
                            break;
                        case "5": // Trailer de lote
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key.Contains("TRAILER_LOTE"))
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        VerificarConteudo(linha, tipoRegistro, keyValueItem.Key, indice);
                                    }
                                    break;
                                }
                            }
                            break;
                        case "3": // Detalhe
                            var segmento = linha.Substring(13, 1);
                            var filtro = "";
                            switch (segmento)
                            {
                                case "A":
                                    filtro = segmentoVariosA;
                                    break;
                                case "B":
                                    filtro = segmentoVariosB;
                                    break;
                                case "C":
                                    filtro = segmentoVariosC;
                                    break;
                                //case "5":
                                //    filtro = segmentoVarios5;
                                //    break;
                                case "Z":
                                    filtro = segmentoVariosZ;
                                    break;
                                case "J":
                                    if (idRegistro == "52")
                                        filtro = segmentoPgTit_J52;
                                    else
                                        filtro = segmentoPgTit_J;
                                    break;
                            }

                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == filtro)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        erro = false;
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        VerificarConteudo(linha, tipoRegistro, keyValueItem.Key + " - (" + filtro + ")", indice);
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

        public void VerificarConteudo(string linha, string tipoRegistro, string chave, int indice)
        {
            var leitura = linha.Substring(posicaoInicial, tamanho);


            // Valida o valor fixo do campo
            //if (valorFixo.Trim() != "" && leitura.Trim() != valorFixo)
            // erro = true;

            // Se for um campo data verifica se é valida
            if (campoData)
            {
                if (!erro && !util.VerificarSeNumerico(leitura))
                {
                    bool dataValida = ValidarData(leitura);
                    if (!dataValida && !erro)
                        erro = true;
                }
            }
            // Se for um campo obrigatório
            if (obrigatorio == "R")
            {
                // Valida campo do tipo  Numérico
                if (tipo == "N")
                {
                    // Valida se numero , e se tamanho é igual ao parametro tamanho
                    if (!erro && !util.VerificarSeNumerico(leitura) && leitura.Trim().Length != tamanho)
                    {
                        erro = true;
                    }
                }

                // Valida campo do tipo Alfanumérico
                if (tipo == "C")
                {
                    // Valida se numero , e se tamanho é igual ao parametro tamanho
                    if (!erro && leitura.Length != tamanho)
                    {
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
            }
            if (erro)
                listaDeErros.Add(RetornaErro(indice, chave, posicaoManual, leitura, mensagem));
        }

        public void TransferirParametros(string[] parametros)
        {
            //---------------------------------------------------------
            // LEGENDAS
            //---------------------------------------------------------
            // : Posição inicial
            // : Tamanho
            // : Tipo
            // : Obrigatório (R = REQUERIDO / V = VAZIO / Z = ZERADO)
            // : Parentesco (0 = PAI / 1 = FILHO)
            // : Posição no manual do bradesco
            // : Valor fixo
            // : Mensagem própria
            // : Campo Data (D)
            //---------------------------------------------------------
            posicaoInicial = Convert.ToInt32(parametros[0]) - 1; // POSICAO INICIAL
            tamanho = Convert.ToInt32(parametros[1]); // TAMANHO
            tipo = parametros[2]; // TIPO = N / C
            obrigatorio = parametros[3]; // (R = REQUERIDO / V = VAZIO)
            parentesco = Convert.ToInt32(parametros[4]); // (0 = PAI / 1 = FILHO)
            posicaoManual = parametros[5]; // POSIÇÃO NO MANUAL 
            valorFixo = parametros[6]; // VALOR FIXO
            mensagem = parametros[7]; // MENSAGEM
            campoData = parametros[8] == "D" ? true : false; // CAMPO DE DATA
        }

        public string RetornaErro(int parametroLinha, string parametroKey, string parametroPosicao, string parametroLeitura, string parametroMensagem)
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

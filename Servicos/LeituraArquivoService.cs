using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraArquivoService
    {
        public List<string> listaDeErros = new List<string>();
        readonly Util util = new Util();
        bool erro = false;
        #region CONSTANTES
        public const string registroZero = "REGISTRO_HEADER_ARQUIVO_(0)";
        public const string registroNove = "REGISTRO_TRAILER_ARQUIVO_(9)";
        public const string segmentoVariosA = "SEGMENTO_PGTOS_DIVERSOS_A";
        public const string segmentoVariosB = "SEGMENTO_PGTOS_DIVERSOS_B";
        public const string segmentoVariosC = "SEGMENTO_PGTOS_DIVERSOS_C";
        //public const string segmentoVarios5 = "SEGMENTO_PGTOS_DIVERSOS_5";
        public const string segmentoVariosZ = "SEGMENTO_PGTOS_DIVERSOS_Z";

        public const string descricaoRegistroUm_PgVarios = "REGISTRO_PGTOS_DIVERSOS_HEADER_LOTE_(1)"; // Header de lote 045
        public const string descricaoRegistroUm_PgTitulos = "REGISTRO_PGTO_TITULOS_HEADER_LOTE_(1)"; // Header de lote 040
        public const string descricaoRegistroUm_PgTributos = "REGISTRO_PGTO_TRIBUTOS_HEADER_LOTE_(1)"; // Header de lote 012
        public const string descricaoRegistroUm_BloquetoEletronico = "REGISTRO_BLOQUETO_ELETRONICO_HEADER_LOTE_(1)"; // Header de lote 022
        public const string descricaoRegistroUm_BasesSistemas = "REGISTRO_BASES_SISTEMAS_HEADER_LOTE_(1)"; // Header de lote 010

        public const string segmentoPgTit_J = "PGTO_TITULO_SEGMENTO_J";
        public const string segmentoPgTit_J52 = "PGTO_TITULO_SEGMENTO_J52";
        //public const string segmentoPgTit_5 = "PGTO_TITULO_SEGMENTO_5";

        public const string segmentoPgTrib_O = "PGTO_TRIBUTOS_SEGMENTO_O";
        public const string segmentoPgTrib_N = "PGTO_TRIBUTOS_SEGMENTO_N";
        //public const string segmentoPgTrib_N1 = "PGTO_TRIBUTOS_SEGMENTO_N1";
        //public const string segmentoPgTrib_N2 = "PGTO_TRIBUTOS_SEGMENTO_N2";
        //public const string segmentoPgTrib_N3 = "PGTO_TRIBUTOS_SEGMENTO_N3";
        //public const string segmentoPgTrib_N4 = "PGTO_TRIBUTOS_SEGMENTO_N4";
        public const string segmentoPgTrib_W = "PGTO_TRIBUTOS_SEGMENTO_W";
        //public const string segmentoPgTrib_W1 = "PGTO_TRIBUTOS_SEGMENTO_W1";
        //public const string segmentoPgTrib_5 = "PGTO_TRIBUTOS_SEGMENTO_5";
        //public const string segmentoPgTrib_Z = "PGTO_TRIBUTOS_SEGMENTO_Z";

        public const string segmentoBloquetoEletronico_G = "BLOQUETO_ELETRONICO_SEGMENTO_G";
        public const string segmentoBloquetoEletronico_H = "BLOQUETO_ELETRONICO_SEGMENTO_H";
        public const string segmentoBloquetoEletronico_Y03 = "BLOQUETO_ELETRONICO_SEGMENTO_Y3";
        public const string segmentoBloquetoEletronico_Y51 = "BLOQUETO_ELETRONICO_SEGMENTO_Y51";
        public const string segmentoAlegacaoSacado_Y2 = "ALEGACAO_SACADO_SEGMENTO_Y2";

        public const string segmento1_BasesSistemas = "BASES_SISTEMAS_SEGMENTO_1";
        public const string segmento2_BasesSistemas = "BASES_SISTEMAS_SEGMENTO_2";
        public const string segmento3_BasesSistemas = "BASES_SISTEMAS_SEGMENTO_3";

        public const string descricaoRegistroCinco_AlegacaoSacado = "REGISTRO_ALEGACAO_SACADO_TRAILER_LOTE_(5)"; // Trailer de lote
        public const string descricaoRegistroCinco_BasesSistemas = "REGISTRO_BASES_SISTEMAS_TRAILER_LOTE_(5)"; // Trailer de lote
        public const string descricaoRegistroCinco_PgVarios = "REGISTRO_PGTOS_DIVERSOS_TRAILER_LOTE_(5)"; // Trailer de lote

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

        //public string ExtrairConteudo(string linha, int posicaoInicial, int posicaoFinal)
        //{
        //    if (string.IsNullOrEmpty(linha) || posicaoInicial <= 0 || posicaoFinal >= linha.Length || posicaoInicial > posicaoFinal)
        //    {
        //        return string.Empty;
        //    }
        //    return linha.Substring(posicaoInicial, posicaoFinal - posicaoInicial + 1);
        //}

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
                    var idRegistro = linha.Substring(17, 2); // Id do registro opcional
                    var espacoVazio = linha.Substring(59, 181); // Espaço vazio
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
                            if (espacoVazio.Trim().Length == 0) //181)
                                filtroHeader = descricaoRegistroCinco_BasesSistemas;
                            else
                                filtroHeader = descricaoRegistroCinco_PgVarios;

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
                                case "O":
                                    filtro = segmentoPgTrib_O;
                                    break;
                                case "N":
                                    filtro = segmentoPgTrib_N;
                                    break;
                                case "W":
                                    filtro = segmentoPgTrib_W;
                                    break;
                                case "G":
                                    filtro = segmentoBloquetoEletronico_G;
                                    break;
                                case "H":
                                    filtro = segmentoBloquetoEletronico_H;
                                    break;
                                case "Y":
                                    filtro = "";
                                    if (idRegistro == "03")
                                        filtro = segmentoBloquetoEletronico_Y03;
                                    if (idRegistro == "51")
                                        filtro = segmentoBloquetoEletronico_Y51;
                                    if (idRegistro == "02")
                                        filtro = segmentoAlegacaoSacado_Y2;
                                    break;
                                case "1":
                                    filtro = segmento1_BasesSistemas;
                                    break;
                                case "2":
                                    filtro = segmento2_BasesSistemas;
                                    break;
                                case "3":
                                    filtro = segmento3_BasesSistemas;
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
            erro = false;
            // Se for um campo data verifica se é valida
            if (campoData)
            {
                if (!erro && !util.VerificarSeNumerico(leitura))
                {
                    bool dataValida = ValidarData(leitura);
                    if (!dataValida )
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
                    if (leitura.Trim().Length != tamanho)
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

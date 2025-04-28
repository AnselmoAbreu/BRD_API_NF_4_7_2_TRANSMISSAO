using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using BRD_API_NF_4_7_2_TRANSMISSAO.Services.Interfaces;
using BRD_API_NF_4_7_2_TRANSMISSAO.Validators;
using System.Reflection;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Services.Cnab
{
    public class LeituraArquivoService : ILeituraArquivoService
    {
        private readonly List<string> listaDeErros = new List<string>();
        private ValidarCnabMtp240 _validarCnabMtp240 = new ValidarCnabMtp240();

        readonly Utils.Helpers.CnabHelper util = new Utils.Helpers.CnabHelper();
        bool erro = false;
        #region CONSTANTES
        public const string registroZero = "REGISTRO_HEADER_ARQUIVO_(0)";
        public const string registroNove = "REGISTRO_TRAILER_ARQUIVO_(9)";
        public const string segmentoVariosA = "SEGMENTO_PGTOS_DIVERSOS_A";
        public const string segmentoVariosB = "SEGMENTO_PGTOS_DIVERSOS_B";
        public const string segmentoVariosC = "SEGMENTO_PGTOS_DIVERSOS_C";
        public const string segmentoVarios5 = "SEGMENTO_PGTOS_DIVERSOS_5";
        public const string segmentoVariosZ = "SEGMENTO_PGTOS_DIVERSOS_Z";

        public const string descricaoRegistroUm_PgVarios = "REGISTRO_PGTOS_DIVERSOS_HEADER_LOTE_(1)"; // Header de lote 045
        public const string descricaoRegistroUm_PgTitulos = "REGISTRO_PGTO_TITULOS_HEADER_LOTE_(1)"; // Header de lote 040
        public const string descricaoRegistroUm_PgTributos = "REGISTRO_PGTO_TRIBUTOS_HEADER_LOTE_(1)"; // Header de lote 012
        public const string descricaoRegistroUm_BloquetoEletronico = "REGISTRO_BLOQUETO_ELETRONICO_HEADER_LOTE_(1)"; // Header de lote 022
        public const string descricaoRegistroUm_BasesSistemas = "REGISTRO_BASES_SISTEMAS_HEADER_LOTE_(1)"; // Header de lote 010

        public const string segmentoPgTit_J = "PGTO_TITULO_SEGMENTO_J";
        public const string segmentoPgTit_J52 = "PGTO_TITULO_SEGMENTO_J52";
        public const string segmentoPgTit_5 = "PGTO_TITULO_SEGMENTO_5";

        public const string segmentoPgTrib_O = "PGTO_TRIBUTOS_SEGMENTO_O";
        public const string segmentoPgTrib_N = "PGTO_TRIBUTOS_SEGMENTO_N";
        public const string segmentoPgTrib_N1 = "PGTO_TRIBUTOS_SEGMENTO_N1";
        public const string segmentoPgTrib_N2 = "PGTO_TRIBUTOS_SEGMENTO_N2";
        public const string segmentoPgTrib_N3 = "PGTO_TRIBUTOS_SEGMENTO_N3";
        public const string segmentoPgTrib_N4 = "PGTO_TRIBUTOS_SEGMENTO_N4";
        public const string segmentoPgTrib_W = "PGTO_TRIBUTOS_SEGMENTO_W";
        public const string segmentoPgTrib_W1 = "PGTO_TRIBUTOS_SEGMENTO_W1";
        public const string segmentoPgTrib_5 = "PGTO_TRIBUTOS_SEGMENTO_5";
        public const string segmentoPgTrib_Z = "PGTO_TRIBUTOS_SEGMENTO_Z";

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
        public string listaDeOpcoes = ""; // Lista de opções possíveis para o campo

        public string parametrosAnteriores=""; // Guarda as opções do registro anterior para tratamento com mais de uma linha
        public string linhaAnterior = ""; // Guarda a linha anterior para tratamento com mais de uma linha
        #endregion

        #region CLASSES AUXILIARES
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
        #endregion

        public async Task<List<string>> ProcessarArquivo(byte[] fileRows, string layout, string jsonRegras)
        {

            listaDeErros.Clear();

            switch (layout)
            {
                case "COB400":
                    ChecarArquivoCob400Async(fileRows, jsonRegras);
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
        public async Task<List<string>> ChecarArquivoCob400Async(byte[] linhas, string jsonRegras)
        {
            return listaDeErros;
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
                    var versaoLayout = linha.Substring(13, 3); // Versão do layout 045, 040, 012, 022, 010
                    var idRegistro = linha.Substring(17, 2); // Id do registro opcional
                    var espacoVazio = linha.Substring(59, 181); // Espaço vazio
                    var segmento = linha.Substring(13, 1); // Segmento do detalhe
                    var filtroHeader = "";

                    /*
                     
                    switch (tipoRegistro)

                    case "0": // Header de arquivo
                    case "9": // Trailer de arquivo
                    case "1": // Header de lote
                    switch (versaoLayout)
                    {
                        case "045":
                            filtroHeader = descricaoRegistroUm_PgVarios;
                        case "040":
                            filtroHeader = descricaoRegistroUm_PgTitulos;
                        case "012":
                            filtroHeader = descricaoRegistroUm_PgTributos;
                        case "022":
                            filtroHeader = descricaoRegistroUm_BloquetoEletronico;
                        case "010":
                            filtroHeader = descricaoRegistroUm_BasesSistemas;
                    }
                    case "5": // Trailer de lote
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
                                case "5":
                                    filtro = segmentoVarios5;
                                    break;
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

                    */
                    foreach (var rootItem in itensJson) // Loop dentro do Json
                    {
                        //if (rootItem.Key == registroZero)
                        //{
                        foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                        {
                            erro = false;
                            string[] parametro = keyValueItem.Value.Split(':'); // Lê regras
                            TransferirParametros(parametro);


                            string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                            MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                metodoNome,
                                new Type[] { typeof(string), typeof(string) }

                                // Especifica que o método aceita uma string
                                );

                            if (metodo != null)
                            {
                                var retorno = metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha , linhaAnterior});
                            }
                            else
                            {
                                throw new ArgumentException($"Método {metodoNome} não encontrado");
                            }
                            parametrosAnteriores = keyValueItem.Value; // Guarda os parametros anteriores
                            linhaAnterior = linha; // Guarda a linha anterior
                        }
                    }

                }
            }
            return listaDeErros;
        }

        #endregion

        #region MÉTODOS AUXILIARES

        //public void VerificarConteudo(string linha, string tipoRegistro, string chave, int indice)
        //{
        //    var leitura = linha.Substring(posicaoInicial, tamanho);
        //    erro = false;

        //    // Verifica valorFixo
        //    if (!string.IsNullOrEmpty(valorFixo))
        //    {
        //        if (leitura != valorFixo)
        //        {
        //            erro = true;
        //        }
        //    }

        //    // Percorre a Lista De Opções do campo
        //    if (!string.IsNullOrEmpty(listaDeOpcoes))
        //    {
        //        string[] array = listaDeOpcoes.Split(',');
        //        var conteudoIgual = false;
        //        // Percorrendo e verificando o conteúdo:
        //        foreach (string item in array)
        //        {
        //            if (item == leitura)
        //                conteudoIgual = true;
        //        }
        //        if (!erro && !conteudoIgual)
        //            erro = true;
        //    }


        //    // Se for um campo data verifica se é valida
        //    if (campoData)
        //    {
        //        if (util.VerificarSeNumerico(leitura))
        //        {
        //            bool dataHoraValida = ValidarDataHora(leitura);
        //            if (!erro && !dataHoraValida)
        //                erro = true;
        //        }
        //        else
        //        {
        //            if (!erro)
        //                erro = true;
        //        }
        //    }

        //    // Se for um campo obrigatório
        //    if (obrigatorio == "R")
        //    {
        //        if (tipo == "N")
        //            if (!erro && (!util.VerificarSeNumerico(leitura) || leitura.Trim().Length < tamanho))
        //                erro = true;


        //        if (tipo == "C")
        //            if (!erro && leitura.Length < tamanho)
        //                erro = true;
        //    }
        //    else
        //    {
        //        if (tipo == "N")
        //            if (!erro && (!util.VerificarSeNumerico(leitura) || leitura.Trim().Length == 0 || leitura.Trim().Length < tamanho))
        //                erro = true;
        //    }

        //    if (erro)
        //        listaDeErros.Add(RetornaErro(indice, chave, posicaoManual, leitura, mensagem));
        //}

        public void TransferirParametros(string[] parametros)
        {
            //--------------------------------------------------------
            // : Posição inicial
            // : Tamanho
            // : Tipo A = ALFANUMÉRICO / N = NUMÉRICO
            // : Obrigatório (R = REQUERIDO / V = VAZIO / Z = ZERADO)
            // : Parentesco (0 = PAI / 1 = FILHO)
            // : Posição no manual do bradesco
            // : Valor fixo
            // : Mensagem própria
            // : Campo Data (D)
            // : Lista de opções do campo
            //--------------------------------------------------------
            posicaoInicial = Convert.ToInt32(parametros[0]) - 1; // POSICAO INICIAL
            tamanho = Convert.ToInt32(parametros[1]); // TAMANHO
            tipo = parametros[2]; // TIPO = N / C
            obrigatorio = parametros[3]; // (R = REQUERIDO / V = VAZIO)
            parentesco = Convert.ToInt32(parametros[4]); // (0 = PAI / 1 = FILHO)
            posicaoManual = parametros[5]; // POSIÇÃO NO MANUAL 
            valorFixo = parametros[6]; // VALOR FIXO
            mensagem = parametros[7]; // MENSAGEM
            campoData = parametros[8] == "D"; // CAMPO DE DATA
            listaDeOpcoes = parametros[9]; // Lista de opções possíveis para o campo
        }

        public string RetornaErro(int parametroLinha, string parametroKey, string parametroPosicao, string parametroLeitura, string parametroMensagem)
        {
            string retorno = "[ Erro na linha : " + parametroLinha + " ] [ Posição : " + parametroPosicao + " ] [ Conteúdo = {" + parametroLeitura + "} ] [ " + parametroMensagem + " ]";
            return retorno;
        }


        #endregion
    }
}

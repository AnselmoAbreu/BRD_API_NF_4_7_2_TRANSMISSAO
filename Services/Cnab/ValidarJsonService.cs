using BRD_API_NF_4_7_2_TRANSMISSAO.Services.Interfaces;
using BRD_API_NF_4_7_2_TRANSMISSAO.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Services.Cnab
{
    public class ValidarJsonService : IValidarJsonService
    {
        private readonly List<string> listaDeErros = new List<string>();
        private ValidarCnabMtp240 _validarCnabMtp240 = new ValidarCnabMtp240();
        private ValidarCnabCob400 _validarCnabCob400 = new ValidarCnabCob400();


        //readonly Utils.Helpers.CnabHelper util = new Utils.Helpers.CnabHelper();
        bool erro = false;
        #region CONSTANTES
        // MTP240
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

        // COB400
        const string registroTipoZero = "REGISTRO_TIPO_0"; // Header de arquivo
        const string registroTipoNove = "REGISTRO_TIPO_9"; // Trailer de arquivo
        const string registroTipo1 = "REGISTRO_TIPO_1";
        const string registroTipo2 = "REGISTRO_TIPO_2";
        const string registroTipo3 = "REGISTRO_TIPO_3";
        const string registroTipo6 = "REGISTRO_TIPO_6";
        const string registroTipo7 = "REGISTRO_TIPO_7";
        #endregion

        #region VARIAVEIS 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1104:Fields should not have public accessibility", Justification = "<Pending>")]
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

        public string parametrosAnteriores = ""; // Guarda as opções do registro anterior para tratamento com mais de uma linha
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

        public async Task<List<string>> ProcessarArquivos(byte[] fileRows, string layout, string jsonRegras)
        {

            listaDeErros.Clear();
            try
            {
                switch (layout)
                {

                    case "COB400":
                        await ProcessarArquivoCob400Async(fileRows, jsonRegras);
                        break;
                    case "MTP240":
                        await ProcessarArquivoMtp240Async(fileRows, jsonRegras);
                        break;
                    default:
                        break;
                }
                return listaDeErros;
            }
            catch (Exception ex)
            {
                listaDeErros.Add($"Erro ao processar o arquivo: {ex.Message}");
                return listaDeErros;
            }
        }

        #region PROCESSAR ARQUIVO COB400
        public async Task<List<string>> ProcessarArquivoCob400Async(byte[] fileRows, string jsonRegras)
        {
            string linha;
            int indice = 0;
            Boolean retorno;
            Boolean exitLoop = false;
            List<RootItem> itensJson = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                while ((linha = await reader.ReadLineAsync()) != null) // Loop dentro do arquivo
                {
                    exitLoop = false;
                    indice++;
                    var tipoRegistro = linha.Substring(0, 1); // Posição 8 (índice 7)
                    var versaoLayout = linha.Substring(13, 3); // Versão do layout
                    var idRegistro = linha.Substring(17, 2); // Id do registro opcional
                    var espacoVazio = linha.Substring(59, 181); // Espaço vazio
                    var segmento = linha.Substring(13, 1);

                    var filtroHeader = "";
                    switch (tipoRegistro)
                    {
                        case " ":
                            exitLoop = true;
                            listaDeErros.Add(RetornaErro(indice, "TipoRegistro", "1-1", "", "Erro - Identificação do Registro", ""));
                            break;
                        case "0": // Header de arquivo
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipoZero)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "9": // Trailer de arquivo
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipoNove)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "1":
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipo1)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "2": // Trailer de lote
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipo2)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "3": // Detalhe
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipo3)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "6": // Detalhe
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipo6)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case "7": // Detalhe
                            foreach (var rootItem in itensJson) // Loop dentro do Json
                            {
                                if (rootItem.Key == registroTipo7)
                                {
                                    foreach (var keyValueItem in rootItem.Value) // Loop dentro da chave principal
                                    {
                                        string[] parametro = keyValueItem.Value.Split(':'); // LÊ REGRAS
                                        TransferirParametros(parametro);
                                        erro = false;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);

                                        if (!_validarCnabCob400.ValidarCampos(keyValueItem.Value, linha))
                                        {
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (exitLoop)
                        break;
                }
            }
            return listaDeErros;
        }
        #endregion

        #region PROCESSAR ARQUIVO MULTIPAG 240
        public async Task<List<string>> ProcessarArquivoMtp240Async(byte[] fileRows, string jsonRegras)
        {
            string linha;
            int indice = 0;
            Boolean retorno;
            Boolean exitLoop = false;
            List<RootItem> itensJson = JsonConvert.DeserializeObject<List<RootItem>>(jsonRegras);
            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                while ((linha = await reader.ReadLineAsync()) != null) // Loop dentro do arquivo
                {
                    exitLoop = false;
                    indice++;
                    var tipoRegistro = linha.Substring(7, 1); // Posição 8 (índice 7)
                    var versaoLayout = linha.Substring(13, 3); // Versão do layout
                    var idRegistro = linha.Substring(17, 2); // Id do registro opcional
                    var espacoVazio = linha.Substring(59, 181); // Espaço vazio
                    var segmento = linha.Substring(13, 1);

                    var filtroHeader = "";
                    switch (tipoRegistro)
                    {
                        case " ":
                            exitLoop = true;
                            listaDeErros.Add(RetornaErro(indice, "TipoRegistro", "8-8", "", "Erro - Tipo de Registro", "G003"));
                            break;
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
                                        string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                                        MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                            metodoNome,
                                            new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }

                                            // Especifica que o método aceita uma string
                                            );
                                        retorno = true;
                                        if (metodo != null)
                                        {
                                            retorno = (bool)metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha, linhaAnterior });
                                        }
                                        else
                                        {
                                            listaDeErros.Add($"Método {metodoNome} não encontrado - Linha {indice}");
                                        }
                                        parametrosAnteriores = keyValueItem.Value;
                                        linhaAnterior = linha;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);
                                        if (!retorno)
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));

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
                                        string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                                        MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                            metodoNome,
                                            new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }

                                            // Especifica que o método aceita uma string
                                            );
                                        retorno = true;
                                        if (metodo != null)
                                        {
                                            retorno = (bool)metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha, linhaAnterior });
                                        }
                                        else
                                        {
                                            listaDeErros.Add($"Método {metodoNome} não encontrado - Linha {indice}");
                                        }
                                        parametrosAnteriores = keyValueItem.Value;
                                        linhaAnterior = linha;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);
                                        if (!retorno)
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));

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
                                        string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                                        MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                            metodoNome,
                                            new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }

                                            // Especifica que o método aceita uma string
                                            );
                                        retorno = true;
                                        if (metodo != null)
                                        {
                                            retorno = (bool)metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha, linhaAnterior });
                                        }
                                        else
                                        {
                                            listaDeErros.Add($"Método {metodoNome} não encontrado - Linha {indice}");
                                        }
                                        parametrosAnteriores = keyValueItem.Value;
                                        linhaAnterior = linha;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);
                                        if (!retorno)
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
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
                                        string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                                        MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                            metodoNome,
                                            new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }

                                            // Especifica que o método aceita uma string
                                            );
                                        retorno = true;
                                        if (metodo != null)
                                        {
                                            retorno = (bool)metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha, linhaAnterior });
                                        }
                                        else
                                        {
                                            listaDeErros.Add($"Método {metodoNome} não encontrado - Linha {indice}");
                                        }
                                        parametrosAnteriores = keyValueItem.Value;
                                        linhaAnterior = linha;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);
                                        if (!retorno)
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                    }
                                    break;
                                }
                            }
                            break;
                        case "3": // Detalhe
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
                                        string metodoNome = "ValidarDescricao_" + listaDeOpcoes;

                                        MethodInfo metodo = typeof(ValidarCnabMtp240).GetMethod(
                                            metodoNome,
                                            new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }

                                            // Especifica que o método aceita uma string
                                            );
                                        retorno = true;
                                        if (metodo != null)
                                        {
                                            retorno = (bool)metodo.Invoke(_validarCnabMtp240, new object[] { keyValueItem.Value, parametrosAnteriores, linha, linhaAnterior });
                                        }
                                        else
                                        {
                                            listaDeErros.Add($"Método {metodoNome} não encontrado - Linha {indice}");
                                        }
                                        parametrosAnteriores = keyValueItem.Value;
                                        linhaAnterior = linha;
                                        var leitura = linha.Substring(posicaoInicial, tamanho);
                                        if (!retorno)
                                            listaDeErros.Add(RetornaErro(indice, keyValueItem.Key, posicaoManual, leitura, mensagem, parametro[9]));
                                    }
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (exitLoop)
                        break;
                }
            }
            return listaDeErros;
        }

        #endregion

        #region MÉTODOS AUXILIARES
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

        public string RetornaErro(int parametroLinha, string parametroKey, string parametroPosicao, string parametroLeitura, string parametroMensagem, string paramManual)
        {
            string retorno = "[ Erro na linha : " + parametroLinha + " ] [ Posição : " + parametroPosicao + " ] [ Conteúdo = {" + parametroLeitura + "} ] [ " + parametroMensagem + " ] [ " + paramManual + " ]";
            return retorno;
        }
        #endregion
    }
}

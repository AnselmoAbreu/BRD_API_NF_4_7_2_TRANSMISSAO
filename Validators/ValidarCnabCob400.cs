using System;
using System.Globalization;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Validators
{
    public class ValidarCnabCob400
    {
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
        public string listaDeOpcoes = ""; // LISTA DE OPÇÕES POSSÍVEIS PARA O CAMPO
        public string campoAtual = ""; // CONTEUDO PARA VALIDAR

        #endregion

        public bool ValidarCampos(string parametrosAtuais, string linhaAtual)
        {
            string[] parametros = parametrosAtuais.Split(':'); // Lê regras
            posicaoInicial = Convert.ToInt32(parametros[0]) - 1; // POSICAO INICIAL
            tamanho = Convert.ToInt32(parametros[1]); // TAMANHO
            tipo = parametros[2]; // TIPO = N / A
            obrigatorio = parametros[3]; // (R = REQUERIDO / V = VAZIO)
            parentesco = Convert.ToInt32(parametros[4]); // (0 = PAI / 1 = FILHO)
            posicaoManual = parametros[5]; // POSIÇÃO NO MANUAL 
            valorFixo = parametros[6]; // VALOR FIXO
            mensagem = parametros[7]; // MENSAGEM
            campoData = parametros[8] == "D"; // CAMPO DE DATA
            listaDeOpcoes = parametros[9]; // LISTA DE OPÇÕES POSSÍVEIS PARA O CAMPO
            campoAtual = linhaAtual.Substring(posicaoInicial, tamanho);

            // Valor fixo
            if (!string.IsNullOrEmpty(valorFixo.Trim()) && !campoData)
            {
                if (campoAtual.Trim() != valorFixo)
                    return false;
            }

            // Numericos
            if (tipo == "N" && !VerificarSeNumerico(campoAtual))
                return false;

            // Alfanumerico
            if (tipo == "A" && campoAtual.Length < tamanho)
                return false;

            // Data
            if (campoData && !ValidarDataHora(campoAtual))
                return false;

            return true;
        }

        public bool VerificarSeNumerico(string valor)
        {
            //return decimal.TryParse(valor, out _);
            return double.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out _);

        }
        public bool ValidarDataHora(string campoData)
        {
            if (campoData.Length != 6 && campoData.Length != 8)
                return false;

            if (campoData.Length == 8)
            {
                return DateTime.TryParseExact(
                    campoData,
                    "ddMMyyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);
            }
            else
            {
                return DateTime.TryParseExact(
                    campoData,
                    "HHmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Validators
{
    public class ValidarCnabCob240
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

        // Anteriores
        public int posicaoInicialAnterior = 0; // POSICAO INICIAL
        public int tamanhoAnterior = 0; // TAMANHO
        public string tipoAnterior = ""; // TIPO = N / C
        public string obrigatorioAnterior = ""; // (R = REQUERIDO / V = VAZIO)
        public int parentescoAnterior = 0; // (0 = PAI / 1 = FILHO)
        public string posicaoManualAnterior = ""; // POSIÇÃO NO MANUAL 
        public string valorFixoAnterior = ""; // VALOR FIXO
        public string mensagemAnterior = ""; // MENSAGEM
        public bool campoDataAnterior = false; // CAMPO DE DATA
        public string listaDeOpcoesAnterior = ""; // LISTA DE OPÇÕES POSSÍVEIS PARA O CAMPO

        public string campoAtual = ""; // CONTEUDO PARA VALIDAR
        public string campoAnterior = ""; // CONTEUDO ANTERIOR PARA VALIDAR

        #endregion

        #region MÉTODOS DE VALIDAÇÃO DE CADA CAMPO
        public bool ValidarDescricao_A001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_A002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaC004 = CriarListaC004();
            return VerificarStringNaListaC004(campoAtual, listaC004);
        }

        public bool ValidarDescricao_C006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5")
                return false;
            return true;
        }

        public bool ValidarDescricao_C007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3")
                return false;
            return true;

        }

        public bool ValidarDescricao_C008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2")
                return false;
            return true;
        }

        public bool ValidarDescricao_C009(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5" && campoAtual != "7" && campoAtual != "8")
                return false;
            return true;
        }

        public bool ValidarDescricao_C010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4")
                return false;
            return true;
        }

        public bool ValidarDescricao_C011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C015(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaC015 = CriarListaC015();
            return VerificarStringNaListaC015(campoAtual, listaC015);
        }

        public bool ValidarDescricao_C016(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "A" && campoAtual != "N")
                return false;
            return true;
        }

        public bool ValidarDescricao_C018(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3")
                return false;
            return true;
        }

        public bool ValidarDescricao_C019(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C020(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C021(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5" && campoAtual != "6" && campoAtual != "7")
                return false;
            return true;
        }

        public bool ValidarDescricao_C022(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C023(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C024(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C026(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5" && campoAtual != "8" && campoAtual != "9")
                return false;
            return true;
        }

        public bool ValidarDescricao_C027(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C028(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3")
                return false;
            return true;
        }

        public bool ValidarDescricao_C029(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C030(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C031(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C032(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C036(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var limites = Convert.ToInt32(campoAtual);
            if (limites < 2 && limites > 99)
                return false;
            return true;
        }

        public bool ValidarDescricao_C037(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C038(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C039(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "01" && campoAtual != "02" && campoAtual != "03")
                return false;
            return true;
        }

        public bool ValidarDescricao_C040(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3")
                return false;
            return true;
        }

        public bool ValidarDescricao_C041(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var limites = Convert.ToInt32(campoAtual);
            if (limites < 1 && limites > 36)
                return false;
            return true;
        }

        public bool ValidarDescricao_C042(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C043(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "01" && campoAtual != "02" && campoAtual != "03" && campoAtual != "04")
                return false;
            return true;
        }

        public bool ValidarDescricao_C044(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaC044 = CriarListaC044();
            return VerificarStringNaListaC044(campoAtual, listaC044);
        }

        public bool ValidarDescricao_C045(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C047(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaC047 = CriarListaC047();
            return VerificarStringNaListaC047(campoAtual, listaC047);
        }

        public bool ValidarDescricao_C048(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C049(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C050(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C052(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C054(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C055(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C056(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C057(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C058(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C059(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C060(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C061(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3")
                return false;
            return true;
        }

        public bool ValidarDescricao_C062(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2")
                return false;
            return true;
        }

        public bool ValidarDescricao_C063(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C064(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C065(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C066(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaC066 = CriarListaC066();
            return VerificarStringNaListaC066(campoAtual, listaC066);
        }

        public bool ValidarDescricao_C070(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C071(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C072(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C073(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_C074(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "0" && campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5" && campoAtual != "9")
                return false;
            return true;
        }

        public bool ValidarDescricao_G004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "0" && campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "9")
                return false;
            return true;
        }

        public bool ValidarDescricao_G006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G009(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G013(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G015(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2")
                return false;
            return true;
        }

        public bool ValidarDescricao_G016(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G017(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G018(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G019(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G020(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "01600" && campoAtual != "06250")
                return false;
            return true;
        }

        public bool ValidarDescricao_G021(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G022(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G025(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaG025 = CriarListaG025();
            return VerificarStringNaListaG025(campoAtual, listaG025);
        }

        public bool ValidarDescricao_G028(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "C" && campoAtual != "D" && campoAtual != "E" && campoAtual != "G" && campoAtual != "I" && campoAtual != "R" && campoAtual != "T")
                return false;
            return true;
        }

        public bool ValidarDescricao_G030(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G032(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G033(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G034(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G035(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G036(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G037(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G038(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G039(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G045(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G049(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G056(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G057(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G065(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            var listaG065 = CriarListaG065();
            return VerificarStringNaListaG065(campoAtual, listaG065);
        }

        public bool ValidarDescricao_G067(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "01" && campoAtual != "02" && campoAtual != "03" && campoAtual != "04" && campoAtual != "11" && campoAtual != "50" && campoAtual != "51")
                return false;
            return true;
        }

        public bool ValidarDescricao_G068(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G069(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G070(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G071(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G072(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G073(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2")
                return false;
            return true;
        }

        public bool ValidarDescricao_G074(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G075(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G076(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G077(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G078(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G079(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G102(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }

        public bool ValidarDescricao_G103(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarCampos())
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5")
                return false;
            return true;
        }

        public bool ValidarDescricao_BRCO(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            // Este validador foi criado para suprimir campos onde o manual diverge da atualidade.
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            return ValidarCampos();
        }
        #endregion 

        #region METODOS AUXILIARES
        public bool ValidarCampos()
        {
            // Valor fixo
            if (!string.IsNullOrEmpty(valorFixo.Trim()) && !campoData)
            {
                if (campoAtual != valorFixo)
                    return false;
            }

            // Numericos
            if (tipo == "N" && !VerificarSeNumerico(campoAtual.Trim()))
                return false;

            if (tipo == "N" && campoAtual.Trim().Length < tamanho)
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
        public void TransferirParametros(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
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

            // Conteudo da linha anterior
            if (!string.IsNullOrEmpty(parametrosAnteriores))
            {
                string[] paramAnteriores = parametrosAnteriores.Split(':'); // Lê regras
                posicaoInicialAnterior = Convert.ToInt32(paramAnteriores[0]) - 1; // POSICAO INICIAL
                tamanhoAnterior = Convert.ToInt32(paramAnteriores[1]); // TAMANHO
                tipoAnterior = paramAnteriores[2]; // TIPO = N / A
                obrigatorioAnterior = paramAnteriores[3]; // (R = REQUERIDO / V = VAZIO)
                parentescoAnterior = Convert.ToInt32(paramAnteriores[4]); // (0 = PAI / 1 = FILHO)
                posicaoManualAnterior = paramAnteriores[5]; // POSIÇÃO NO MANUAL 
                valorFixoAnterior = paramAnteriores[6]; // VALOR FIXO
                mensagemAnterior = paramAnteriores[7]; // MENSAGEM
                campoDataAnterior = paramAnteriores[8] == "D"; // CAMPO DE DATA
                listaDeOpcoesAnterior = paramAnteriores[9]; // LISTA DE OPÇÕES POSSÍVEIS PARA O CAMPO
            }
            campoAtual = linhaAtual.Substring(posicaoInicial, tamanho);
            campoAnterior = linhaAnterior.Substring(posicaoInicialAnterior, tamanhoAnterior);
        }
        #endregion

        #region LISTAS DE OPÇÕES DE CAMPOS
        //=====================================================================
        public static List<string> CriarListaC004()
        {
            return new List<string>
            {
                "01","02","03","04","05","06","07","08","09",
                "10","11","12","13","14","15","16","17","18",
                "19","20","21","22","24","25","26","27","30",
                "31","33","34","35","40","41","42","43","44",
                "45","46","47"
            };
        }
        public bool VerificarStringNaListaC004(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaC015()
        {
            return new List<string>
            {
                "01","02","03","04","05","06","07",
                "08","09","10","11","12","13","14",
                "15","16","17","18","19","20","21",
                "22","23","24","25","26","27","28",
                "29","30","31","32","33","99"

            };
        }
        public bool VerificarStringNaListaC015(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaC044()
        {
            return new List<string>
            {
                "02","03","04","05","06","07","08",
                "09","10","11","12","13","14","15",
                "16","17","18","19","20","21","23",
                "24","25","26","27","28","29","30",
                "31","32","33","34","35","36","37",
                "38","39","40","41","42","43","44",
                "45","46","47","48","49","50","51",
                "52","53","54","66","73"

            };
        }
        public bool VerificarStringNaListaC044(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaC047()
        {
            return new List<string>
            {
                "01","02","03","04","05","06","07","08","09","10","11","12","13","14","15",
                "16","17","18","19","20","21","22","23","24","25","26","27","28","29","30",
                "31","32","33","34","35","36","37","38","39","40","41","42","43","44","45",
                "46","47","48","49","50","51","52","53","54","55","56","57","58","59","60",
                "61","62","63","64","65","66","67","68","69","70","71","72","73","74","75",
                "76","77","78","79","80","81","82","83","84","85","86","87","88","89","90",
                "91","92","93","94","95","96","97","98","99","A1","A2","A3","A4","A6","B3",
                "B7","B8","B9","C0","C1","P1","P2","P3","P4","P5","P6","P7","P8"
            };
        }
        public bool VerificarStringNaListaC047(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaC066()
        {
            return new List<string>
            {
                "01","02","03","04","05","06","07","08",
                "09","10","11","12","13","14","15","16",
                "17","18","19","20","21","22","23","24",
                "25","26","27","28","29","30","31","32",
                "33","34"
            };
        }
        public bool VerificarStringNaListaC066(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaG025()
        {
            return new List<string>
            {
                "01","03","04","05","06","07","08","09","10","11","12","13","14","20","22","25",
                "26","29","30","32","33","34","40","41","50","60","70","75","77","80","90","98"
            };
        }
        public bool VerificarStringNaListaG025(string input, List<string> list)
        {
            return list.Contains(input);
        }
        //=====================================================================
        public static List<string> CriarListaG065()
        {
            return new List<string>
            {
                "01","02","03","04","05","06","07",
                "08","09","10","11","12","13","14"
            };
        }
        public bool VerificarStringNaListaG065(string input, List<string> list)
        {
            return list.Contains(input);
        }
        #endregion
    }
}
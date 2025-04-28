using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using static BRD_API_NF_4_7_2_TRANSMISSAO.Services.Cnab.LeituraArquivoService;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Validators
{
    public class ValidarCnabMtp240
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

        public bool ValidarDescricao_1001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_1002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length == 0 && campoAnterior.Length > 0)
                return false;

            if (campoAtual.Length > 0 && campoAnterior.Length == 0)
                return false;

            return true;
        }

        public bool ValidarDescricao_1003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_2001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_2002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_2003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_2004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_2005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_3002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_3008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_5003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            if (campoAtual != "001" && campoAtual != "002" && campoAtual != "003")
                return false;
            return true;
        }
        public bool ValidarDescricao_5007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_5010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_5011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_A001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var listaA001 = CriarListaA001();
            return VerificarIsStringInListA001(campoAtual, listaA001);
        }
        public bool ValidarDescricao_A002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_B001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length == 0)
                return false;
            return true;
        }
        public bool ValidarDescricao_C004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var listaC004 = CriarListaC004();
            return VerificarIsStringInListC004(campoAtual, listaC004);
        }
        public bool ValidarDescricao_C006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4")
                return false;
            return true;
        }
        public bool ValidarDescricao_C011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_C014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C015(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var listaC015 = CriarListaC015();
            return VerificarIsStringInListC015(campoAtual, listaC015);
        }
        public bool ValidarDescricao_C020(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C021(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var listaC021 = CriarListaC021();
            return VerificarIsStringInListC021(campoAtual, listaC021);
        }
        public bool ValidarDescricao_C022(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_C023(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C026(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var listaC026 = CriarListaC026();
            return VerificarIsStringInListC026(campoAtual, listaC026);
        }
        public bool ValidarDescricao_C027(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C067(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C068(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C069(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_C073(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_C075(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (!ValidarDataHora(campoAtual))
                return false;
            return true;
        }
        public bool ValidarDescricao_G001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }


        public bool ValidarDescricao_G002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!string.IsNullOrEmpty(valorFixo))
            {
                if (campoAtual != valorFixo)
                    return false;
            }

            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            if (campoAtual != "0" && campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "4" && campoAtual != "5" && campoAtual != "9")
                return false;
            return true;
        }
        public bool ValidarDescricao_G004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            if (campoAtual.Length < tamanho)
                return false;
            if (campoAtual != "1" && campoAtual != "2" && campoAtual != "3" && campoAtual != "9" )
                return false;
            return true;
        }
        public bool ValidarDescricao_G006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (!VerificarSeNumerico(campoAtual))
                return false;
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G009(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            if (campoAtual.Length < tamanho)
                return false;
            return true;
        }
        public bool ValidarDescricao_G010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G013(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G015(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G016(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G017(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G018(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G019(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G020(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G021(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G022(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G025(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G028(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G029(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G030(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G031(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G032(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G033(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G034(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G035(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G036(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G037(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G038(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G039(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G040(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G041(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G042(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G043(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G044(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G045(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G046(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G047(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G048(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G049(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G050(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G051(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G052(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G053(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G054(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G055(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G056(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G057(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G058(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G059(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G060(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G061(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G062(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G063(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G064(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G065(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G066(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G067(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G070(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G071(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G073(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G074(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_G075(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_L002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_L003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N009(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N013(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N021(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N023(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N024(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N025(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N026(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N027(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N028(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_N029(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P003(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P004(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P005(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P006(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P007(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P008(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P009(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P010(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P011(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P012(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P014(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_P015(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_Z001(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }
        public bool ValidarDescricao_Z002(string parametrosAtuais, string parametrosAnteriores, string linhaAtual, string linhaAnterior)
        {
            TransferirParametros(parametrosAtuais, parametrosAnteriores, linhaAtual, linhaAnterior);
            var teste = "Ä";
            return true;
        }

        #region METODOS AUXILIARES

        public List<string> CriarTabelaG059()
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

        public bool VerificarSeExisteOcorrenciaG059(string elemento, List<string> ocorrencias)
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

        public static List<string> CriarListaC004()
        {
            return new List<string>
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09",
                "10", "11", "12", "13", "14", "15", "16", "17", "18",
                "19", "20", "21", "22", "23", "24", "30", "31", "33",
                "34", "35", "40", "41", "42", "43", "44", "45", "46"
            };
        }

        public bool VerificarIsStringInListC004(string input, List<string> list)
        {
            return list.Contains(input);
        }

        public List<string> CriarListaA001()
        {
            return new List<string>
            {
                "0101", "0102", "0103", "0104", "0105", "0106", "0107", "0108", "0109",
                "0201", "0202", "0203", "0204", "0205", "0206", "0207", "0208",
                "0301", "0302", "0303", "0304", "0305", "0306",
                "0401", "0402", "0403", "0404", "0405", "0406", "0407", "0408", "0409",
                "0501", "0502", "0503", "0504", "0505",
                "0601", "0602", "0603", "0604", "0605", "0606", "0607", "0608", "0609",
                "0610", "0611", "0612", "0613", "0614", "0615", "0616", "0617"
            };
        }

        public bool VerificarIsStringInListA001(string input, List<string> list)
        {
            return list.Contains(input);
        }

        public List<string> CriarListaC015()
        {
            return new List<string>
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09",
                "10", "11", "12", "13", "14", "15", "16", "17", "18",
                "19", "20", "21", "22", "23", "24", "25", "26", "27"
            };
        }

        public bool VerificarIsStringInListC015(string input, List<string> list)
        {
            return list.Contains(input);
        }

        public List<string> CriarListaC021()
        {
            return new List<string>
            {
                "1", "2", "3", "4", "5", "6", "7"
            };
        }

        public bool VerificarIsStringInListC021(string input, List<string> list)
        {
            return list.Contains(input);
        }

        public List<string> CriarListaC026()
        {
            return new List<string>
            {
                "1","2","3","4","5","8","9"
            };
        }

        public bool VerificarIsStringInListC026(string input, List<string> list)
        {
            return list.Contains(input);
        }

        public bool VerificarSeNumerico(string valor)
        {
            return decimal.TryParse(valor, out _);
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

            campoAtual = linhaAtual.Substring(posicaoInicial, tamanho).Trim();
            campoAnterior = linhaAnterior.Substring(posicaoInicialAnterior, tamanhoAnterior).Trim();

        }
        #endregion
    }
}
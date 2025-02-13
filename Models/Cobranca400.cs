using BRD_API_NF_4_7_2_TRANSMISSAO.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Models
{
    public class Cobranca400
    {
        #region objetos públicos
        Util util = new Util();
        public List<string> listaDeErros = new List<string>();
        public int linhaAtual = 0;
        const string erroNome = "Erro na linha : ";
        const string idNome = " Id Registro : ";
        #endregion

        #region Propriedades do Header
        public string IdentificacaoRegistro { get; set; } // 001 a 001 >>> N <<<
        public string IdentificacaoArquivoRemessa { get; set; } // 002 a 002 >>> N <<<
        public string LiteralRemessa { get; set; } // 003 a 009
        public string CodigoServico { get; set; } // 010 a 011 >>> N <<<
        public string LiteralServico { get; set; } // 012 a 026
        public string CodigoEmpresa { get; set; } // 027 a 046 >>> N <<<
        public string NomeEmpresa { get; set; } // 047 a 076
        public string NumeroBradescoCompensacao { get; set; } // 077 a 079 >>> N <<<
        public string NomeBancoExtenso { get; set; } // 080 a 094
        public string DataGravacaoArquivo { get; set; } // 095 a 100 (DDMMAA) >>> N <<<
        public string Branco1 { get; set; } // 101 a 108
        public string IdentificacaoSistema { get; set; } // 109 a 110
        public string NumeroSequencialRemessa { get; set; } // 111 a 117 >>> N <<<
        public string Branco2 { get; set; } // 118 a 394
        public string NumeroSequencialRegistro { get; set; } // 395 a 400 >>> N <<<
        #endregion

        #region Propriedades Tipo 1
        public string IdentificacaoRegistroTipo1 { get; set; } // 001 a 001 N
        public string AgenciaDebito { get; set; } // 002 a 006 N
        public string DigitoAgencia { get; set; } // 007 a 007
        public string RazaoContaCorrente { get; set; }  // 008 a 012 N
        public string ContaCorrente { get; set; }  // 013 a 019 N
        public string DigitoContaCorrente { get; set; } // 020 a 020
        public string IdEmpresaBeneficiariaNanco { get; set; }  // 021 a 037
        public string NumeroControlePaticipante { get; set; }  // 038 a 062
        public string CodigoBancoDebitadoCamaraCompensacao { get; set; }  // 063 a 065 N
        public string CampoMulta { get; set; }  // 066 a 066 N
        public string PercentualMulta { get; set; }  // 067 a 070 N
        public string IdentificacaoTituloBanco { get; set; }  // 071 a 081 N
        public string DigitoAutoconferenciaNumeroBancario { get; set; }  // 082 a 082
        public string DescontoBonificacaoDia { get; set; }  // 083 a 092 N
        public string CondicaoEmissaoPapeletaCobranca { get; set; }  // 093 a 093 N
        public string IdentEmiteBoletoDebitoAutomaticao { get; set; }  // 094 a 094
        public string IdentificacaoOperacaoBanco { get; set; }  // 095 a 104
        public string IndicadorRateioCredito { get; set; }  // 105 a 105
        public string EnderecamentoAvisoDebitoAutomaticoContaCorrente { get; set; }  // 106 a 106 N
        public string QuantidadePagamentos { get; set; }  // 107 a 108
        public string IdentificacaoOcorrencia { get; set; }  // 109 a 110 N
        public string NumeroDocumento { get; set; }  // 111 a 120
        public string DataVencimentoTitulo { get; set; }  // 121 a 126 N
        public string ValorTitulo { get; set; }  // 127 a 139 N
        public string BancoEncarregadoCobranca { get; set; }  // 140 a 142 N
        public string AgenciaDepositaria { get; set; }  // 143 a 147 N
        public string EspecieTitulo { get; set; }  // 148 a 149 N
        public string Identificacao { get; set; }  // 150 a 150
        public string DataEmissaoTitulo { get; set; }  // 151 a 156 N
        public string PrimeiraInstrucao { get; set; }  // 157 a 158 N
        public string SegundaInstrucao { get; set; }  // 159 a 160 N
        public string ValorCobradoDiaAtraso { get; set; }  // 161 a 173 N
        public string DataLimiteConcessaoDesconto { get; set; }  // 174 a 179 N
        public string ValorDesconto { get; set; }  // 180 a 192 N
        public string ValorIof { get; set; }  // 193 a 205 N
        public string ValorAbatimentoConcedidoOuCancelado { get; set; }  // 206 a 218 N
        public string IdentificacaoTipoInscricaoPagador { get; set; }  // 219 a 220 N
        public string NumeroInscricaoPagador { get; set; }  // 221 a 234 N
        public string NomeDoPagador { get; set; }  // 235 a 274
        public string EnderecoCompleto { get; set; }  // 275 a 314
        public string PrimeiraMensagem { get; set; }  // 315 a 326
        public string Cep { get; set; }  // 327 a 331 N
        public string SufixoCep { get; set; } // 332 a 334 N
        public string BeneficiarioFinalSegundaMensagem { get; set; }  // 335 a 394
        public string NumeroSequencialRegistroTipo1 { get; set; }  // 395 a 400 N
        #endregion

        #region Propriedades Tipo 2
        public string TipoRegistro { get; private set; }
        public string Mensagem1 { get; private set; }
        public string Mensagem2 { get; private set; }
        public string Mensagem3 { get; private set; }
        public string Mensagem4 { get; private set; }
        public string DataLimiteDesconto2 { get; private set; }
        public string ValorDesconto2 { get; private set; }
        public string DataLimiteDesconto3 { get; private set; }
        public string ValorDesconto3 { get; private set; }
        public string Filler { get; private set; }
        public string Carteira { get; private set; }
        public string Agencia { get; private set; }
        public string ContaCorrente2 { get; private set; }
        public string DigitoCC { get; private set; }
        public string NossoNumero { get; private set; }
        public string DacNossoNumero { get; private set; }
        public string NumeroSequencialRegistro2 { get; private set; }
        #endregion

        #region Propriedades Tipo 3
        public string IdentificacaoRegistro3 { get; set; } // 001 a 001
        public string IdentificacaoEmpresa { get; set; } // 002 a 017
        public string IdentificacaoTitulo { get; set; } // 018 a 029
        public string CodigoCalculoRateio { get; set; } // 030 a 030
        public string TipoValorInformado { get; set; } // 031 a 031
        public string Filler1 { get; set; } // 032 a 043
        public string CodigoBanco1 { get; set; } // 044 a 046
        public string CodigoAgencia1 { get; set; } // 047 a 051
        public string DigitoAgencia1 { get; set; } // 052 a 052
        public string NumeroConta1 { get; set; } // 053 a 064
        public string DigitoConta1 { get; set; } // 065 a 065
        public string ValorPercentualRateio1 { get; set; } // 066 a 080
        public string NomeBeneficiario1 { get; set; } // 081 a 120
        public string Filler2 { get; set; } // 121 a 151
        public string Parcela1 { get; set; } // 152 a 157
        public string Floating1 { get; set; } // 158 a 160
        public string CodigoBanco2 { get; set; } // 161 a 163
        public string CodigoAgencia2 { get; set; } // 164 a 168
        public string DigitoAgencia2 { get; set; } // 169 a 169
        public string NumeroConta2 { get; set; } // 170 a 181
        public string DigitoConta2 { get; set; } // 182 a 182
        public string ValorPercentualRateio2 { get; set; } // 183 a 197
        public string NomeBeneficiario2 { get; set; } // 198 a 237
        public string Filler3 { get; set; } // 238 a 268
        public string Parcela2 { get; set; } // 269 a 274
        public string Floating2 { get; set; } // 275 a 277
        public string CodigoBanco3 { get; set; } // 278 a 280
        public string CodigoAgencia3 { get; set; } // 281 a 285
        public string DigitoAgencia3 { get; set; } // 286 a 286
        public string NumeroConta3 { get; set; } // 287 a 298
        public string DigitoConta3 { get; set; } // 299 a 299
        public string ValorPercentualRateio3 { get; set; } // 300 a 314
        public string NomeBeneficiario3 { get; set; } // 315 a 354
        public string Filler4 { get; set; } // 355 a 385
        public string Parcela3 { get; set; } // 386 a 391
        public string Floating3 { get; set; } // 392 a 394
        public string NumeroSequencialRegistro3 { get; set; } // 395 a 400
        #endregion

        #region Header
        #region Metodos do Header
        public void LerLinhaHeader(string linha)
        {
            IdentificacaoRegistro = linha.Substring(0, 1);
            IdentificacaoArquivoRemessa = linha.Substring(1, 1);
            LiteralRemessa = linha.Substring(2, 7);
            CodigoServico = linha.Substring(9, 2);
            LiteralServico = linha.Substring(11, 15);
            CodigoEmpresa = linha.Substring(26, 20);
            NomeEmpresa = linha.Substring(46, 30);
            NumeroBradescoCompensacao = linha.Substring(76, 3);
            NomeBancoExtenso = linha.Substring(79, 15);
            DataGravacaoArquivo = linha.Substring(94, 6);
            Branco1 = linha.Substring(100, 8);
            IdentificacaoSistema = linha.Substring(108, 2);
            NumeroSequencialRemessa = linha.Substring(110, 7);
            Branco2 = linha.Substring(117, 277);
            NumeroSequencialRegistro = linha.Substring(394, 6);
        }

        public void TrataErrosHeader()
        {
            if (!util.EhNumerico(IdentificacaoRegistro))  // 001 a 001 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " IdentificacaoRegistro Posição : 001 a 001");
            if (!util.EhNumerico(IdentificacaoArquivoRemessa))  // 002 a 002 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " IdentificacaoArquivoRemessa Posição : 002 a 002");
            if (!util.EhNumerico(CodigoServico)) // 010 a 011 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " CodigoServico Posição : 010 a 011");
            if (!util.EhNumerico(CodigoEmpresa))  // 027 a 046 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " CodigoEmpresa Posição : 027 a 046");
            if (!util.EhNumerico(NumeroBradescoCompensacao))  // 077 a 079 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " NumeroBradescoCompensacao Posição : 077 a 079");
            if (!util.EhNumerico(DataGravacaoArquivo))  // 095 a 100 (DDMMAA) >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " DataGravacaoArquivo Posição : 095 a 100");
            if (!util.EhNumerico(NumeroSequencialRemessa))  // 111 a 117 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " NumeroSequencialRemessa Posição : 111 a 117");
            if (!util.EhNumerico(NumeroSequencialRegistro))  // 395 a 400 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro.ToString() + " NumeroSequencialRegistro Posição : 395 a 400");
        }
        #endregion
        #endregion

        #region Tipo 1
        #region Metodos do Tipo 1
        public void LerLinhaTipo1(string linha)
        {
            IdentificacaoRegistroTipo1 = linha.Substring(0, 1);
            AgenciaDebito = linha.Substring(1, 5);
            DigitoAgencia = linha.Substring(6, 1);
            RazaoContaCorrente = linha.Substring(7, 5);
            ContaCorrente = linha.Substring(12, 7);
            DigitoContaCorrente = linha.Substring(19, 1);
            IdEmpresaBeneficiariaNanco = linha.Substring(20, 17);
            NumeroControlePaticipante = linha.Substring(37, 25);
            CodigoBancoDebitadoCamaraCompensacao = linha.Substring(62, 3);
            CampoMulta = linha.Substring(65, 1);
            PercentualMulta = linha.Substring(66, 4);
            IdentificacaoTituloBanco = linha.Substring(70, 11);
            DigitoAutoconferenciaNumeroBancario = linha.Substring(81, 1);
            DescontoBonificacaoDia = linha.Substring(82, 10);
            CondicaoEmissaoPapeletaCobranca = linha.Substring(92, 1);
            IdentEmiteBoletoDebitoAutomaticao = linha.Substring(93, 1);
            IdentificacaoOperacaoBanco = linha.Substring(94, 10);
            IndicadorRateioCredito = linha.Substring(104, 1);
            EnderecamentoAvisoDebitoAutomaticoContaCorrente = linha.Substring(105, 1);
            QuantidadePagamentos = linha.Substring(106, 2);
            IdentificacaoOcorrencia = linha.Substring(108, 2);
            NumeroDocumento = linha.Substring(110, 10);
            DataVencimentoTitulo = linha.Substring(120, 6);
            ValorTitulo = linha.Substring(126, 13);
            BancoEncarregadoCobranca = linha.Substring(139, 3);
            AgenciaDepositaria = linha.Substring(142, 5);
            EspecieTitulo = linha.Substring(147, 2);
            Identificacao = linha.Substring(149, 1);
            DataEmissaoTitulo = linha.Substring(150, 6);
            PrimeiraInstrucao = linha.Substring(156, 2);
            SegundaInstrucao = linha.Substring(158, 2);
            ValorCobradoDiaAtraso = linha.Substring(160, 13);
            DataLimiteConcessaoDesconto = linha.Substring(173, 6);
            ValorDesconto = linha.Substring(179, 13);
            ValorIof = linha.Substring(192, 13);
            ValorAbatimentoConcedidoOuCancelado = linha.Substring(205, 13);
            IdentificacaoTipoInscricaoPagador = linha.Substring(218, 2);
            NumeroInscricaoPagador = linha.Substring(220, 14);
            NomeDoPagador = linha.Substring(234, 40);
            EnderecoCompleto = linha.Substring(274, 40);
            PrimeiraMensagem = linha.Substring(314, 12);
            Cep = linha.Substring(326, 5);
            SufixoCep = linha.Substring(331, 3);
            BeneficiarioFinalSegundaMensagem = linha.Substring(334, 60);
            NumeroSequencialRegistroTipo1 = linha.Substring(394, 6);
        }
        public void TrataErrosTipo1()
        {
            if (!util.EhNumerico(IdentificacaoRegistroTipo1)) // 001 a 001 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " IdentificacaoRegistroTipo1 Posição : 001 a 001");

            if (!util.EhNumerico(AgenciaDebito))// 002 a 006 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " AgenciaDebito Posição : 002 a 006");

            if (!util.EhNumerico(RazaoContaCorrente)) // 008 a 012 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " RazaoContaCorrente Posição : 008 a 012");

            if (!util.EhNumerico(ContaCorrente)) // 013 a 019 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ContaCorrente Posição : 013 a 019");

            if (!util.EhNumerico(CodigoBancoDebitadoCamaraCompensacao)) // 063 a 065
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " CodigoBancoDebitadoCamaraCompensacao Posição : 063 a 065");

            if (!util.EhNumerico(CampoMulta)) // 066 a 066 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " CampoMulta Posição : 066 a 066");

            if (!util.EhNumerico(PercentualMulta)) // 067 a 070 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " PercentualMulta Posição : 067 a 070");

            if (!util.EhNumerico(IdentificacaoTituloBanco)) // 071 a 081 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " IdentificacaoTituloBanco Posição : 071 a 081");

            if (!util.EhNumerico(DescontoBonificacaoDia)) // 083 a 092 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " DescontoBonificacaoDia Posição : 083 a 092");

            if (!util.EhNumerico(CondicaoEmissaoPapeletaCobranca)) // 093 a 093 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " CondicaoEmissaoPapeletaCobranca Posição : 093 a 093");

            if (!util.EhNumerico(EnderecamentoAvisoDebitoAutomaticoContaCorrente)) // 106 a 106 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " EnderecamentoAvisoDebitoAutomaticoContaCorrente Posição : 106 a 106");

            if (!util.EhNumerico(IdentificacaoOcorrencia)) // 109 a 110 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " IdentificacaoOcorrencia Posição : 109 a 110");

            if (!util.EhNumerico(DataVencimentoTitulo)) // 121 a 126 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " DataVencimentoTitulo Posição : 121 a 126");

            if (!util.EhNumerico(ValorTitulo)) // 127 a 139 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ValorTitulo Posição : 127 a 139");

            if (!util.EhNumerico(BancoEncarregadoCobranca)) // 140 a 142 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " BancoEncarregadoCobranca Posição : 140 a 142");

            if (!util.EhNumerico(AgenciaDepositaria)) // 143 a 147 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " AgenciaDepositaria Posição : 143 a 147");

            if (!util.EhNumerico(EspecieTitulo)) // 148 a 149 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " EspecieTitulo Posição : 148 a 149");

            if (!util.EhNumerico(DataEmissaoTitulo)) // 151 a 156 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " DataEmissaoTitulo Posição : 151 a 156");

            if (!util.EhNumerico(PrimeiraInstrucao)) // 157 a 158 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " PrimeiraInstrucao Posição : 157 a 158");

            if (!util.EhNumerico(SegundaInstrucao)) // 159 a 160 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " SegundaInstrucao Posição : 159 a 160");

            if (!util.EhNumerico(ValorCobradoDiaAtraso)) // 161 a 173 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ValorCobradoDiaAtraso Posição : 161 a 173");

            if (!util.EhNumerico(DataLimiteConcessaoDesconto)) // 174 a 179 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " DataLimiteConcessaoDesconto Posição : 174 a 179");

            if (!util.EhNumerico(ValorDesconto)) // 180 a 192 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ValorDesconto Posição : 180 a 192");

            if (!util.EhNumerico(ValorIof)) // 193 a 205 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ValorIof Posição : 193 a 205");

            if (!util.EhNumerico(ValorAbatimentoConcedidoOuCancelado)) // 206 a 218 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " ValorAbatimentoConcedidoOuCancelado Posição : 206 a 218");

            if (!util.EhNumerico(IdentificacaoTipoInscricaoPagador)) // 219 a 220 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " IdentificacaoTipoInscricaoPagador Posição : 219 a 220");

            if (!util.EhNumerico(NumeroInscricaoPagador)) // 221 a 234 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " NumeroInscricaoPagador Posição : 221 a 234");

            if (!util.EhNumerico(Cep)) // 327 a 331 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " Cep Posição : 327 a 331");

            if (!util.EhNumerico(SufixoCep))// 332 a 334 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " SufixoCep Posição : 332 a 334");

            if (!util.EhNumerico(NumeroSequencialRegistroTipo1)) // 395 a 400 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " NumeroSequencialRegistroTipo1 Posição : 395 a 400");
        }

        public void ValidarCamposTipo1()
        {
            // Valida Multa + Percentual
            if ((CampoMulta == "2" && Convert.ToInt32(PercentualMulta) == 0) || (CampoMulta != "2" && Convert.ToInt32(PercentualMulta) > 0))
            {
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " Posição : 066 a 070 {Campo de Multa + Percentual de Multa");
            }
            List<string> codigosValidos = new List<string>
            {
                "01", "02", "03", "05", "10", "11", "12", "31", "32", "33", "99"
            };
            if (!codigosValidos.Contains(EspecieTitulo))
            {
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " Posição : 148 a 149 {Campo Espécie de título}");
            }

            // Valida IdentificacaoTipoInscricaoPagador
            List<string> codigosValidosDocumento = new List<string>
            {
                "01", "02"
            };
            if (!codigosValidosDocumento.Contains(IdentificacaoTipoInscricaoPagador))
            {
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " Posição : 219 a 220 {Campo Identificação do Tipo de Inscrição do Pagador}");
            }

            // Valida CPF e CNPJ
            if (NumeroInscricaoPagador == "00000000000000")
            {
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistroTipo1.ToString() + " Posição : 221 a 234 {Campo Número de Inscrição do Pagador}");
            }
        }
        #endregion
        #endregion

        #region Tipo 2
        #region Métodos Tipo 2
        public void LerLinhaTipo2(string linha)
        {
            TipoRegistro = linha.Substring(0, 1);
            Mensagem1 = linha.Substring(1, 80).Trim();
            Mensagem2 = linha.Substring(81, 80).Trim();
            Mensagem3 = linha.Substring(161, 80).Trim();
            Mensagem4 = linha.Substring(241, 80).Trim();
            DataLimiteDesconto2 = linha.Substring(321, 6);
            ValorDesconto2 = linha.Substring(327, 13).Trim();
            DataLimiteDesconto3 = linha.Substring(340, 6);
            ValorDesconto3 = linha.Substring(346, 13).Trim();
            Filler = linha.Substring(359, 7);
            Carteira = linha.Substring(366, 3);
            Agencia = linha.Substring(369, 5);
            ContaCorrente2 = linha.Substring(374, 7);
            DigitoCC = linha.Substring(381, 1);
            NossoNumero = linha.Substring(382, 11);
            DacNossoNumero = linha.Substring(393, 1);
            NumeroSequencialRegistro2 = linha.Substring(394, 6);
        }
        public void TrataErrosTipo2()
        {
            if (!util.EhNumerico(IdentificacaoRegistro))  // 001 a 001 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + TipoRegistro.ToString() + " TipoRegistro Posição : 001 a 001");
            if (!util.EhNumerico(IdentificacaoArquivoRemessa))  // 322 a 327 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + DataLimiteConcessaoDesconto.ToString() + " DataLimiteConcessaoDesconto2 Posição : 322 a 327");
            if (!util.EhNumerico(CodigoServico)) // 328 a 340 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ValorDesconto2.ToString() + " ValorDesconto2 Posição : 328 a 340");
            if (!util.EhNumerico(CodigoEmpresa))  // 341 a 346 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + DataLimiteDesconto3.ToString() + " DataLimiteDesconto3 Posição : 341 a 346");
            if (!util.EhNumerico(NumeroBradescoCompensacao))  // 347 a 359 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ValorDesconto3.ToString() + " ValorDesconto3 Posição : 347 a 359");
            if (!util.EhNumerico(DataGravacaoArquivo))  // 367 a 369 (DDMMAA) >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + Carteira.ToString() + " Carteira Posição : 367 a 369");
            if (!util.EhNumerico(NumeroSequencialRemessa))  // 370 a 374 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + Agencia.ToString() + " Agencia Posição : 370 a 374");
            if (!util.EhNumerico(NumeroSequencialRegistro))  // 375 a 381 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ContaCorrente2.ToString() + " ContaCorrente Posição : 375 a 381");
            if (!util.EhNumerico(NumeroSequencialRegistro))  // 383 a 393 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NossoNumero.ToString() + " NossoNumero Posição : 383 a 393");
            if (!util.EhNumerico(NumeroSequencialRegistro))  // 395 a 400 >>> N <<<
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro2.ToString() + " NumeroSequencialRegistro Posição : 395 a 400");
            // Verifica as mensagens se > zero , deve ter no mínimo 41 caracteres
            if (Mensagem1.Trim().Length < 41 && Mensagem1.Trim().Length > 0)
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro2.ToString() + " Mensagem1 Posição : 002 a 081 , menor que 41 caracteres");
            if (Mensagem2.Trim().Length < 41 && Mensagem2.Trim().Length > 0)
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro2.ToString() + " Mensagem2 Posição : 082 a 161 , menor que 41 caracteres");
            if (Mensagem3.Trim().Length < 41 && Mensagem3.Trim().Length > 0)
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro2.ToString() + " Mensagem3 Posição : 162 a 241 , menor que 41 caracteres");
            if (Mensagem4.Trim().Length < 41 && Mensagem4.Trim().Length > 0)
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro2.ToString() + " Mensagem4 Posição : 242 a 321 , menor que 41 caracteres");
        }
        #endregion
        #endregion

        #region Tipo3
        #region Métodos Tipo 3
        public void LerLinhaTipo3(string linha)
        {
            IdentificacaoRegistro3 = linha.Substring(0, 1);
            IdentificacaoEmpresa = linha.Substring(1, 16).Trim();
            IdentificacaoTitulo = linha.Substring(17, 12).Trim();
            CodigoCalculoRateio = linha.Substring(29, 1);
            TipoValorInformado = linha.Substring(30, 1);
            Filler1 = linha.Substring(31, 12).Trim();
            CodigoBanco1 = linha.Substring(43, 3);
            CodigoAgencia1 = linha.Substring(46, 5);
            DigitoAgencia1 = linha.Substring(51, 1);
            NumeroConta1 = linha.Substring(52, 12);
            DigitoConta1 = linha.Substring(64, 1);
            ValorPercentualRateio1 = linha.Substring(65, 15).Trim();
            NomeBeneficiario1 = linha.Substring(80, 40).Trim();
            Filler2 = linha.Substring(120, 31).Trim();
            Parcela1 = linha.Substring(151, 6).Trim();
            Floating1 = linha.Substring(157, 3).Trim();
            CodigoBanco2 = linha.Substring(160, 3);
            CodigoAgencia2 = linha.Substring(163, 5);
            DigitoAgencia2 = linha.Substring(168, 1);
            NumeroConta2 = linha.Substring(169, 12);
            DigitoConta2 = linha.Substring(181, 1);
            ValorPercentualRateio2 = linha.Substring(182, 15).Trim();
            NomeBeneficiario2 = linha.Substring(197, 40).Trim();
            Filler3 = linha.Substring(237, 31).Trim();
            Parcela2 = linha.Substring(268, 6).Trim();
            Floating2 = linha.Substring(274, 3).Trim();
            CodigoBanco3 = linha.Substring(277, 3);
            CodigoAgencia3 = linha.Substring(280, 5);
            DigitoAgencia3 = linha.Substring(285, 1);
            NumeroConta3 = linha.Substring(286, 12);
            DigitoConta3 = linha.Substring(298, 1);
            ValorPercentualRateio3 = linha.Substring(299, 15).Trim();
            NomeBeneficiario3 = linha.Substring(314, 40).Trim();
            Filler4 = linha.Substring(354, 31).Trim();
            Parcela3 = linha.Substring(385, 6).Trim();
            Floating3 = linha.Substring(391, 3).Trim();
            NumeroSequencialRegistro3 = linha.Substring(394, 6).Trim();
        }
        public void TrataErrosTipo3()
        {
            if (!util.EhNumerico(IdentificacaoRegistro3))  // 001 a 001 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + IdentificacaoRegistro3.ToString() + " IdentificacaoRegistro Posição : 001 a 001");
            if (!util.EhNumerico(CodigoCalculoRateio))  // 030 a 030 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoCalculoRateio.ToString() + " DataLimiteConcessaoDesconto2 Posição : 322 a 327");
            if (!util.EhNumerico(TipoValorInformado)) // 031 a 031 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + TipoValorInformado.ToString() + " ValorDesconto2 Posição : 328 a 340");
            if (!util.EhNumerico(CodigoBanco1))  // 044 a 046 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoBanco1.ToString() + " DataLimiteDesconto3 Posição : 341 a 346");
            if (!util.EhNumerico(CodigoAgencia1))  // 047 a 051 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoAgencia1.ToString() + " ValorDesconto3 Posição : 347 a 359");
            if (!util.EhNumerico(NumeroConta1))  // 053 a 064 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroConta1.ToString() + " Carteira Posição : 367 a 369");
            if (!util.EhNumerico(ValorPercentualRateio1))  // 066 a 080
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ValorPercentualRateio1.ToString() + " Agencia Posição : 370 a 374");
            if (!util.EhNumerico(Floating1))  // 158 a 160 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + Floating1.ToString() + " ContaCorrente Posição : 375 a 381");
            if (!util.EhNumerico(CodigoBanco2))  // 161 a 163 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoBanco2.ToString() + " NossoNumero Posição : 383 a 393");
            if (!util.EhNumerico(CodigoAgencia2))  // 164 a 168 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoAgencia2.ToString() + " NumeroSequencialRegistro Posição : 395 a 400");
            if (!util.EhNumerico(NumeroConta2))  // 170 a 181 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroConta2.ToString() + " DataLimiteConcessaoDesconto2 Posição : 322 a 327");
            if (!util.EhNumerico(ValorPercentualRateio2)) // 183 a 197 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ValorPercentualRateio2.ToString() + " ValorDesconto2 Posição : 328 a 340");
            if (!util.EhNumerico(Floating2))  // 275 a 277 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + Floating2.ToString() + " DataLimiteDesconto3 Posição : 341 a 346");
            if (!util.EhNumerico(CodigoBanco3))  // 278 a 280
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoBanco3.ToString() + " ValorDesconto3 Posição : 347 a 359");
            if (!util.EhNumerico(CodigoAgencia3))  // 281 a 285
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + CodigoAgencia3.ToString() + " Carteira Posição : 367 a 369");
            if (!util.EhNumerico(NumeroConta3))  // 287 a 298
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroConta3.ToString() + " Agencia Posição : 370 a 374");
            if (!util.EhNumerico(ValorPercentualRateio3))  // 300 a 314 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + ValorPercentualRateio3.ToString() + " ContaCorrente Posição : 375 a 381");
            if (!util.EhNumerico(Floating3))  // 392 a 394 
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + Floating3.ToString() + " NossoNumero Posição : 383 a 393");
            if (!util.EhNumerico(NumeroSequencialRegistro3))  // 395 a 400
                listaDeErros.Add(erroNome + linhaAtual.ToString() + idNome + NumeroSequencialRegistro3.ToString() + " NumeroSequencialRegistro Posição : 395 a 400");
        }
        #endregion
        #endregion

        #region Motor Inicial
        public async Task<List<string>> ExecutaArquivoAsync(FileInfo fileRows)
        {
            listaDeErros.Clear();
            using (var reader = new StreamReader(fileRows.FullName))
            {
                string linha;
                while ((linha = await reader.ReadLineAsync()) != null)
                {
                    switch (linha.Substring(0, 1))
                    {
                        case "0":
                            LerLinhaHeader(linha);
                            linhaAtual++;
                            TrataErrosHeader();
                            break;
                        case "1":
                            LerLinhaTipo1(linha);
                            linhaAtual++;
                            TrataErrosTipo1();
                            ValidarCamposTipo1();
                            break;
                        case "2":
                            LerLinhaTipo2(linha);
                            linhaAtual++;
                            TrataErrosTipo2();
                            break;
                        case "3":
                            LerLinhaTipo3(linha);
                            linhaAtual++;
                            TrataErrosTipo3();
                            break;
                        default:
                            break;
                    }
                }
            }
            return listaDeErros;
        }
        #endregion
    }
}


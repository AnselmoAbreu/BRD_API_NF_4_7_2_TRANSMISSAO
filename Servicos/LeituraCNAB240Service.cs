using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraCNAB240Service
    {
        #region Constantes CNAB 240
        private const string REGISTRO_HEADER_ARQUIVO = "0";
        private const string REGISTRO_HEADER_LOTE = "1";
        private const string REGISTRO_DETALHE = "3";
        private const string REGISTRO_TRAILER_LOTE = "5";
        private const string REGISTRO_TRAILER_ARQUIVO = "9";
        #endregion

        #region Campos e Propriedades
        public List<string> listaDeErros = new List<string>();
        private Util util = new Util();
        private bool erro = false;

        private int totalLotesProcessados = 0;
        private int totalRegistrosProcessados = 0;
        private decimal valorTotalCalculado = 0m;
        private int totalLotesDeclarado = 0;
        private int totalRegistrosDeclarado = 0;
        private decimal valorTotalDeclarado = 0m;
        private decimal valorTotalLoteAtual = 0m;
        private int totalRegistrosLoteAtual = 0;
        #endregion

        #region Métodos Públicos
        public string ExtrairConteudo(string linha, int posicaoInicial, int tamanho)
        {
            if (string.IsNullOrEmpty(linha) || posicaoInicial < 0 || posicaoInicial + tamanho > linha.Length)
                return string.Empty;

            return linha.Substring(posicaoInicial, tamanho);
        }

        public async Task<List<string>> ProcessarArquivo(byte[] fileRows)
        {
            listaDeErros.Clear();
            totalLotesProcessados = 0;
            totalRegistrosProcessados = 0;
            valorTotalCalculado = 0m;

            using (var memoryStream = new MemoryStream(fileRows))
            using (var reader = new StreamReader(memoryStream))
            {
                string linha;
                int linhaIndex = 0;

                while ((linha = await reader.ReadLineAsync()) != null)
                {
                    linhaIndex++;

                    if (linha.Length != 240)
                    {
                        listaDeErros.Add($"Erro na linha {linhaIndex}: Registro deve ter exatamente 240 caracteres.");
                        continue;
                    }

                    string tipoRegistro = linha.Substring(0, 1);
                    totalRegistrosProcessados++;

                    ValidarRegistro(linha, tipoRegistro, linhaIndex);
                }
            }

            ValidarTotais();
            return listaDeErros;
        }
        #endregion

        #region Métodos Privados de Validação
        private void ValidarRegistro(string linha, string tipoRegistro, int linhaIndex)
        {
            erro = false;

            switch (tipoRegistro)
            {
                case REGISTRO_HEADER_ARQUIVO:
                    ValidarHeaderArquivo(linha, linhaIndex);
                    break;

                case REGISTRO_HEADER_LOTE:
                    ValidarHeaderLote(linha, linhaIndex);
                    totalLotesProcessados++;
                    valorTotalLoteAtual = 0m;
                    totalRegistrosLoteAtual = 0;
                    break;

                case REGISTRO_DETALHE:
                    ValidarDetalhe(linha, linhaIndex);
                    totalRegistrosLoteAtual++;
                    break;

                case REGISTRO_TRAILER_LOTE:
                    ValidarTrailerLote(linha, linhaIndex);
                    break;

                case REGISTRO_TRAILER_ARQUIVO:
                    ValidarTrailerArquivo(linha, linhaIndex);
                    break;

                default:
                    listaDeErros.Add($"Erro na linha {linhaIndex}: Tipo de registro desconhecido ({tipoRegistro}).");
                    break;
            }
        }

        private void ValidarHeaderArquivo(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 1, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 8, 1, "N", "0"); // Tipo de registro fixo
            ValidarConteudoCampo(linha, linhaIndex, 144, 8, "N"); // Data de geração
        }

        private void ValidarHeaderLote(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 8, 1, "N", "1"); // Tipo de registro fixo
        }

        private void ValidarTrailerLote(string linha, int linhaIndex)
        {
            int totalRegistrosLote = Convert.ToInt32(ExtrairConteudo(linha, 18, 6));
            decimal valorTotalLote = ExtrairDecimal(linha, 24, 16);

            if (totalRegistrosLoteAtual != totalRegistrosLote)
                listaDeErros.Add($"Erro na linha {linhaIndex}: Total de registros no lote ({totalRegistrosLoteAtual}) diferente do informado no trailer ({totalRegistrosLote}).");

            if (valorTotalLoteAtual != valorTotalLote)
                listaDeErros.Add($"Erro na linha {linhaIndex}: Valor total do lote ({valorTotalLoteAtual}) diferente do informado no trailer ({valorTotalLote}).");

            valorTotalCalculado += valorTotalLoteAtual;
        }

        private void ValidarTrailerArquivo(string linha, int linhaIndex)
        {
            totalLotesDeclarado = Convert.ToInt32(ExtrairConteudo(linha, 18, 6));
            totalRegistrosDeclarado = Convert.ToInt32(ExtrairConteudo(linha, 24, 6));
            valorTotalDeclarado = ExtrairDecimal(linha, 42, 16);
        }

        private void ValidarConteudoCampo(string linha, int linhaIndex, int posicao, int tamanho, string tipo, string valorDefault = "")
        {
            string conteudo = ExtrairConteudo(linha, posicao, tamanho);

            if (tipo == "N" && !conteudo.PadLeft(tamanho, '0').Equals(conteudo))
                listaDeErros.Add($"Erro na linha {linhaIndex}: Campo numérico deve estar alinhado à direita com zeros na posição {posicao}.");

            if (tipo == "A" && !conteudo.PadRight(tamanho, ' ').Equals(conteudo))
                listaDeErros.Add($"Erro na linha {linhaIndex}: Campo alfanumérico deve estar alinhado à esquerda com espaços à direita na posição {posicao}.");

            if (!string.IsNullOrEmpty(valorDefault) && conteudo.Trim() != valorDefault)
                listaDeErros.Add($"Erro na linha {linhaIndex}: Campo na posição {posicao} deveria ter o valor '{valorDefault}', mas foi encontrado '{conteudo.Trim()}'.");
        }

        private void ValidarTotais()
        {
            if (totalLotesProcessados != totalLotesDeclarado)
                listaDeErros.Add($"Erro: Quantidade de lotes processados ({totalLotesProcessados}) diferente do declarado no trailer ({totalLotesDeclarado}).");

            if (totalRegistrosProcessados != totalRegistrosDeclarado)
                listaDeErros.Add($"Erro: Quantidade de registros processados ({totalRegistrosProcessados}) diferente do declarado no trailer ({totalRegistrosDeclarado}).");

            if (valorTotalCalculado != valorTotalDeclarado)
                listaDeErros.Add($"Erro: Valor total calculado ({valorTotalCalculado}) diferente do declarado no trailer ({valorTotalDeclarado}).");
        }

        private decimal ExtrairDecimal(string linha, int posicao, int tamanho)
        {
            string valor = ExtrairConteudo(linha, posicao, tamanho).Trim();
            if (decimal.TryParse(valor, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                                 CultureInfo.InvariantCulture, out decimal resultado))
            {
                return resultado;
            }
            return 0m;
        }

        private void ValidarDetalhe(string linha, int linhaIndex)
        {
            string segmento = ExtrairConteudo(linha, 13, 1);
            switch (segmento)
            {
                case "A":
                    ValidarSegmentoA(linha, linhaIndex);
                    break;
                case "B":
                    ValidarSegmentoB(linha, linhaIndex);
                    break;
                case "J":
                    ValidarSegmentoJ(linha, linhaIndex);
                    break;
                case "O":
                    ValidarSegmentoO(linha, linhaIndex);
                    break;
                case "N":
                    ValidarSegmentoN(linha, linhaIndex);
                    break;
                case "Z":
                    ValidarSegmentoZ(linha, linhaIndex);
                    break;
                case "W":
                    ValidarSegmentoW(linha, linhaIndex);
                    break;
                case "Y":
                    ValidarSegmentoY02(linha, linhaIndex);
                    break;
                default:
                    listaDeErros.Add($"Erro na linha {linhaIndex}: Segmento '{segmento}' não reconhecido.");
                    break;
            }
        }

        private void ValidarSegmentoA(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 120, 13, "N"); // Valor do pagamento
            ValidarConteudoCampo(linha, linhaIndex, 94, 8, "N"); // Data de pagamento
        }

        private void ValidarSegmentoB(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 126, 2, "A"); // Estado do favorecido
            ValidarConteudoCampo(linha, linhaIndex, 32, 14, "N"); // CPF/CNPJ do favorecido
        }

        private void ValidarSegmentoJ(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 18, 44, "N"); // Código de barras
            ValidarConteudoCampo(linha, linhaIndex, 92, 8, "N"); // Data de vencimento
            ValidarConteudoCampo(linha, linhaIndex, 100, 13, "N"); // Valor do título
        }

        private void ValidarSegmentoO(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 18, 44, "N"); // Código de barras do tributo
            ValidarConteudoCampo(linha, linhaIndex, 108, 13, "N"); // Valor do tributo
        }

        private void ValidarSegmentoN(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 50, 14, "N"); // Número de referência do tributo
            ValidarConteudoCampo(linha, linhaIndex, 65, 8, "N"); // Data de pagamento
        }

        private void ValidarSegmentoZ(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 5, 8, "A"); // Código da autenticação
            ValidarConteudoCampo(linha, linhaIndex, 20, 10, "N"); // Data da autenticação
        }

        private void ValidarSegmentoW(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 92, 10, "N"); // Identificação do tributo
        }

        private void ValidarSegmentoY02(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 3, 3, "N"); // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 14, 5, "N"); // Código da alegação
            ValidarConteudoCampo(linha, linhaIndex, 50, 15, "A"); // Detalhes da alegação
        }
        #endregion
    }
}
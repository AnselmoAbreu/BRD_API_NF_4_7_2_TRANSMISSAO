using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
    public class LeituraCNAB240Service
    {
        public List<string> listaDeErros = new List<string>();
        Util util = new Util();
        bool erro = false;

        #region Constantes CNAB 240
        const string REGISTRO_HEADER_ARQUIVO = "0";
        const string REGISTRO_HEADER_LOTE = "1";
        const string REGISTRO_DETALHE = "3";
        const string REGISTRO_TRAILER_LOTE = "5";
        const string REGISTRO_TRAILER_ARQUIVO = "9";
        #endregion

        private int totalLotesProcessados = 0;
        private int totalRegistrosProcessados = 0;
        private decimal valorTotalCalculado = 0m;
        private int totalLotesDeclarado = 0;
        private int totalRegistrosDeclarado = 0;
        private decimal valorTotalDeclarado = 0m;
        private decimal valorTotalLoteAtual = 0m;
        private int totalRegistrosLoteAtual = 0;

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
            ValidarConteudoCampo(linha, linhaIndex, 8, 1, "N", "0");  // Tipo de registro fixo
            ValidarConteudoCampo(linha, linhaIndex, 1, 3, "N");  // Código do banco
            ValidarConteudoCampo(linha, linhaIndex, 144, 8, "N");  // Data de geração
        }

        private void ValidarHeaderLote(string linha, int linhaIndex)
        {
            ValidarConteudoCampo(linha, linhaIndex, 8, 1, "N", "1");
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

        private void ValidarTotais()
        {
            if (totalLotesProcessados != totalLotesDeclarado)
                listaDeErros.Add($"Erro: Quantidade de lotes processados ({totalLotesProcessados}) diferente do declarado no trailer ({totalLotesDeclarado}).");

            if (totalRegistrosProcessados != totalRegistrosDeclarado)
                listaDeErros.Add($"Erro: Quantidade de registros processados ({totalRegistrosProcessados}) diferente do declarado no trailer ({totalRegistrosDeclarado}).");

            if (valorTotalCalculado != valorTotalDeclarado)
                listaDeErros.Add($"Erro: Valor total calculado ({valorTotalCalculado}) diferente do declarado no trailer ({valorTotalDeclarado}).");
        }
    }
}

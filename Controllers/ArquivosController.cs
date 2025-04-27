using BRD_API_NF_4_7_2_TRANSMISSAO.Services.Cnab;
using BRD_API_NF_4_7_2_TRANSMISSAO.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeuProjeto.Controllers
{
    [RoutePrefix("api/Arquivos")]
    public class ArquivosController : ApiController
    {
        private readonly ExternalApiRegrasService externalApiRegrasService;

        public ArquivosController()
        {
            this.externalApiRegrasService = new ExternalApiRegrasService();
        }

        [HttpPost]
        [Route("ENVIAR_ARQUIVO")]
        public async Task<IHttpActionResult> UploadArquivoAsync()
        {
            try
            {
                // Criação de objetos
                LeituraArquivoService leituraArquivoServico = new LeituraArquivoService();
                var util = new BRD_API_NF_4_7_2_TRANSMISSAO.Utils.Helpers.CnabHelper();

                // Obtém o arquivo e o tipoArquivo do request
                HttpPostedFile file = HttpContext.Current.Request.Files["arquivo"];
                string tipoArquivo = HttpContext.Current.Request.Form["tipoArquivo"];

                if (file == null)
                    return BadRequest("Nenhum arquivo foi enviado.");

                if (string.IsNullOrEmpty(tipoArquivo))
                    return BadRequest("O tipo de arquivo não foi especificado.");

                // Lê o arquivo gera um byte array
                byte[] fileBytes;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileBytes = binaryReader.ReadBytes(file.ContentLength);
                }

                // Verifica a extensão do arquivo
                string extensao = Path.GetExtension(file.FileName).ToLower();

                if (!util.VerificarExtensao(extensao))
                    return BadRequest("Arquivo com extensão inválida. Extensões permitidas: .rem, .ret, .txt, .rst ou .dat");

                // Verifica a quantidade de linhas do arquivo
                int totalLinhas = util.ContarLinhasArquivo(fileBytes);

                if (totalLinhas > 10000)
                    return BadRequest("Não é possível validar arquivos acima de 10 mil linhas. Por favor, insira um arquivo menor para uma validação completa.");

                //verifica integridade do arquivo
                var retornoIntegridade = await util.VerificarIntegridadeArquivo(fileBytes, tipoArquivo);
                if(!string.IsNullOrEmpty(retornoIntegridade))
                    return BadRequest(retornoIntegridade);

                //------------------------------------------------------------------------------------------------------------------------
                // Acessa API de Regras e Processa o arquivo
                string apiExternaUrl = "https://localhost:44355/api/Layout/RetornaLayOut/?codigoLayout=" + tipoArquivo;
                string resultadoApiExterna = await externalApiRegrasService.CallExternalApiAsync(apiExternaUrl);

                // Verifica se o resultado da API externa é um JSON válido
                try
                {
                    JsonDocument.Parse(resultadoApiExterna);
                }
                catch (System.Text.Json.JsonException)
                {
                    return BadRequest("O resultado da API de REGRAS não é um JSON válido.");
                }
                List<string> retornoValidacao = await leituraArquivoServico.ProcessarArquivo(fileBytes, tipoArquivo, resultadoApiExterna);
                //------------------------------------------------------------------------------------------------------------------------
                if (retornoValidacao.Count > 0)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { erros = retornoValidacao }));
                
                return Ok("Arquivo processado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro API Backend : " + ex.InnerException);

            }
        }
    }
}

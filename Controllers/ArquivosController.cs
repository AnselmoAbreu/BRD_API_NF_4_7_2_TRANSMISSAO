using BRD_API_NF_4_7_2_TRANSMISSAO.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeuProjeto.Controllers
{
    [RoutePrefix("api/Arquivos")]
    public class ArquivosController : ApiController
    {
        private readonly ExternalApiService externalApiService;

        public ArquivosController()
        {
            this.externalApiService = new ExternalApiService();
        }

        [HttpPost]
        [Route("ENVIAR_ARQUIVO")]
        public async Task<IHttpActionResult> UploadArquivoAsync()
        {
            try
            {

                // Acessa API externa
                string apiExternaUrl = "https://jsonplaceholder.typicode.com/todos/1"; // Exemplo de API pública
                string resultadoApiExterna = await externalApiService.CallExternalApiAsync(apiExternaUrl);

                // Obtém o arquivo e o tipoArquivo do request
                HttpPostedFile file = HttpContext.Current.Request.Files["arquivo"];
                string tipoArquivo = HttpContext.Current.Request.Form["tipoArquivo"];

                if (file == null)
                    return BadRequest("Nenhum arquivo foi enviado.");

                if (string.IsNullOrEmpty(tipoArquivo))
                    return BadRequest("O tipo de arquivo não foi especificado.");

                // Lê o arquivo em um byte array
                byte[] fileBytes;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileBytes = binaryReader.ReadBytes(file.ContentLength);
                }

                // Log do tamanho do arquivo
                //File.AppendAllText(logPath, $"[{DateTime.Now}] FileSize: {fileBytes.Length}\n");

                LeituraArquivoService leituraArquivoServico = new LeituraArquivoService();
                Util util = new Util();

                string extensao = Path.GetExtension(file.FileName).ToLower();

                if (!util.VerificaExtensao(extensao))
                    return BadRequest("Arquivo com extensão inválida. Extensões permitidas: .rem, .ret, .txt, .rst ou .dat");

                int totalLinhas = util.ContarLinhasArquivo(fileBytes);

                if (totalLinhas < 3)
                    return BadRequest("O arquivo deve conter pelo menos 3 linhas.");

                if (totalLinhas > 10000)
                    return BadRequest("Não é possível validar arquivos acima de 10 mil linhas. Por favor, insira um arquivo menor para uma validação completa.");

                // Processa o arquivo
                List<string> retornoValidacao = await leituraArquivoServico.ProcessarArquivo(fileBytes, tipoArquivo);

                if (retornoValidacao.Count > 0)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { erros = retornoValidacao }));
                }

                return Ok(new
                {
                    mensagem = "Arquivo recebido com sucesso!",
                    nomeArquivo = file.FileName,
                    tamanhoArquivo = fileBytes.Length,
                    tipoArquivo = tipoArquivo,
                    totalLinhas = totalLinhas
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

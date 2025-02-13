using BRD_API_NF_4_7_2_TRANSMISSAO.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeuProjeto.Controllers
{
    [RoutePrefix("api/Arquivos")]
    public class ArquivosController : ApiController
    {
        [HttpPost]
        [Route("ENVIAR_ARQUIVO")]
        public async Task<IHttpActionResult> UploadArquivoAsync()
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "upload-log.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(logPath));

            try
            {
                // Log inicial da requisição
                File.AppendAllText(logPath, $"[{DateTime.Now}] Iniciando UploadArquivo\n");

                // Ler o stream de entrada
                var requestStream = HttpContext.Current.Request.InputStream;
                using (var reader = new StreamReader(requestStream))
                {
                    string requestBody = reader.ReadToEnd();
                    File.AppendAllText(logPath, $"[{DateTime.Now}] RequestBody: {requestBody}\n");
                }

                // Obtém o arquivo do request
                HttpPostedFile file = HttpContext.Current.Request.Files["arquivo"];
                string tipoArquivo = HttpContext.Current.Request.Form["tipoArquivo"];

                // Log dos parâmetros recebidos
                File.AppendAllText(logPath, $"[{DateTime.Now}] File: {(file != null ? file.FileName : "null")}, TipoArquivo: {tipoArquivo}\n");

                if (file == null)
                {
                    File.AppendAllText(logPath, $"[{DateTime.Now}] Nenhum arquivo foi enviado\n");
                    return BadRequest("Nenhum arquivo foi enviado.");
                }

                if (string.IsNullOrEmpty(tipoArquivo))
                {
                    File.AppendAllText(logPath, $"[{DateTime.Now}] O tipo de arquivo não foi especificado\n");
                    return BadRequest("O tipo de arquivo não foi especificado.");
                }

                // Lê o arquivo em um byte array
                byte[] fileBytes;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    fileBytes = binaryReader.ReadBytes(file.ContentLength);
                }

                // Log do tamanho do arquivo
                File.AppendAllText(logPath, $"[{DateTime.Now}] FileSize: {fileBytes.Length}\n");

                // Chama o método de validação
                // var validador = new ValidadorArquivo();
                LeituraArquivoServico leituraArquivoServico = new LeituraArquivoServico();
                Util util = new Util();


                string extensao = Path.GetExtension(file.FileName).ToLower();

                if (!util.VerificaExtensao(extensao))
                {
                    return BadRequest("Arquivo com extensão inválida. Extensões permitidas: .rem, .ret, .txt, .rst ou .dat");
                }
                var conteudosExtraidos = new List<string>();

                int totalLinhas = util.ContarLinhasArquivo(fileBytes);

                if (totalLinhas < 3)
                {
                    return BadRequest("O arquivo deve conter pelo menos 3 linhas.");
                }

                if (totalLinhas > 10000)
                {
                    return BadRequest("Não é possível validar arquivos acima de 10 mil linhas. Por favor, insira um arquivo menor para uma validação completa.");
                }

                //--------------------
                // Processa o arquivo
                //--------------------
                List<string> retornoValidacao = await leituraArquivoServico.ProcessarArquivo(fileBytes, tipoArquivo);


                File.AppendAllText(logPath, $"[{DateTime.Now}] Arquivo recebido com sucesso\n");

                // Aqui você pode processar o arquivo na memória conforme necessário
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
                File.AppendAllText(logPath, $"[{DateTime.Now}] Erro: {ex.Message}\n");
                return InternalServerError(ex);
            }
        }
    }
}

using BRD_API_NF_4_7_2_TRANSMISSAO.Servicos;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MeuProjeto.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class ArquivosController : ApiController
    {
        [HttpGet]
        [Route("ENVIAR_ARQUIVO")]
        public async Task<IHttpActionResult> UploadArquivo()
        {
            // Verifica se a requisição contém um arquivo
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("A requisição deve ser multipart/form-data.");
            }

            try
            {
                string uploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");

                // Cria o diretório se não existir
                //if (!Directory.Exists(uploadPath))
                //{
                //    Directory.CreateDirectory(uploadPath);
                //}

                // Processa o conteúdo recebido
                var provider = new MultipartFormDataStreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(provider);

                //// Obtém o arquivo
                var fileData = provider.FileData[0];
                var uploadedFile = new FileInfo(fileData.LocalFileName);

                // Obtém o nome original do arquivo
                string originalFileName = provider.FileData[0].Headers.ContentDisposition.FileName.Trim('"');
                string newFilePath = Path.Combine(uploadPath, originalFileName);

                // Renomeia o arquivo para manter o nome original
                File.Move(uploadedFile.FullName, newFilePath);

                // Obtém o valor do parâmetro 'tipoArquivo'
                string tipoArquivo = provider.FormData["tipoArquivo"];

                return Ok(new
                {
                    message = "Arquivo enviado com sucesso!",
                    fileName = originalFileName,
                    filePath = newFilePath,
                    tipoArquivo = tipoArquivo
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

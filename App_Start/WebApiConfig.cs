using Swashbuckle.Application;
using System.Web.Http;

namespace BRD_API_NF_4_7_2_TRANSMISSAO
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Rota para o método UploadArquivoAsync
            config.Routes.MapHttpRoute(
                name: "UploadArquivoApi",
                routeTemplate: "api/{controller}/ENVIAR_ARQUIVO",
                defaults: new { action = "UploadArquivoAsync" }
            );

            // Ativar Swagger
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "API BACKEND V1.8 - ASP.NET 4.7.2");
                c.IncludeXmlComments(GetXmlCommentsPath()); // Inclui comentários XML dos métodos
            })
            .EnableSwaggerUi();

            // Redirecionar para o Swagger por padrão
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index"));

        }
        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}bin\\BRD_API_NF_4_7_2_TRANSMISSAO.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}

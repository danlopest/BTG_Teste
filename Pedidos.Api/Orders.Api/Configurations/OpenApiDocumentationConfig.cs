using Orders.Borders.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Orders.Api.Configurations;
public static class OpenApiDocumentationConfig
{
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, ApplicationConfig config)
    {
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(x => x.FullName);
            c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
            c.SupportNonNullableReferenceTypes();
            c.OperationFilter<AddResponseHeadersFilter>();
            c.SwaggerDoc(
                "v1",
                new()
                {
                    Title = "Orders",
                    Version = "v1",
                    Description = "API de Gerenciamento de Pedidos",
                }
            );

            // Encontra arquivos de documentação XML e os utiliza na geração de documentação da API.
            var xmlFiles = Directory
                .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                .ToList();
            xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
        });

        return services;
    }
}
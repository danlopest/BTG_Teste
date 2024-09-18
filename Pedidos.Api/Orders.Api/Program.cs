using Orders.Api.Configurations;
using Orders.Api.Extensions;
using Orders.Api.Middlewares;
using Orders.Api.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration.LoadConfiguration();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }).ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.CreateResponse);

builder.Services.AddSingleton(appConfig);
builder.Services.AddMemoryCache();
builder.Services.AddOpenApiDocumentation(appConfig);
builder.Services.AddHttpContextAccessor();
builder.Services.AddUseCases();
builder.Services.AddRepositories(appConfig);
builder.Services.AddScoped<IActionResultConverter, ActionResultConverter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger(c => c.RouteTemplate = "api-docs/{documentName}/open-api.json");
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api-docs/v1/open-api.json", " Orders v1");
    c.RoutePrefix = "api-docs";
    c.OAuthScopeSeparator(" ");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

await app.RunAsync();
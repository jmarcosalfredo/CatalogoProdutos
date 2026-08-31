using CatalogoProdutos.Api.Configs;
using CatalogoProdutos.Api.Context;
using CatalogoProdutos.Api.Repositories;
using CatalogoProdutos.Api.Repositories.Implementations;
using CatalogoProdutos.Api.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddCorsConfig();

builder.Services.AddIdentityConfig();
builder.Services.AddAuthorizationConfig();
builder.Services.AddAuthenticationConfig(builder.Configuration);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<JwtBearerConfig>();
});

builder.Services.AddRateLimiterConfig();

builder.Services.AddDatabaseConfigurarion(builder.Configuration);
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerConfig();
    //app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseCors(CorsConfig.OrigensComAcessoPermitido);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

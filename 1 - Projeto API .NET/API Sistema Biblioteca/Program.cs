
#region Builder
using Dados.Autores;
using Dados.Livros;
using Dados.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ORM;
using Serilog;
using Servicos.Autores;
using Servicos.Livros;
using Servicos.Usuarios;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gerenciamento de Biblioteca", Version = "v1" });

    // Configuração para suportar autenticação Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
#endregion



#region Injeção de Dependência
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IUsuariosDados, UsuariosDados>();
builder.Services.AddScoped<ILivrosService, LivrosService>();
builder.Services.AddScoped<ILivrosDados, LivrosDados>();
builder.Services.AddScoped<IAutoresService, AutoresService>();
builder.Services.AddScoped<IAutoresDados, AutoresDados>();

#endregion

#region Autenticação
string jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("A chave JWT não está configurada corretamente.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
})
.AddCookie();

builder.Services.AddAuthorization();
#endregion

#region Logs
// Configuração do Serilog para logging em arquivo

string logFilePath = builder.Configuration["Logging:File:Path"] ?? throw new InvalidOperationException("O caminho do arquivo de log não está configurado corretamente.");

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, logFilePath))
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Limpar provedores padrão, como ConsoleLoggerProvider
    loggingBuilder.AddSerilog(); // Adicionar o logger do Serilog
});
#endregion


#region App
var app = builder.Build();
#endregion 

#region Banco de Dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao configurar o banco de dados: {ex.Message}");
    }
}
#endregion

#region App Settings
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
#endregion
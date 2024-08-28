using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente do arquivo .env
Env.Load();

// Recuperar variáveis de ambiente
string? dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
string? dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE");
string? dbUser = Environment.GetEnvironmentVariable("DB_USER");
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Verifique se todas as variáveis de ambiente necessárias estão presentes
if (!string.IsNullOrEmpty(dbServer) && !string.IsNullOrEmpty(dbDatabase) &&
    !string.IsNullOrEmpty(dbUser) && !string.IsNullOrEmpty(dbPassword))
{
    // Construir a string de conexão usando variáveis de ambiente
    string connectionString = $"Server={dbServer};Database={dbDatabase};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=true";

    // Configurar o DbContext usando a string de conexão construída
    builder.Services.AddDbContext<CollaboratorManagementContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Usar a string de conexão do appsettings.json
    builder.Services.AddDbContext<CollaboratorManagementContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Configurar a política de CORS para permitir qualquer origem (Apenas para desenvolvimento)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Registrar serviços no contêiner de dependência
builder.Services.AddScoped<ICollaboratorService, CollaboratorService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

// Configurar Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar a política de CORS
app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

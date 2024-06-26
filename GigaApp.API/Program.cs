using GigaApp.Storage;
using Microsoft.EntityFrameworkCore;
using GigaApp.Domain.DependencyInjection;
using GigaApp.Storage.DependencyInjection;
using AutoMapper;
using System.Reflection;
using GigaApp.API.Authentication;
using GigaApp.Domain.Authentication;
using GigaApp.API.Middlewares;
using GigaApp.API.Monitoring;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiLogging(builder.Configuration, builder.Environment);
builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication").Bind);
builder.Services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("MsSql");
builder.Services.AddMetrics(builder.Configuration);
builder.Services
    .AddForumStorage(connectionString)
    .AddForumDomain();

builder.Services
    .AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));


var app = builder.Build();

app.Services
    .GetRequiredService<IMapper>()
    .ConfigurationProvider
    .AssertConfigurationIsValid();




    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapPrometheusScrapingEndpoint();
app
    .UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<AuthenticationMiddleware>();

app.Services.GetRequiredService<ForumDbContext>().Database.Migrate();
app.Run();

public partial class Program { }
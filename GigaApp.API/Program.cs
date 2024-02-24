using FluentValidation;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Storage;
using GigaApp.Storage.Storages;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using GigaApp.Domain.DependencyInjection;
using GigaApp.Storage.DependencyInjection;
using GigaApp.API.Mapping;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(b=>b.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithProperty("Application", "GigaApp.API")
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.Logger(lc=> lc.Filter.ByExcluding(Matching.FromSource("Microsoft")).WriteTo.OpenSearch(
        "http://localhost:9200",
        "forum-logs-{0:yyyy.MM.dd}"
        ))
    .WriteTo.Logger(lc=>lc.Filter.ByExcluding(Matching.FromSource("Microsoft")).WriteTo.Console())
    .CreateLogger()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("MsSql");

builder.Services
    .AddForumStorage(connectionString)
    .AddForumDomain();

builder.Services
    .AddAutoMapper(config => config.AddProfile<ApiProfile>());


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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Services.GetRequiredService<ForumDbContext>().Database.Migrate();
app.Run();

public partial class Program { }
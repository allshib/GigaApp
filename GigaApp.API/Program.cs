using GigaApp.Domain.Authorization;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Storage;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IGetForumsUseCase, GetForumUseCase>();
builder.Services.AddScoped<ICreateTopicUseCase, CreateTopicUseCase>();
builder.Services.AddScoped<ICreateTopicStorage>();
builder.Services.AddScoped<IIntentionResolver, TopicIntetntionResolver>();
builder.Services.AddScoped<IIntentionManager, IntentionManager>();

builder.Services.AddScoped<ICreateTopicStorage>();

var connectionString = builder.Configuration.GetConnectionString("MsSql");

builder.Services.AddDbContext<ForumDbContext>(options => 
options.UseSqlServer(connectionString, b=> b.MigrationsAssembly("GigaApp.API")), ServiceLifetime.Singleton);


var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.GetRequiredService<ForumDbContext>().Database.Migrate();
app.Run();

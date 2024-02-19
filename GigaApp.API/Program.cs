using GigaApp.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ForumDbContext>(options => 
options.UseSqlServer("Integrated Security=SSPI;Pooling=false;Data Source=.;Initial Catalog=GigaApp;TrustServerCertificate=true;", b=> b.MigrationsAssembly("GigaApp.API")), ServiceLifetime.Singleton);

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.GetRequiredService<ForumDbContext>().Database.Migrate();
app.Run();

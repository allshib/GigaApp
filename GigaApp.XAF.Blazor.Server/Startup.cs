using System.Reflection;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.Base;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using GigaApp.XAF.Blazor.Server.Services;
using GigaApp.Storage.DependencyInjection;
using GigaApp.XAF.Blazor.Server.Authentication;
using GigaApp.XAF.Blazor.Server.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GigaApp.XAF.Module.ServiceClasses;
using GigaApp.XAF.Module.Mapping;

namespace GigaApp.XAF.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddControllers();
        services
            .AddForumStorage(Configuration.GetConnectionString("MsSql"))
            .AddForumDomain()
            .AddScoped<NonPersistentStorageBase, GigaAppStorage>()
            .AddAutoMapper(
                config => 
                config.AddProfile<XafObjectsProfile>());

        services.Configure<AuthenticationConfiguration>(Configuration.GetSection("Authentication").Bind);
        services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();


        services.AddXaf(Configuration, builder => {
            builder.UseApplication<XAFBlazorApplication>();
            builder.Modules
                .AddConditionalAppearance()
                .AddValidation(options => {
                    options.AllowValidationDetailsAccess = false;
                })
                .Add<GigaApp.XAF.Module.XAFModule>()
            	.Add<XAFBlazorModule>();
            builder.ObjectSpaceProviders
                    .AddNonPersistent();
            builder.Security.AddAuthenticationProvider<>()
            })

        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseXaf();
        app
            //.UseMiddleware<ErrorHandlingMiddleware>()
            .UseMiddleware<AuthenticationMiddleware>();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}

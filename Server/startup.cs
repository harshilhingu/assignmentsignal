using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponceComression;
using Microsoft.Extension.Configuration;
using Microsoft.Extension.DependencyInjection;
using Microsoft.Extension.Hosting;
using System.Linq;
using BlazorWebAssemblySignalRApp.Server.Hubs;

namespace BlazorWebAssemblySignalRApp.Server{
    public class startup{

        public startup(IConfiguration Configuration){
            Configuration = configuration;
        }
        public IConfiguration configuration {get;}
        public void configuration(IserviceCollection services)
        {
            services.AddSignalR();
            services.AddControllerWithViews();
            services.AddRazorPages();
            services.AddResponseCompression(opts => {
                opts.MimeTypes = ResponceComressionDefaults.MimeTypes.Concat(
                    new[]{"application/octet-stream"});4
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    app.UseResponseCompression();

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
        endpoints.MapControllers();
        endpoints.MapHub<ChatHub>("/chathub");
        endpoints.MapFallbackToFile("index.html");
    });
}
    }
}

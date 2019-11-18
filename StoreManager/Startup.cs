using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StoreManager.Data;
using StoreManager.Data.Validation;
using Microsoft.Extensions.FileProviders;

namespace StoreManager {
    public class Startup {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            // Normally you'd get this from a secure vault or auth against a server
            var pass = Encoding.ASCII.GetBytes("ThisIsNotASecurePassword");
            var salt = Encoding.ASCII.GetBytes("??V?7Rv?&6?P?)?");

            EncryptionHandler.AESEncryptFile(Environment.CurrentDirectory + @"\appsecrets.txt", pass, salt);

            var encrypted = File.ReadAllBytes(Environment.CurrentDirectory + @"\appsecrets.txt.encrypted");
            var connection = Encoding.UTF8.GetString(EncryptionHandler.AesDecryptFile(encrypted, pass, salt));

            services.AddMvcCore().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddMvcCore().AddRazorViewEngine();

            services.AddMvcCore().AddJsonFormatters();

            services.AddDbContextPool<StoreManagerContext>(options =>
                options.UseSqlServer(connection)
            
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
                    RequestPath = "/Scripts"
            });

            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Companies}/{action=Index}/{id?}");
            });
        }
    }
}

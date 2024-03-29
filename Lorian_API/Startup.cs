using LOGIC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lorian_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lorian_API", Version = "v1" });
            });

            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(3);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/api/Auth/AccessDenied";
                    options.LoginPath = "/api/Auth/LoginRequired";
                    options.Cookie.Name = "access_token";
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            #endregion

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region Custom services
            services.AddScoped<CaracteristicaService>();
            services.AddScoped<ComponenteService>();
            services.AddScoped<ConversacionService>();
            services.AddScoped<DireccionService>();
            services.AddScoped<EstadoComponenteService>();
            services.AddScoped<EstadoEnvioService>();
            services.AddScoped<EstadoMensajeService>();
            services.AddScoped<EstadoUsuarioService>();
            services.AddScoped<MarcaService>();
            services.AddScoped<PromocionService>();
            services.AddScoped<RolService>();
            services.AddScoped<TarjetaService>();
            services.AddScoped<TelefonoService>();
            services.AddScoped<TipoComponenteService>();
            services.AddScoped<TipoTarjetaService>();
            services.AddScoped<TipoUsuarioMensajeService>();
            services.AddScoped<UsuarioService>();
            #endregion

            #region CORS
            services.AddCors();
            string corsUrl = Configuration["CORS:site"];

            string[] corsUrls = new string[1];
            corsUrls[0] = corsUrl;

            services.AddCors(options =>
            {
                options.AddPolicy("ltangular",
                    builder =>
                    {
                        builder.WithOrigins(corsUrls)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            #endregion

            #region Files configurations
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lorian_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ltangular");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

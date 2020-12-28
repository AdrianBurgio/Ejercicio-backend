using DataIQEjercicio.Configuracion;
using DataIQEjercicio.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace DataIQEjercicio
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
            //servicio para que pueda conectar el frontend React
            services.AddCors();
            services.AddControllers();

            // 2 - Vincular la clase con la interface para la conexecion con Mongo
            services.Configure<UsuariosDatabaseStore>(
                            Configuration.GetSection(nameof(UsuariosDatabaseStore)));

            services.AddSingleton<IUsuariosDatabaseStore>(sp =>
                            sp.GetRequiredService<IOptions<UsuariosDatabaseStore>>().Value);

            services.AddSingleton<UsuarioDB>();

            // 3 - Instalar nuget de mongoDB , yo instale Official .NET driver for MongoDB. 2.11.5

            // 9 - documentacion de web microsoft
            // Register the Swagger generator, defining 1 or more Swagger documents
            
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    { Title = "Api Usuarios Ejercicio DataIQ", Version = "v1" });

                    // para que salgan los mensajes, antes agregar en proyecto/editar las lineas de PropertyGroup
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);

                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //puerta y permiso para que entre el front end 
            app.UseCors(options=>
            {
                options.WithOrigins("*"); // aca iria localhost:3000/ pero no me funciona, por alguna cuestion de permisos
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            //fin puerta

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger as a JSON endpoint.
            // fuente: docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ejercicio Api V1");
            });
        }
    }
}

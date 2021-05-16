using Domain.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CustomPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(
                            Configuration.GetSection("Origins")["WebSystem"]
                        )
                        .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Agrotools API",
                    Description = "API do Agrotools Forms.",
                    TermsOfService = new Uri("https://github.com/DanielMendesdoAmaral"),
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Amaral",
                        Email = "daniel.amaral720@gmail.com",
                        Url = new Uri("https://github.com/DanielMendesdoAmaral"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "LICX",
                        Url = new Uri("https://github.com/DanielMendesdoAmaral"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            var assembly = AppDomain.CurrentDomain.Load("Domain");
            services.AddMediatR(assembly);

            services.Configure<DataContext>(
                Configuration.GetSection(nameof(DataContext)));
            services.AddSingleton<IDataContext>(sp =>
                sp.GetRequiredService<IOptions<DataContext>>().Value);

            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agrotools Forms API V1");
            });

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

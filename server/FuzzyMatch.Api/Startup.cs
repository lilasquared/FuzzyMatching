using System;
using System.Linq;
using FuzzyMatch.Api.Abstracts;
using FuzzyMatch.Api.Handlers.Generic;
using FuzzyMatch.Api.Providers;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickApi;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FuzzyMatch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info {Title = "API", Version = "v1"});
                config.TagActionsBy(api =>
                {
                    api.TryGetMethodInfo(out var info);

                    return info.DeclaringType.GetGenericArguments().Any()
                        ? info.DeclaringType.GetGenericArguments().First().Name
                        : info.DeclaringType.Name;
                });
            });

            var types = typeof(Dataset)
                .Assembly
                .GetQuickApiTypes()
                .ToArray();

            services
                .AddMvc()
                .AddQuickApi(typeof(BaseController<>), types)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var container = new Container(new DefaultRegistry());
            container.Configure(config =>
            {
                config.AddGenericHandlers(types, new []
                {
                    typeof(GetAllHandler<>),
                    typeof(GetOneHandler<>),
                    typeof(CreateHandler<>),
                    typeof(DeleteHandler<>)
                });

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSpa(builder => builder.UseProxyToSpaDevelopmentServer("http://localhost:3000"));
                return;
            }

            app.UseStaticFiles();
            app.UseSpa(x => { });
        }
    }
}

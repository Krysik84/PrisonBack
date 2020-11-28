using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using PrisonBack.Mapping;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Services;

namespace PrisonBack
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Backend Prison",
                        Description = "Backend prison uruchomiony w Swagger",
                        Version = "v1"
                    });
            });
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("PrisonDbContext"));
            });
            //services.BuildServiceProvider().GetService<AppDbContext>().Database.Migrate();

            services.AddScoped<ICellService, CellService>();
            services.AddScoped<ICellRepository, CellRepository>();

            services.AddScoped<IPunishmentService, PunishmentService>();
            services.AddScoped<IPunishmentRepository, PunishmentRepository>();

            services.AddScoped<IPrisonerService, PrisonerService>();
            services.AddScoped<IPrisonerRepository, PrisonerRepository>();

            services.AddScoped<IPassService, PassService>();
            services.AddScoped<IPassRepository, PassRepository>();

            services.AddAutoMapper(typeof(ModelToResourceProfile));
            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Prison");
            });
        }
    }
}

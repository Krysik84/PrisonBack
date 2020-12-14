using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using PrisonBack.Mapping;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Services;

using PrisonBack.Auth;
using PrisonBack.Mailing;
using PrisonBack.Mailing.Service;
using Microsoft.Extensions.Options;

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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            services.AddScoped<ICellService, CellService>();
            services.AddScoped<ICellRepository, CellRepository>();

            services.AddScoped<IPunishmentService, PunishmentService>();
            services.AddScoped<IPunishmentRepository, PunishmentRepository>();

            services.AddScoped<IPrisonerService, PrisonerService>();
            services.AddScoped<IPrisonerRepository, PrisonerRepository>();

            services.AddScoped<IPassService, PassService>();
            services.AddScoped<IPassRepository, PassRepository>();

            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();

            services.AddScoped<IInviteCodeService, InviteCodeService>();
            services.AddScoped<IInviteCodeRepository, InviteCodeRepository>();

            services.AddScoped<IIsolationService, IsolationService>();
            services.AddScoped<IIsolationRepository, IsolationRepository>();

            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IAddUserRepository, AddUserRepository>();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<RegisterMail>();
            services.AddTransient<IHostedService, MailNotificationService>();
            services.AddTransient<NotificationMail>();

            

            services.AddAutoMapper(typeof(ModelToResourceProfile));
            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

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

            app.UseAuthentication();
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

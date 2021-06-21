using constructionCompanyAPI.Authorization;
using constructionCompanyAPI.Entities;
using constructionCompanyAPI.Middleware;
using constructionCompanyAPI.Models;
using constructionCompanyAPI.Models.Validators;
using constructionCompanyAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace constructionCompanyAPI
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
            var authenticationSettings = new AuthenticationSettings();

            services.AddSingleton(authenticationSettings);

            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddAuthentication(option => 
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg => 
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),

                };

            });
            // w³asna polityka autoryzacji
            services.AddAuthorization(options =>
            {
            options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
            options.AddPolicy("Atleast18", builder => builder.AddRequirements(new MinimumAgeRequirement(18)));
            options.AddPolicy("AtLeast2Companies", builder => builder.AddRequirements(new MinimumNumberOfCompanies(2)));
            });

            services.AddScoped<IAuthorizationHandler, MinimumNumberOfCompaniesHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<ConstructionCompanyDbContext>();
            services.AddScoped<ConstructionCompanySeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IConstructionCompanyService, ConstructionCompanyService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddSwaggerGen();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IValidator<ConstructionCompanyQuery>, ConstructionCompanyQueryValidator>();
            services.AddCors(option =>
            {
                option.AddPolicy("FrontEndClient", builder => 
                     builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(Configuration["AllowedOrigins"])
                            
                 );
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConstructionCompanySeeder seeder)
        {
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseCors("FrontEndClient");

            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConstructionCompany API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

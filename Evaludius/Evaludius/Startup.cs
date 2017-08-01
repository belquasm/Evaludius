// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using DAL;
using DAL.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using AutoMapper;
using Newtonsoft.Json;
using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Evaludius.ViewModels;
using Evaludius.Helpers;
using Evaludius.Policies;
using AppPermissions = DAL.Core.ApplicationPermissions;
using AspNet.Security.OpenIdConnect.Primitives;
using OpenIddict.Core;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Evaludius
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment _hostingEnvironment;


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnvironment = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EmailTemplates.Initialize(_hostingEnvironment);

            EmailSender.Configuration = new SmtpConfig
            {
                Host = Configuration["SmtpConfig:Host"],
                Port = int.Parse(Configuration["SmtpConfig:Port"]),
                UseSSL = bool.Parse(Configuration["SmtpConfig:UseSSL"]),
                Name = Configuration["SmtpConfig:Name"],
                Username = Configuration["SmtpConfig:Username"],
                EmailAddress = Configuration["SmtpConfig:EmailAddress"],
                Password = Configuration["SmtpConfig:Password"]
            };


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("Evaludius"));
                options.UseOpenIddict();
            });

            // add identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options and password complexity here
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                //    //options.Password.RequireDigit = true;
                //    //options.Password.RequiredLength = 8;
                //    //options.Password.RequireNonAlphanumeric = false;
                //    //options.Password.RequireUppercase = true;
                //    //options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;

                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });



            // Register the OpenIddict services.
            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
                options.AddMvcBinders();
                options.EnableTokenEndpoint("/connect/token");
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                options.DisableHttpsRequirement();
                // options.UseJsonWebTokens(); //Use JWT if preferred
                options.AddSigningKey(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["STSKey"])));
            });


            // Enable cors if required
            //services.AddCors();

            // Add framework services.
            services.AddMvc();

            //Todo: ***Using DataAnnotations for validation until Swashbuckle supports FluentValidation***
            //services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //.AddJsonOptions(opts =>
            //{
            //    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //});


            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("BearerAuth", new Swashbuckle.Swagger.Model.ApiKeyScheme
                {
                    Name = "Authorization",
                    Description = "Login with your bearer authentication token. e.g. Bearer <auth-token>",
                    In = "header",
                    Type = "apiKey"
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.ViewUserByUserIdPolicy, policy => policy.Requirements.Add(new ViewUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ViewUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewUsers));

				options.AddPolicy(AuthPolicies.ViewPlayersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewPlayers));

				options.AddPolicy(AuthPolicies.ManageUserByUserIdPolicy, policy => policy.Requirements.Add(new ManageUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ManageUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageUsers));

                options.AddPolicy(AuthPolicies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleByNameRequirement()));

                options.AddPolicy(AuthPolicies.ViewRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewRoles));

                options.AddPolicy(AuthPolicies.AssignRolesPolicy, policy => policy.Requirements.Add(new AssignRolesRequirement()));

                options.AddPolicy(AuthPolicies.ManageRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRoles));
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });


            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountManager, AccountManager>();

            // Auth Policies
            services.AddSingleton<IAuthorizationHandler, ViewUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleByNameHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesHandler>();

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Warning);
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            Utilities.ConfigureLogger(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            // Configure Cors if enabled
            //app.UseCors(builder => builder
            //    .AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod());


            app.UseStaticFiles();
            app.UseOAuthValidation();
            app.UseOpenIddict();



			//var defaultDateCulture = "ar-MA";
			//var ci = new CultureInfo(defaultDateCulture);
			//ci.NumberFormat.NumberDecimalSeparator = ".";
			//ci.NumberFormat.CurrencyDecimalSeparator = ".";

			//// Configure the Localization middleware
			//app.UseRequestLocalization(new RequestLocalizationOptions
			//{
			//	DefaultRequestCulture = new RequestCulture(ci),
			//	SupportedCultures = new List<CultureInfo>
			//	{
			//		ci,
			//	},
			//	SupportedUICultures = new List<CultureInfo>
			//	{
			//		ci,
			//	}
			//});



			app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.ApplicationJson;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        string errorMsg = JsonConvert.SerializeObject(new { error = error.Error.Message });
                        await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);
                    }
                });
            });


            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });


            try
            {
                databaseInitializer.SeedAsync().Wait();
            }
            catch (Exception ex)
            {
                Utilities.CreateLogger<Startup>().LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);
                throw;
            }
        }
    }
}
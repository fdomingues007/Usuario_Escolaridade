using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Security;
using Domain.Interface;
using Infra.DB;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace API
{
  public class Startup
  {
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    private readonly IHostingEnvironment _hostingEnvironment;

    public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    {
      Configuration = configuration;
      _hostingEnvironment = hostingEnvironment;
    }

    public IConfiguration Configuration { get; }

    private readonly string AUDIENCE = "c1f51f42";
    private readonly string ISSUER = "c6bbbb645024";



    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDirectoryBrowser();

      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
      if (!Directory.Exists(fullPath))
      {
        Directory.CreateDirectory(fullPath);
      }

      services.Configure<RequestLocalizationOptions>(options =>
      {
        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
        options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR"), new CultureInfo("en-US") };
      });

      services.Configure<IISOptions>(o =>
      {
        o.ForwardClientCertificate = false;
      });

      services.AddSingleton<IFileProvider>(
               new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload")));

      // Comprimir todas as informações da API
      services.AddResponseCompression();

      // Injeção de dependências
      services.AddScoped<EfContext, EfContext>(); // Entity Framework
      services.AddTransient<IUsuarioRepository, UsuarioRepository>();
      services.AddTransient<IEscolaridadeRepository, EscolaridadeRepository>();

      // Configurações do Token
      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      services.AddDbContext<EfContext>(options =>
          options.UseSqlServer(Configuration.GetSection("ConnectionStrings:EFContext").Value));

      var tokenConfiguration = new TokenJwtOptions
      {
        Audience = AUDIENCE,
        Issuer = ISSUER,
        Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString(CultureInfo.InvariantCulture))
      };

      services.AddSingleton(tokenConfiguration);

      services.AddAuthentication(authoOptions =>
      {
        authoOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authoOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
        paramsValidation.ValidAudience = tokenConfiguration.Audience;
        paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

              // Valida a assinatura de um Token recebido
              paramsValidation.ValidateIssuerSigningKey = true;

              // Verifica se um Token recebido ainda é válido
              paramsValidation.ValidateLifetime = true;

              // Tempo de tolerância para a expiração de um token ( Ultilizando 
              // caso haja problemas de sincronismo de horário entre diferentes 
              // computadores envolvidos no processo de comunicação )
              paramsValidation.ClockSkew = TimeSpan.Zero;

      });

      // Ativa o uso do Token como forma de autorizar o acesso
      // a recursos deste projeto
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                  .RequireAuthenticatedUser().Build());

              //auth.AddPolicy("RequireAdministratorRole",
              //    policy => policy.RequireRole("Administrator"));

            });

      // Cors
      // Add service and create Policy with options
      // Add AllowAll policy just like in single controller example.
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAll",
            builder =>
            {
          builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        });
      });

      // MVC
      services.AddMvcCore().AddJsonFormatters(j => j.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

      services.AddMvc(config =>
      {
        var policy = new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                  .RequireAuthenticatedUser().Build();
        config.Filters.Add(new AuthorizeFilter(policy));
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.Configure<MvcOptions>(options =>
      {
        options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
      });

      //services.AddSwaggerGen(x =>
      //{
      //  x.SwaggerDoc("v1", new Info { Title = "Restaurate", Version = "v1.0.0" });
      //});

      services.AddOpenApiDocument(config =>
      {
        config.DocumentName = "OpenApi3";
        config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        config.AddSecurity("Bearer", Enumerable.Empty<string>(),
            new OpenApiSecurityScheme()
        {
          Description = "Copy this into the value field: Bearer {token}",
          Type = OpenApiSecuritySchemeType.Http,
          Scheme = "bearer",
          BearerFormat = "jwt",
          In = OpenApiSecurityApiKeyLocation.Header
        });

        config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));

        config.PostProcess = document => { document.Info.Title = GetType().Namespace; };
      });

      //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors("AllowAll");
      app.UseStaticFiles(); // For the wwwroot folder


      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
      if (!Directory.Exists(fullPath))
      {
        Directory.CreateDirectory(fullPath);
      }

      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload")),
        RequestPath = "/MyUploads"
      });

      app.UseDirectoryBrowser(new DirectoryBrowserOptions
      {
        FileProvider = new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload")),
        RequestPath = "/MyUploads"
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      // MVC
      app.UseMvc();

      // Documentação da API
      // http://localhost:52516/swagger/index.html
      //app.UseSwagger();
      //app.UseSwaggerUI(x =>
      //{
      //  x.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Padrão - v1");
      //});

      app.UseOpenApi();
      app.UseSwaggerUi3();
      app.UseReDoc();

      // Comprimir todas as informações da API
      app.UseResponseCompression();
    }
  }

}

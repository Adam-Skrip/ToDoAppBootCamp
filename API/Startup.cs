using System.Text;
using API.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200", "https://localhost:5001","http://localhost:5000" )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Visma.TaskHackaton",
                Description = "This is a simple Task API used for education purposes only.",
                License = new OpenApiLicense
                {
                    Name = "AdamLaci s.r.o",
                },
                Contact = new OpenApiContact
                {
                    Email = "adam.skrip@visma.com"
                },
                Version = "1.0.0"
            });
            c.EnableAnnotations();
            c.AddServer(new OpenApiServer
            {
                Description = "Development localhost server - Kestrel",
                Url = "https://localhost:5001"
            });
            var securitySchema = new OpenApiSecurityScheme()
            {
                Description = "JWT Auth Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    securitySchema,
                    new[] { "Bearer" }
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);
            c.AddSecurityRequirement(securityRequirement);
        });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Token:Key").Value)),
                    ValidIssuer = Configuration.GetSection("Token:Issuer").Value,
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddMemoryCache();
        services.AddApplicationServices(Configuration, Environment);
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Visma.TaskHackaton"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        // use custom middlewares here
        app.UseErrorHandlingMiddleware();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
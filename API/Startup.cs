using API.DependencyInjection;
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

        app.UseAuthorization();

        // use custom middlewares here
        app.UseErrorHandlingMiddleware();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
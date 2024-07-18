using Serilog;

namespace WebApiTemplate.WebApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Log.Debug("ConfigureServices => Setting AddControllers");
        services.AddControllers();

        Log.Debug("ConfigureServices => Setting AddHealthChecks");
        services.AddHealthChecks();

        Log.Debug("ConfigureServices => Setting AddEndpointsApiExplorer");
        services.AddEndpointsApiExplorer();

        Log.Debug("ConfigureServices => Setting AddSwaggerGen");
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        {
            if (env.IsDevelopment())
            {
                Log.Debug("Setting UseDeveloperExceptionPage");
                app.UseDeveloperExceptionPage();

                Log.Debug("Setting UseSwagger");
                app.UseSwagger();

                Log.Debug("Setting UseSwaggerUI");
                app.UseSwaggerUI();
            }

            Log.Debug("Setting cors => allow *");
            app.UseCors(builder =>
            {
                builder.SetIsOriginAllowed(_ => true);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            Log.Debug("Setting UseSerilogRequestLogging");
            app.UseSerilogRequestLogging();

            Log.Debug("Setting UseHttpsRedirection");
            app.UseHttpsRedirection();

            Log.Debug("Setting UseRouting");
            app.UseRouting();

            Log.Debug("Setting UseEndpoints");
            app.UseEndpoints(endpoints =>
            {
                Log.Debug("Setting endpoints => MapControllers");
                endpoints.MapControllers();

                Log.Debug("Setting endpoints => MapHealthChecks");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
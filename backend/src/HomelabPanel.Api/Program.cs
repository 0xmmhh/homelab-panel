using HomelabPanel.Api.Integrations;
using HomelabPanel.Api.Integrations.Gitea;
using HomelabPanel.Api.Integrations.PiHole;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog((services, lc) =>
        lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services));

    builder.Services.AddOptions<PiHoleOptions>()
        .BindConfiguration(PiHoleOptions.SectionName)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    builder.Services.AddOptions<GiteaOptions>()
        .BindConfiguration(GiteaOptions.SectionName)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    builder.Services.AddOptions<MetricsOptions>()
        .BindConfiguration(MetricsOptions.SectionName)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

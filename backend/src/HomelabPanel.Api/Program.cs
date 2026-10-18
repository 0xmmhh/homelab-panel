using HomelabPanel.Api.Integrations;
using HomelabPanel.Api.Integrations.Gitea;
using HomelabPanel.Api.Integrations.PiHole;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHealthChecks();

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
        app.MapScalarApiReference();
    }

    // Plain HTTP on purpose: the app is only reachable on the LAN and over Tailscale (encrypted).
    // No HTTPS redirection, which would also break Gitea webhook deliveries.
    // No authorization middleware until authentication exists.
    app.MapControllers();
    app.MapHealthChecks("/health");

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

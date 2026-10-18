using HomelabPanel.Api.Integrations;
using HomelabPanel.Api.Integrations.Gitea;
using HomelabPanel.Api.Integrations.PiHole;

var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using System.ComponentModel.DataAnnotations;

namespace HomelabPanel.Api.Integrations;

public class MetricsOptions
{
    public const string SectionName = "Metrics";
    [Range(1, 365)] public int RetentionDays { get; set; } = 30;
}

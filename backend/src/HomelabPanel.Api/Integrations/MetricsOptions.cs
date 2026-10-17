using System.ComponentModel.DataAnnotations;

namespace HomelabPanel.Api.Integrations;

public class MetricsOptions
{
    [Range(1, 365)] public int RetentionDays { get; set; } = 30;
}

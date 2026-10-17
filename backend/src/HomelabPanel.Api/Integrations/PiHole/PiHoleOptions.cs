using System.ComponentModel.DataAnnotations;

namespace HomelabPanel.Api.Integrations.PiHole;

public class PiHoleOptions
{
    [Url] [Required] public string BaseUrl { get; set; } = string.Empty;

    [Required] public string AppPassword { get; set; } = string.Empty;

    [Range(typeof(TimeSpan), "00:00:10", "01:00:00")]
    public TimeSpan PollingInterval { get; set; } = TimeSpan.FromMinutes(1);
}

using System.ComponentModel.DataAnnotations;

namespace HomelabPanel.Api.Integrations.Gitea;

public class GiteaOptions
{
    [Required] public string WebhookSecret { get; set; } = string.Empty;
    [Required] public string TrackedRepository { get; set; } = string.Empty;
    public string TrackedBranch { get; set; } = "main";
}

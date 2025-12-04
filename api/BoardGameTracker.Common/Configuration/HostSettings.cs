namespace BoardGameTracker.Common.Configuration;

public class HostSettings
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public string ClientUrl { get; set; } = string.Empty;
}
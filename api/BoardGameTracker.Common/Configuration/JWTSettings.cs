namespace BoardGameTracker.Common.Configuration;

public class JWTSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Secret { get; set; }
    public int ExpiryInMinutes { get; set; }
}
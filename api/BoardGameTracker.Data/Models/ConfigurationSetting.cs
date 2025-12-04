using BoardGameTracker.Data.Models.Interfaces;

namespace BoardGameTracker.Data.Models;

public partial class ConfigurationSetting : IAudit
{
    public long ConfigurationSettingId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
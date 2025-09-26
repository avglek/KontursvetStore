namespace KontursvetStore.Core.Models;

public class BaseModel
{
    public BaseModel(Guid id,bool enabled, DateTime lastUpdate)
    {
        Id = id;
        LastUpdated = lastUpdate;
        Enabled = enabled;
    }
    
    public Guid Id { get; }
    public DateTime LastUpdated { get; }
    public bool Enabled { get; }
}
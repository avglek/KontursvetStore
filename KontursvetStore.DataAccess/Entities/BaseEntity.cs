namespace KontursvetStore.DataAccess.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Enabled { get; set; }
}
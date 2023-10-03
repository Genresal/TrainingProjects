namespace BlazorServerTest.Core.Data.Entities.Core;

public class Entity : IEntity
{
    public long Id { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime? Updated { get; set; }
}

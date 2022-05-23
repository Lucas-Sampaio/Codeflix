namespace Codeflix.Catalog.Domain.SeedWork;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; }
    public DateTime CreatedAt { get; }
}
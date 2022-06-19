namespace Codeflix.Catalog.Domain.SeedWork;

public interface IGenericRepository<TAggregate> : IRepository where TAggregate : IAggregateRoot
{
    public ValueTask Insert(TAggregate aggregate, CancellationToken ct);

    public Task<TAggregate> Get(Guid id, CancellationToken cancellationToken);

    public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);

    public Task Update(TAggregate aggregate, CancellationToken cancellationToken);
}
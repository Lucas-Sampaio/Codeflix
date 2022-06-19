namespace Codeflix.Catalog.Application.Interfaces;

public interface IUnitOfWork
{
    public ValueTask Commit(CancellationToken ct);
}
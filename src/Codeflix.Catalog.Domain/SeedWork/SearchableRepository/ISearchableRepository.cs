namespace Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

public interface ISearchableRepository<Taggregate>
    where Taggregate : IAggregateRoot
{
    ValueTask<SearchOutput<Taggregate>> Search(
       SearchInput input,
       CancellationToken cancellationToken
   );
}
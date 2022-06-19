using Codeflix.Catalog.Domain.SeedWork;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Codeflix.Catalog.Domain.AggregatesModel;

public interface ICategoryRepository
    : IGenericRepository<Category>,
    ISearchableRepository<Category>
{
}
using System;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.AggregatesModel.Category;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[CollectionDefinition(nameof(CategoryTestFixtureCollection))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{ }

public class CategoryTestFixture : IDisposable
{
    public void Dispose()
    {
       
    }

    public DomainEntity GetValidCategory()
        => new("Categoria name", "Categoria description");
}
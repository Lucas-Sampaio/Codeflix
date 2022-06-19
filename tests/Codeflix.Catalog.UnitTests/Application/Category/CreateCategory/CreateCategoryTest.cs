using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Exceptions;
using Moq;
using System.Threading;
using Xunit;

using Entity = Codeflix.Catalog.Domain.AggregatesModel;

using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var uowMock = _fixture.GetUOWMock();
        var useCase = new UseCases.CreateCategory(uowMock.Object, repositoryMock.Object);
        var input = _fixture.GetValidInput();

        //act
        var output = await useCase.Handle(input, CancellationToken.None);

        //Assert
        repositoryMock.Verify(x => x.Insert(It.IsAny<Entity.Category>(), It.IsAny<CancellationToken>()), Times.Once);
        uowMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(output);
        Assert.Equal(input.Name, output.Name);
        Assert.Equal(input.Description, output.Description);
        Assert.Equal(input.IsActive, output.IsActive);
        Assert.NotEqual(default, output.Id);
        Assert.NotEqual(default, output.CreatedAt);
        Assert.NotEqual(default, output.Id);
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use cases")]
    public async void ThrowWhenCanInstantiateAggregate()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var uowMock = _fixture.GetUOWMock();
        var useCase = new UseCases.CreateCategory(uowMock.Object, repositoryMock.Object);
        var input = new CreateCategoryInput("", "a");
        //act
        var erro = Assert.ThrowsAsync<EntityValidationException>(async () => await useCase.Handle(input, CancellationToken.None));
        //Assert
        Assert.NotNull(erro);
    }
}
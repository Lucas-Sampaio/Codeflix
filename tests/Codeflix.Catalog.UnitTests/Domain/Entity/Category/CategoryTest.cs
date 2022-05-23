using Codeflix.Catalog.Domain.Exceptions;
using System;
using System.Linq;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.AggregatesModel.Category;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixtureCollection))]
public class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture)
    {
        _categoryTestFixture = categoryTestFixture;
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };
        var dateTimeBefore = DateTime.Now;

        //act
        var category = new DomainEntity(validData.Name, validData.Description);
        var dateTimeAfter = DateTime.Now;

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default, category.Id);
        Assert.NotEqual(default, category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };
        var dateTimeBefore = DateTime.Now;

        //act
        var category = new DomainEntity(validData.Name, validData.Description, isActive);
        var dateTimeAfter = DateTime.Now.AddSeconds(1);

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default, category.Id);
        Assert.NotEqual(default, category.CreatedAt);
        Assert.True(category.CreatedAt > dateTimeBefore);
        Assert.True(category.CreatedAt < dateTimeAfter);
        Assert.Equal(isActive, category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorNameInvalid))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorNameInvalid(string name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var action = () => new DomainEntity(name, validCategory.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name não pode ser vazio", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("ca")]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var action = () => new DomainEntity(name, validCategory.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name tem que ser maior que 3 caracteres", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreatherThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreatherThan255Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a"));
        var action = () => new DomainEntity(invalidName, validCategory.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name não pode ser maior que 255 caracteres", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreatherThan10000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreatherThan10000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a"));
        var action = () => new DomainEntity(validCategory.Name, invalidDescription);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description não pode ser maior que 10.000 caracteres", exception.Message);
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        //Arrange
        var category = new DomainEntity("Categoria name", "Categoria description", false);

        //Action
        category.Activate();

        //Assert
        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        //Arrange
        var category = _categoryTestFixture.GetValidCategory();

        //Action
        category.Deactivate();

        //Assert
        Assert.False(category.IsActive);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {
        //Arrange
        var category = _categoryTestFixture.GetValidCategory();
        var newValues = new { name = "new category", description = "new description" };
        //Action
        category.Update(newValues.name, newValues.description);

        //Assert
        Assert.Equal(newValues.name, category.Name);
        Assert.Equal(newValues.description, category.Description);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreatherThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreatherThan255Characters()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a"));
        var action = () => category.Update(invalidName, "categoria description");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name não pode ser maior que 255 caracteres", exception.Message);
    }

    [Theory(DisplayName = nameof(UpdateErrorNameInvalid))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData("  ")]
    public void UpdateErrorNameInvalid(string invalidName)
    {
        var category = _categoryTestFixture.GetValidCategory();
        var action = () => category.Update(invalidName, "categoria description");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name não pode ser vazio", exception.Message);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("ca")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var category = _categoryTestFixture.GetValidCategory();
        var action = () => category.Update(invalidName, "categoria description");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name tem que ser maior que 3 caracteres", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreatherThan10000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreatherThan10000Characters()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a"));
        var action = () => category.Update("Categoria name", invalidDescription);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description não pode ser maior que 10.000 caracteres", exception.Message);
    }
}
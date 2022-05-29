using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Validation;
using System;
using System.Collections.Generic;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Domain.Validation;

public class DomainValidationTest
{
    private readonly Faker Faker = new();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();

        DomainValidation.NotNull(value, "value");
    }

    [Fact(DisplayName = nameof(NotNullThrowError))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThrowError()
    {
        string? value = null;

        var error = Assert.Throws<EntityValidationException>(() => DomainValidation.NotNull(value!, "value"));
        Assert.Equal("value não pode ser nulo", error.Message);
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOk()
    {
        var value = Faker.Commerce.ProductName();

        DomainValidation.NotNullOrEmpty(value, "value");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenError))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void NotNullOrEmptyThrowWhenError(string value)
    {
        var error = Assert.Throws<EntityValidationException>(() => DomainValidation.NotNullOrEmpty(value!, "value"));

        Assert.Equal("value não pode ser nulo ou vazio", error.Message);
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValusSmallerThanTheMin), parameters: 3)]
    public void MinLengthThrowWhenLess(string value, int minLegth)
    {
        var error = Assert.Throws<EntityValidationException>(() => DomainValidation.MinLenght(value!, minLegth, "value"));

        Assert.Equal($"value não pode ser menor do que {minLegth} caracteres", error.Message);
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValusGreatherThanTheMin), parameters: 3)]
    public void MinLengthOk(string value, int minLegth)
    {
        DomainValidation.MinLenght(value!, minLegth, "value");
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValusGreatherThanMax), parameters: 3)]
    public void MaxLengthThrowWhenGreater(string value, int maxLength)
    {
        var error = Assert.Throws<EntityValidationException>(() => DomainValidation.MaxLenght(value!, maxLength, "value"));

        Assert.Equal($"value não pode ser maior do que {maxLength} caracteres", error.Message);
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValusLessThanMax), parameters: 3)]
    public void MaxLengthOk(string value, int maxLength)
    {
        DomainValidation.MaxLenght(value!, maxLength, "value");
    }

    public static IEnumerable<object[]> GetValusSmallerThanTheMin(int numberOfTests)
    {
        yield return new object[] { "1234556", 10 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLegth = example.Length + new Random().Next(1, 20);
            yield return new object[] { example, minLegth };
        }
    }

    public static IEnumerable<object[]> GetValusGreatherThanTheMin(int numberOfTests)
    {
        yield return new object[] { "123455", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLegth = example.Length - new Random().Next(1, 20);
            yield return new object[] { example, minLegth };
        }
    }

    public static IEnumerable<object[]> GetValusGreatherThanMax(int numberOfTests)
    {
        yield return new object[] { "123455", 5 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLegth = example.Length - new Random().Next(1, 5);
            yield return new object[] { example, maxLegth };
        }
    }

    public static IEnumerable<object[]> GetValusLessThanMax(int numberOfTests)
    {
        yield return new object[] { "123455", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLegth = example.Length + new Random().Next(1, 5);
            yield return new object[] { example, maxLegth };
        }
    }
}
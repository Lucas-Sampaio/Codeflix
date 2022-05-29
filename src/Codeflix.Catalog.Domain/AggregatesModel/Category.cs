using Codeflix.Catalog.Domain.SeedWork;
using Codeflix.Catalog.Domain.Validation;

namespace Codeflix.Catalog.Domain.AggregatesModel;

public class Category : Entity, IAggregateRoot
{
    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        Validate();
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void Update(string? name = null, string? description = null)
    {
        Name = name ?? Name;
        Description = description ?? Description;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLenght(Name, 3, nameof(Name));
        DomainValidation.MaxLenght(Name, 255, nameof(Name));
        DomainValidation.MaxLenght(Description, 10_000, nameof(Description));
    }
}
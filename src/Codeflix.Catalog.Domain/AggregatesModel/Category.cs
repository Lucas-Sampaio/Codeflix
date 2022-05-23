using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;

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
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} não pode ser vazio");

        if (Name.Length < 3)
            throw new EntityValidationException($"{nameof(Name)} tem que ser maior que 3 caracteres");

        if (Name.Length > 255)
            throw new EntityValidationException($"{nameof(Name)} não pode ser maior que 255 caracteres");

        if (Description.Length > 10000)
            throw new EntityValidationException($"{nameof(Description)} não pode ser maior que 10.000 caracteres");
    }
}
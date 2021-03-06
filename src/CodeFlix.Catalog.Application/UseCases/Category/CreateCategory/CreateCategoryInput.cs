using MediatR;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryInput : IRequest<CreateCategoryOutput>
{
    public CreateCategoryInput(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
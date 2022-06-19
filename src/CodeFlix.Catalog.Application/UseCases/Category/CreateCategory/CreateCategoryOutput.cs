namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryOutput
{
    public CreateCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createAt)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        Id = id;
        CreatedAt = createAt;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
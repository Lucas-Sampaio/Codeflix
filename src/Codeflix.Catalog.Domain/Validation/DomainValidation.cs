using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.Domain.Validation;

public static class DomainValidation
{
    public static void NotNull(object target, string fieldname)
    {
        if (target is null)
            throw new EntityValidationException($"{fieldname} não pode ser nulo");
    }

    public static void NotNullOrEmpty(string target, string fieldname)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldname} não pode ser nulo ou vazio");
    }

    public static void MinLenght(string target, int minLenght, string fieldname)
    {
        if (target.Length < minLenght)
            throw new EntityValidationException($"{fieldname} não pode ser menor do que {minLenght} caracteres");
    }

    public static void MaxLenght(string target, int maxLenght, string fieldname)
    {
        if (target.Length > maxLenght)
            throw new EntityValidationException($"{fieldname} não pode ser maior do que {maxLenght} caracteres");
    }
}
using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (int indice = 0; indice < times; indice++)
        {
            var exampleCategory = fixture.GetExampleCategory();
            var exampleInput = fixture.GetValidInput(exampleCategory.Id);
            yield return new object[] {
                exampleCategory, exampleInput
            };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name não pode ser menor do que 3 caracteres"
                    });
                    break;

                case 1:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name não pode ser maior do que 255 caracteres"
                    });
                    break;

                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description não pode ser maior do que 10000 caracteres"
                    });
                    break;
            }
        }

        return invalidInputsList;
    }
}
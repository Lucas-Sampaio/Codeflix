﻿using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Common;
using Codeflix.Catalog.Domain.AggregatesModel;

namespace Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory : IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _uinitOfWork;

    public UpdateCategory(
        ICategoryRepository categoryRepository,
        IUnitOfWork uinitOfWork)
        => (_categoryRepository, _uinitOfWork)
            = (categoryRepository, uinitOfWork);

    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        category.Update(request.Name, request.Description);
        if (
            request.IsActive != null &&
            request.IsActive != category.IsActive
        )
            if ((bool)request.IsActive!) category.Activate();
            else category.Deactivate();
        await _categoryRepository.Update(category, cancellationToken);
        await _uinitOfWork.Commit(cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}
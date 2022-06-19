﻿using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace Codeflix.Catalog.Application.Common;
public abstract class PaginatedListInput
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string Search { get; set; }
    public string Sort { get; set; }
    public ESearchOrder Dir { get; set; }
    public PaginatedListInput(
        int page, 
        int perPage, 
        string search, 
        string sort, 
        ESearchOrder dir)
    {
        Page = page;
        PerPage = perPage;
        Search = search;
        Sort = sort;
        Dir = dir;
    }

    public SearchInput ToSearchInput()
        => new(Page, PerPage, Search, Sort, Dir);
}

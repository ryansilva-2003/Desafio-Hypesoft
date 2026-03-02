using MediatR;
using Hypesoft.Domain.Repositories;
using Hypesoft.Domain.Entities;
using Hypesoft.Application.Queries;

namespace Hypesoft.Application.Handlers;

public class GetCategoriesHandler 
    : IRequestHandler<GetCategoriesQuery, List<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetCategoriesHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
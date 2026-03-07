using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Application.Queries;
using MediatR;

namespace Hypesoft.Application.Handlers
{
    public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, List<Product>>
    {
        private readonly IProductRepository _repository;

        public SearchProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchAsync(request.Term);
        }
    }
}
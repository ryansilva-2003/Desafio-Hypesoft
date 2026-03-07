using MediatR;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hypesoft.Application.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            IEnumerable<Product> query = products;

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(p => p.Name.Contains(request.Name, System.StringComparison.OrdinalIgnoreCase));

            if (request.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);

            var pagedProducts = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return pagedProducts;
        }
    }
}
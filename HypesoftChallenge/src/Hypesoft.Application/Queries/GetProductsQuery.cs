using MediatR;
using Hypesoft.Domain.Entities;
using System.Collections.Generic;

namespace Hypesoft.Application.Queries
{
    public class GetProductsQuery : IRequest<List<Product>>
    {
        public string? Name { get; set; } 
        public Guid? CategoryId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
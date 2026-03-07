using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public record SearchProductsQuery(string Term) : IRequest<List<Product>>;
}
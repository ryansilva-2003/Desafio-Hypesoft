using MediatR;
using Hypesoft.Domain.Entities;
using System.Collections.Generic;

namespace Hypesoft.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
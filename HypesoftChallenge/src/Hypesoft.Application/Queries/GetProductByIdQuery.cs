using MediatR;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Application.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public Guid Id { get; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}
using MediatR;

namespace Hypesoft.Application.Commands;

public class DeleteProductCommand : IRequest<Unit>
{
    public Guid Id { get; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
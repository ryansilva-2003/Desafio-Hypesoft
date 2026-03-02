using MediatR;

namespace Hypesoft.Application.Commands;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public Guid Id { get; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}
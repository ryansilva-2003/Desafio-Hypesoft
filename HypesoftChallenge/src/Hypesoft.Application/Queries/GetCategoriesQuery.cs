using MediatR;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Application.Queries;

public class GetCategoriesQuery : IRequest<List<Category>>
{
}
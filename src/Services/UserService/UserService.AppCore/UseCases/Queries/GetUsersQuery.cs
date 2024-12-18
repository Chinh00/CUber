using Core.Domain;
using MediatR;

namespace UserService.AppCore.UseCases.Queries;

public record GetUsersQuery : IListQuery<IResult>
{
    public List<FilterModel> Filters { get; set; }
    
    internal class Handler : IRequestHandler<GetUsersQuery, IResult>
    {
        
        public async Task<IResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return Results.Ok(await Task.FromResult(request.Filters));
        }
    }
    
    
}
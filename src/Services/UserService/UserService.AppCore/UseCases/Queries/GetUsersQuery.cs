using Core.Domain;
using Core.Repository;
using MediatR;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Specs;

namespace UserService.AppCore.UseCases.Queries;

public record GetUsersQuery : IListQuery<IResult>
{
    public List<FilterModel> Filters { get; set; }
    
    internal class Handler(IRepository<Customer> customerRepository) : IRequestHandler<GetUsersQuery, IResult>
    {
        public async Task<IResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetUsersSpec(request);
            
            
            return Results.Ok(await Task.FromResult(request.Filters));
        }
    }
    
    
}
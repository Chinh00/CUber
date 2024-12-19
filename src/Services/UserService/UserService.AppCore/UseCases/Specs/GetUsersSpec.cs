using Core.Specifications;
using UserService.AppCore.Domain;
using UserService.AppCore.UseCases.Queries;

namespace UserService.AppCore.UseCases.Specs;

public sealed class GetUsersSpec : ListSpecificationBase<Customer>
{
    public GetUsersSpec(GetUsersQuery query)
    {
        ApplyFilters(query.Filters);
    }
}
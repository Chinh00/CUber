using Core.Domain;

namespace UserService.AppCore.Domain;

public class Customer : AggregateBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
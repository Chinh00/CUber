namespace UserService.AppCore.UseCases.Dtos;

public class DriverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

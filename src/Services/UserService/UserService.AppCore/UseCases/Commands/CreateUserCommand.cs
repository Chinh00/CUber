using Core.Domain;
using Core.EventStore;
using MediatR;

namespace UserService.AppCore.UseCases.Commands;

public record CreateUserCommand(string FullName, string Email, string PhoneNumber) : ICommand<IResult>
{
    internal class Handler(IEventStoreService eventStore) : IRequestHandler<CreateUserCommand, IResult>
    {

        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return Results.Ok("");
        }
    }
}
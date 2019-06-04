using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Authentication
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository repository;
        private User savedUser;
   
        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CreateUserCommand> HandleAsync(CreateUserCommand command)
        { 
                var user = new User(
                username: command.Username,
                email: command.Email,
                hash: command.PasswordHash,
                id: new Id(command.Id)
                );

                this.savedUser = new User();
                this.savedUser = user;

                user = await repository.AddUserAsync(user);

                return command;           
        }

        public User getUser()
        {
            return this.savedUser;
        }
    }
}

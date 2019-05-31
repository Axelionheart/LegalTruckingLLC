using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Tasks;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Tasks;
using Task = LegalTrucking.IntakePlus.Core.Domain.Tasks.Task;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Tasks
{
    public class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand>
    {
        private  ITaskRepository _repository;

        public AssignTaskCommandHandler(ITaskRepository repository)
        {
            _repository = repository;
        }
        
        public async System.Threading.Tasks.Task<AssignTaskCommand> HandleAsync(AssignTaskCommand command)
        {
            var task = new Task(
                new Id(command.AssignedTo),
                command.DueOn,
                command.Task);

            task = await _repository.AddAsync(task);

            command.Id = task.Id;

            return command;
        }
    }
}

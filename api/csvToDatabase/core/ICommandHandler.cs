using System.Threading;
using System.Threading.Tasks;

namespace core
{
    public interface ICommandHandler <in TCommand> where TCommand : Command
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
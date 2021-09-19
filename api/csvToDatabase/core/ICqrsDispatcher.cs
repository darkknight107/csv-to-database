using System.Threading;
using System.Threading.Tasks;

namespace core
{
    public interface ICqrsDispatcher
    {
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : Command;

        Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery: Query<TResult>;
    }
}
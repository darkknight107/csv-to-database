using System;
using System.Threading;
using System.Threading.Tasks;
using core;

namespace core
{
    public class CqrsDispatcher : ICqrsDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CqrsDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : Command
        {
            var handler = (ICommandHandler<TCommand>) _serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
            if (handler == null)
            {
                throw new Exception("The specified command does not have any command handler associated with it!");
            }

            await handler.Handle(command, cancellationToken);
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TResult>
        {
            var requestType = query.GetType();
            var handler = (QueryHandlerWrapper<TResult>)Activator.CreateInstance(
                typeof(QueryHandlerWrapper<,>).MakeGenericType(requestType, typeof(TResult)));
            if (handler == null)
            {
                throw new Exception("The specified query does not have any query handler associated with it!");
            }

            return await handler.Handle(query, _serviceProvider, cancellationToken);
        }
    }
}
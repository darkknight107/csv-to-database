using System;
using System.Threading;
using System.Threading.Tasks;

namespace core
{
    internal abstract class QueryHandlerWrapper<TResult> 
    {

        public abstract Task<TResult> Handle(Query<TResult> query, IServiceProvider serviceProvider,
            CancellationToken cancellationToken);

        protected static THandler GetHandler<THandler>(IServiceProvider serviceProvider)
        {
            return (THandler)serviceProvider.GetService(typeof(THandler));
        }
    }

    internal class QueryHandlerWrapper<TQuery, TResult> : QueryHandlerWrapper<TResult> where TQuery : Query<TResult>
    {
        public override Task<TResult> Handle(Query<TResult> query, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var handler = GetHandler<IQueryHandler<TQuery, TResult>>(serviceProvider);
            return handler.Handle((TQuery) query, cancellationToken);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace core
{
    public interface IQueryHandler <in TQuery, TResult> where TQuery : Query<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core
{
    public interface IDbConnector<TEntity>
    {
        Task<IEnumerable<TEntity>> Query(ISpecification specification);
    }
}
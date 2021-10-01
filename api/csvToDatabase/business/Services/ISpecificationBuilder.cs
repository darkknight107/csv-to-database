using System.Threading;
using System.Threading.Tasks;
using core;

namespace business.Services
{
    public interface ISpecificationBuilder<T>
    {
        ISpecification Build(T entries);
    }
}
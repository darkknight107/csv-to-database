using System.Threading;
using System.Threading.Tasks;

namespace business.Services
{
    public interface ISpecificationBuilder<T>
    {
        Task<string> Build(T entries);
    }
}
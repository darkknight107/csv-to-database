using System.Collections.Generic;
using System.Threading.Tasks;

namespace business.Services
{
    public interface IFileParser<T>
    {
        List<T> ParseAsync(byte[] file);

        T ParseLine(string line);
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Services;
using core;

namespace business.Queries
{
    public class ExtractCsvHeadersQueryHandler : IQueryHandler<ExtractCsvHeadersQuery, List<string>>
    {
        private readonly ICsvParser<List<string>> _csvParser;

        public ExtractCsvHeadersQueryHandler(ICsvParser<List<string>> csvParser)
        {
            _csvParser = csvParser;
        }
        
        public async Task<List<string>> Handle(ExtractCsvHeadersQuery query, CancellationToken cancellationToken)
        {
            var stream = new MemoryStream();
            await query.File.CopyToAsync(stream, cancellationToken);
            var fileByte = stream.ToArray();
            var rows = _csvParser.Parse(fileByte);
            return rows.FirstOrDefault();
        }
    }
}
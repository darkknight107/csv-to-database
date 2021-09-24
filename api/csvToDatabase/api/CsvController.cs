using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using business.Queries;
using core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api
{
    [Route("csv")]
    public class CsvController : FileController
    {
        public CsvController(ICqrsDispatcher cqrsDispatcher) : base(cqrsDispatcher)
        {
        }
        
        [Route("extract-header")]
        [HttpPost]
        public async Task<IActionResult> Extract(IFormFile file, CancellationToken cancellationToken = default)
        {
            var query = new ExtractCsvHeadersQuery
            {
                File = file
            };

            var response = await CqrsDispatcher.Query<ExtractCsvHeadersQuery, List<string>>(query, cancellationToken);
            return Ok(response);
        }
    }
}
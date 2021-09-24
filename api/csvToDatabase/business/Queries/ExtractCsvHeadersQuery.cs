using System.Collections.Generic;
using core;
using Microsoft.AspNetCore.Http;

namespace business.Queries
{
    public class ExtractCsvHeadersQuery : Query<List<string>>
    {
        public IFormFile File { get; set; }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace business.Entity
{
    public class TableEntry
    {
        public List<Dictionary<string, string>> FieldMaps { get; set; }

        public  IFormFile CsvFile { get; set; }
    }
}
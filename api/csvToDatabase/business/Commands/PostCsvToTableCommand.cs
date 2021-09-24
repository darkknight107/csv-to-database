using System.Collections.Generic;
using core;
using Microsoft.AspNetCore.Http;

namespace business.Commands
{
    public class PostCsvToTableCommand : Command
    {
        public List<Dictionary<string, string>> FieldMaps { get; set; }

        public  IFormFile CsvFile { get; set; }
        
    }
}
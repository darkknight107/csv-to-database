using System.Collections.Generic;
using core;
using Microsoft.AspNetCore.Http;

namespace business.Commands
{
    public class PostCsvToTableCommand : Command
    {
        public string SchemaName { get; set; }

        public string TableName { get; set; }
        
        public Dictionary<string, string> FieldMaps { get; set; }

        public  IFormFile CsvFile { get; set; }
        
        
    }
}
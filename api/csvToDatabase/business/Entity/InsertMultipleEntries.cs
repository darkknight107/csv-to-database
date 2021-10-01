using System.Collections.Generic;
using Microsoft.AspNetCore.HttpOverrides;

namespace business.Entity
{
    public class InsertMultipleEntries
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public List<Dictionary<string, string>> Values { get; set; }
    }
}
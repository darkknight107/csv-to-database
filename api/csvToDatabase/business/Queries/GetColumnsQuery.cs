using System.Collections.Generic;
using business.Entity;
using core;

namespace business.Queries
{
    public class GetColumnsQuery : Query<List<Column>>
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
}
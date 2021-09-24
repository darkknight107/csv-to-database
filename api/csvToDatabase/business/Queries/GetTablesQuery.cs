using System.Collections.Generic;
using business.Entity;
using core;

namespace business.Queries
{
    public class GetTablesQuery : Query<List<string>>
    {
        public string Schema { get; set; }
    }
}
using System;
using core;

namespace data
{
    public class TablesGetSpecification : ISpecification
    {
        public TablesGetSpecification(string schema)
        {
            Schema = schema;
        }
        
        public string Schema { get; set; }
        
        public string SqlCommand() => 
            "SELECT schemaname, tablename FROM pg_catalog.pg_tables WHERE schemaname = @Schema";
    }
}
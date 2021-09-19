using core;

namespace data
{
    public class ColumnsGetSpecification : ISpecification
    {
        public ColumnsGetSpecification(string schema, string table)
        {
            Schema = schema;
            Table = table;
        }

        public string Schema { get; set; }
        
        public string Table { get; set; }
        
        public string SqlCommand() => 
            "SELECT table_schema as Schema, table_name as Table, column_name as ColumnName FROM information_schema.columns WHERE table_schema = @Schema AND table_name   = @Table";
    }
}
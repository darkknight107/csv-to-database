using core;

namespace data
{
    public static class ColumnSpecifications
    {
        public static ISpecification GetColumnsByTableName(string schema, string table) =>
            new ColumnsGetSpecification(schema, table);
    }
}
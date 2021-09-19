using core;

namespace data
{
    public static class TableSpecifications
    {
        public static ISpecification GetTables(string schema) => new TablesGetSpecification(schema);
    }
}
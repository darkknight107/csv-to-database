using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using Dapper;

namespace business.Services
{
    public class InsertMultipleSpecificationBuilder : ISpecificationBuilder<InsertMultipleEntries>
    {
        private readonly StringBuilder _builder;

        

        public InsertMultipleSpecificationBuilder(StringBuilder builder)
        {
            _builder = builder;
        }

        public async Task<string> Build(InsertMultipleEntries entries)
        {
            var columnNames = entries.Values[0].Keys.ToList();
            var insertHeaderBuilder = await _builder.BuildCommaSeparatedString(columnNames);

            var values = await  BuildValuesInQuery(entries);
            
            var baseInsertCommand = @$"INSERT INTO @TableName({insertHeaderBuilder})
                                       VALUES {values}";
            return baseInsertCommand;
        }

        private async Task<string> BuildValuesInQuery(InsertMultipleEntries entries)
        {
            var values = "";
            var numberOfValues = entries.Values.Count;

            await Task.Run(() =>
            {
                foreach (var value in entries.Values)
                {
                    if (entries.Values.IndexOf(value) == numberOfValues)
                    {
                        values += $"({_builder.BuildCommaSeparatedString(value.Values.ToList())})";
                        continue;
                    }

                    values += $"({_builder.BuildCommaSeparatedString(value.Values.ToList())}), ";
                }
            });
            return values;
        }
    }
}
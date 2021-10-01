using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using core;
using Dapper;
using data;

namespace business.Services
{
    public class InsertMultipleSpecificationBuilder : ISpecificationBuilder<InsertMultipleEntries>
    {
        private readonly StringBuilder _builder;

        

        public InsertMultipleSpecificationBuilder(StringBuilder builder)
        {
            _builder = builder;
        }

        public ISpecification Build(InsertMultipleEntries entries)
        {
            var columnNames = entries.Values[0].Keys.ToList();
            var insertHeaderBuilder = _builder.BuildCommaSeparatedString(columnNames);

            var values = BuildValuesInQuery(entries);
            
            var baseInsertCommand = @$"INSERT INTO {entries.SchemaName}.{entries.TableName}({insertHeaderBuilder})
                                       VALUES {values}";
            return new Specification(baseInsertCommand);
        }

        private string BuildValuesInQuery(InsertMultipleEntries entries)
        {
            var values = "";
            var numberOfValues = entries.Values.Count;

           
            foreach (var value in entries.Values)
            { 
                if (entries.Values.IndexOf(value) == numberOfValues - 1) 
                { 
                    values += $"({_builder.BuildCommaSeparatedString(value.Values.ToList())})";
                    continue; 
                }
                values += $"({_builder.BuildCommaSeparatedString(value.Values.ToList())}), ";
            }
          
            return values;
        }
    }
}
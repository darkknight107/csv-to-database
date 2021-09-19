using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using core;
using data;

namespace business.Queries
{
    public class GetColumnsQueryHandler : IQueryHandler<GetColumnsQuery, List<Column>>
    {
        private readonly IDbConnector<Column> _connector;

        public GetColumnsQueryHandler(IDbConnector<Column> connector)
        {
            _connector = connector;
        }
        
        public async Task<List<Column>> Handle(GetColumnsQuery query, CancellationToken cancellationToken)
        {
            var specification = ColumnSpecifications.GetColumnsByTableName(query.SchemaName, query.TableName);
            var result = await _connector.Query(specification);
            return result.ToList();
        }
    }
}
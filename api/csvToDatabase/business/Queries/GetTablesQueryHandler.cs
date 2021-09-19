using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using core;
using data;

namespace business.Queries
{
    public class GetTablesQueryHandler : IQueryHandler<GetTablesQuery,List<Table>>
    {
        private readonly IDbConnector<Table> _connector;

        public GetTablesQueryHandler(IDbConnector<Table> connector)
        {
            _connector = connector;
        }
        
        public async Task<List<Table>> Handle(GetTablesQuery query, CancellationToken cancellationToken)
        {
            var specification = TableSpecifications.GetTables(query.Schema);
            var result = await _connector.Query(specification);
            return result.ToList();
        }
    }
}
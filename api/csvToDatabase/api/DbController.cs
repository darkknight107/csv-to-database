using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using business.Queries;
using core;
using data;
using Microsoft.AspNetCore.Mvc;

namespace api
{
    [Route("database")]
    public class DbController : ControllerBase
    {
        private readonly ICqrsDispatcher _cqrsDispatcher;

        public DbController(ICqrsDispatcher cqrsDispatcher)
        {
            _cqrsDispatcher = cqrsDispatcher;
        }

        [HttpGet]
        [Route("tables")]
        public async Task<IActionResult> GetTables(string schema, CancellationToken cancellationToken = default)
        {
            var query = new GetTablesQuery
            {
                Schema = schema
            };
            var response = await _cqrsDispatcher.Query<GetTablesQuery, List<Table>>(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("columns")]
        public async Task<IActionResult> GetColumns(string schema, string table,
            CancellationToken cancellationToken = default)
        {
            var query = new GetColumnsQuery
            {
                SchemaName = schema,
                TableName = table
            };
            var response = await _cqrsDispatcher.Query<GetColumnsQuery, List<Column>>(query, cancellationToken);
            return Ok(response);
        }
    }
}
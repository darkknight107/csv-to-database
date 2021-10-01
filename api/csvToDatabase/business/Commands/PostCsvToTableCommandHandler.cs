using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Entity;
using business.Services;
using core;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace business.Commands
{
    public class PostCsvToTableCommandHandler : ICommandHandler<PostCsvToTableCommand>
    {
        private readonly IFileParser<List<string>> _fileParser;
        private readonly ISpecificationBuilder<InsertMultipleEntries> _insertMultipleEntriesSpecBuilder;
        private readonly IDbConnector<InsertMultipleEntries> _dbConnector;

        public PostCsvToTableCommandHandler(ICsvParser<List<string>> fileParser,
            ISpecificationBuilder<InsertMultipleEntries> insertMultipleEntriesSpecBuilder, IDbConnector<InsertMultipleEntries> dbConnector)
        {
            _fileParser = fileParser;
            _insertMultipleEntriesSpecBuilder = insertMultipleEntriesSpecBuilder;
            _dbConnector = dbConnector;
        }
        
        public async Task Handle(PostCsvToTableCommand command, CancellationToken cancellationToken)
        {
            await using var stream = new MemoryStream();
            await command.CsvFile.CopyToAsync(stream, cancellationToken);
            
            var fileByte = stream.ToArray();
            var rows = _fileParser.Parse(fileByte);
            
            rows[0] = TranslateHeader(rows[0], command.FieldMaps);
            
            var entries = new InsertMultipleEntries
            {
                SchemaName = command.SchemaName,
                TableName = command.TableName,
                Values = BuildInsertRequest(rows)
            };

            var specification = _insertMultipleEntriesSpecBuilder.Build(entries);

            await _dbConnector.Query(specification);
        }

        private static List<Dictionary<string, string>> BuildInsertRequest(List<List<string>> parsedData)
        {
            var headers = parsedData[0];
            
            //remove the header from the data 
            parsedData.RemoveAt(0);

            var mappedData = new List<Dictionary<string, string>>();

            foreach (var row in parsedData)
            {
                if (row.Count < headers.Count)
                {
                    continue;
                }
                
                var mappedRow = headers.Zip(row, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v);
                
                mappedData.Add(mappedRow);
            }

            return mappedData;
        }

        private static List<string> TranslateHeader(List<string> headers, Dictionary<string, string> mappings)
        {
            return  headers.Select(header => header.Replace(header, mappings[header])).ToList() ;
        }
    }
}
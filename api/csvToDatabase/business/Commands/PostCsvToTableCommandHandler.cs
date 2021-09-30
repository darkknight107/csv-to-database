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

        public PostCsvToTableCommandHandler(ICsvParser<List<string>> fileParser,
            ISpecificationBuilder<InsertMultipleEntries> insertMultipleEntriesSpecBuilder)
        {
            _fileParser = fileParser;
            _insertMultipleEntriesSpecBuilder = insertMultipleEntriesSpecBuilder;
        }
        
        public async Task Handle(PostCsvToTableCommand command, CancellationToken cancellationToken)
        {
            await using var stream = new MemoryStream();
            await command.CsvFile.CopyToAsync(stream, cancellationToken);
            
            var fileByte = stream.ToArray();
            var rows = _fileParser.Parse(fileByte);
            
            rows[0] = translateHeader(rows[0], command.FieldMaps);

            var entries = new InsertMultipleEntries
            {
                TableName = command.TableName
                //TODO Add Entry Values by translating from above parsed values to list of dictionary.
            };
        }

        private List<string> translateHeader(List<string> headers, Dictionary<string, string> mappings)
        {
            return  headers.Select(header => header.Replace(header, mappings[header])).ToList() ;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using business.Services;
using core;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace business.Commands
{
    public class PostCsvToTableCommandHandler : ICommandHandler<PostCsvToTableCommand>
    {
        private readonly IFileParser<List<string>> _fileParser;

        public PostCsvToTableCommandHandler(ICsvParser<List<string>> fileParser)
        {
            _fileParser = fileParser;
        }
        
        public async Task Handle(PostCsvToTableCommand command, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();
            command.CsvFile.CopyTo(stream);
            var fileByte = stream.ToArray();
            var rows = _fileParser.Parse(fileByte);
            var headers = (rows.FirstOrDefault() ?? new List<string>()).OrderBy(h => h).ToList();

            var mappings = command.FieldMaps.Values.OrderBy(v => v).ToList();

        }
    }
}
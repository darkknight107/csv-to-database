using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using business.Services;
using core;

namespace business.Commands
{
    public class PostCsvToTableCommandHandler : ICommandHandler<PostCsvToTableCommand>
    {
        private readonly IFileParser<List<string>> _fileParser;

        public PostCsvToTableCommandHandler(IFileParser<List<string>> fileParser)
        {
            _fileParser = fileParser;
        }
        
        public async Task Handle(PostCsvToTableCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
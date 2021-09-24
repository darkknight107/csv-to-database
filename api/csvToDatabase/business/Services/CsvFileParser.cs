using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace business.Services
{
    public class CsvFileParser : ICsvParser<List<string>>
    {
        public List<List<string>> ParseAsync(byte[] file)
        {
            var data = Encoding.Default.GetString(file);
            var  csvLine= data.Split(Environment.NewLine);
            return csvLine.Select(ParseLine).ToList();
            
        }

        public List<string> ParseLine(string line)
        {
            return line.Split(",").ToList();
        }
    }
}
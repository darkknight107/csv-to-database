using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace business.Services
{
    public class CsvFileParser : ICsvParser<List<string>>
    {
        public List<List<string>> Parse(byte[] file)
        {
            //remove the unwanted characters from the start of the string.
            var data = Encoding.ASCII.GetString(file, 3, file.Length - 3);
            var  csvLine= data.Split(Environment.NewLine);
            return csvLine.Select(ParseLine).ToList();
            
        }

        public List<string> ParseLine(string line)
        {
            return line.Split(",").ToList();
        }
    }
}
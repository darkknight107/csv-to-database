using System.Collections.Generic;
using System.Threading.Tasks;

namespace business.Services
{
    public class StringBuilder
    {
        public string BuildCommaSeparatedString(List<string> values)
        {
            var stringValue = "";
            var listLength = values.Capacity;
            foreach (var value in values) 
            { 
                if (values.IndexOf(value) == listLength - 1) 
                { 
                    stringValue += $"{value}"; 
                    continue;
                } 
                
                stringValue += $"{value}, ";
            }
            return stringValue;
        }
    }
}
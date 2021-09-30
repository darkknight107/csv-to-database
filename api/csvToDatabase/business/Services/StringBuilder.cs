using System.Collections.Generic;
using System.Threading.Tasks;

namespace business.Services
{
    public class StringBuilder
    {
        public async Task<string> BuildCommaSeparatedString(List<string> values)
        {
            var stringValue = "";
            var listLength = values.Capacity;
            
            await Task.Run(() =>
            {
                foreach (var value in values)
                {
                    if (values.IndexOf(value) == listLength - 1)
                    {
                        stringValue += $"{value}";
                        continue;
                    }

                    stringValue += $"{value}, ";
                }
            });
            
            return stringValue;
        }
    }
}
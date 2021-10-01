using core;

namespace data
{
    public class Specification : ISpecification
    {
        public string CommandText { get; }

        public Specification(string commandText)
        {
            CommandText = commandText;
        }

        public string SqlCommand() => CommandText;
    }
}
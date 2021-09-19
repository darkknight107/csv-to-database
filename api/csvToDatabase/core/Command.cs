using System;

namespace core
{
    public class Command
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
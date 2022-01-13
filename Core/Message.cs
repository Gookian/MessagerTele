using System;

namespace Core
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}

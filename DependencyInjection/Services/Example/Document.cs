using System;

namespace DependencyInjection.Services
{
    public class Document
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Byte[] File { get; set; }
    }
}

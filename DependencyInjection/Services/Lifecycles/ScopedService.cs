using System;

namespace DependencyInjection.Services
{  
    public class ScopedService : IScopedService
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Value { get; set; } = "Default";
    }
}

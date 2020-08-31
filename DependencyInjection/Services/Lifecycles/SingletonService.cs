using System;

namespace DependencyInjection.Services
{
    public class SingletonService : ISingletonService
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Value { get; set; } = "Default";
    }
}

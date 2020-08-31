using System;

namespace DependencyInjection.Services
{  
    public interface IScopedService
    {
        Guid Id { get; }
        string Value { get; set; }
    }
}

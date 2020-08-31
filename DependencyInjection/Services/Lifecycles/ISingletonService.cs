using System;

namespace DependencyInjection.Services
{
    public interface ISingletonService
    {
        Guid Id { get; }
        string Value { get; set; }
    }
}

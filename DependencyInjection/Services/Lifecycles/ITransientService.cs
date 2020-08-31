using System;

namespace DependencyInjection.Services
{
    public interface ITransientService
    {
        Guid Id { get; }
        string Value { get; set; }
    }
}

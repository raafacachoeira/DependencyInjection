﻿using System;

namespace DependencyInjection.Services
{
    public class TransientService : ITransientService
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Value { get; set; } = "Default";
    }
}

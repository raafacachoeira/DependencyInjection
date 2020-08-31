using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection.Controllers
{
    public class DependencyController : Controller
    {
        private readonly ISingletonService _singletonService;
        private readonly IScopedService _scopedService;
        private readonly ITransientService _transientService;
        private readonly IServiceProvider _serviceProvider;

        public DependencyController(
            ISingletonService singletonService,
            IScopedService scopedService,
            ITransientService transientService,
            IServiceProvider serviceProvider)
        {
            _singletonService = singletonService;
            _scopedService    = scopedService;
            _transientService = transientService;
            _serviceProvider = serviceProvider;
        }


        /// <summary>
        /// Entrega apenas uma instancia, sempre a mesma
        /// </summary>
        public IActionResult Singleton(string newValue)
        {
            var currentValue = _singletonService.Value;

            _singletonService.Value = newValue;

            var singletonIntance = _serviceProvider.GetService<ISingletonService>();

            return Json(singletonIntance.Value);
        }


        /// <summary>
        /// Entrega a mesma instancia dentro da mesma requisição
        /// </summary>
        public IActionResult Scoped(string newValue)
        {
            var currentValue = _scopedService.Value;

            _scopedService.Value = newValue;

            var scopedIntance1 = _serviceProvider.GetService<IScopedService>();
            var scopedIntance2 = _serviceProvider.GetService<IScopedService>();
            var scopedIntance3 = _serviceProvider.GetService<IScopedService>();


            //check singleton value
            var singletonIntance = _serviceProvider.GetService<ISingletonService>();

            return Json(_scopedService.Value);
        }

        /// <summary>
        /// Sempre nos entrega uma nova instancia da classe
        /// </summary>
        public IActionResult Transient(string newValue)
        {
            var currentValue = _transientService.Value;

            _transientService.Value = newValue;

            var transientIntance1 = _serviceProvider.GetService<ITransientService>();
            var transientIntance2 = _serviceProvider.GetService<ITransientService>();
            var transientIntance3 = _serviceProvider.GetService<ITransientService>();


            //check singleton value
            var singletonIntance = _serviceProvider.GetService<ISingletonService>();

            return Json(_scopedService.Value);
        }

    }
}

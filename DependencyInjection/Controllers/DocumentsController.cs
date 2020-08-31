using DependencyInjection.Models;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace DependencyInjection.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentRepository _repository;
        private readonly Func<FactorySettings, IDocumentRepository> _funcRepository;
        private readonly FactorySettings _factorySettings;

        public DocumentsController(
            IOptions<FactorySettings> factorySettings,
            IDocumentRepository repository,
            Func<FactorySettings, IDocumentRepository> funcRepository)
        {
            _factorySettings = factorySettings.Value;
            _repository = repository;
            _funcRepository = funcRepository;
        }

        /// <summary>
        /// Entrega a instancia de classe conforme parametrizado no config
        /// </summary>
        public IActionResult Index(long id)
        {
            var document = _repository.GetById(id);

            return Json(document);
        }


        /// <summary>
        /// Entrega a instancia de classe conforme passado por func
        /// </summary>
        public IActionResult Runtime(long id)
        {
            var documentFromElasticSearch = _funcRepository(new FactorySettings { SourceDocuments = "ElasticSearch" }).GetById(id);
            var documentFromFolder = _funcRepository(new FactorySettings { SourceDocuments = "Folder" }).GetById(id);
            var documentFromDatabase = _funcRepository(new FactorySettings { SourceDocuments = "Database" }).GetById(id);

            var documentFromSettings = _funcRepository(_factorySettings).GetById(id);


            return Json(new
            {

                documentFromSettings, 
                documentFromElasticSearch,
                documentFromFolder,
                documentFromDatabase
            });
        }

    }
}

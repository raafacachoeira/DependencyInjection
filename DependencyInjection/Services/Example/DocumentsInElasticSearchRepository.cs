namespace DependencyInjection.Services
{
    public class DocumentsInElasticSearchRepository : IDocumentRepository
    {
        public Document GetById(long id)
        {
            return new Document
            {
                Id = id,
                Title = "From ElasticSearch"
            };
        }
    }
}

namespace DependencyInjection.Services
{
    public class DocumentsInDatabaseRepository : IDocumentRepository
    {
        public Document GetById(long id)
        {
            return new Document
            {
                Id = id,
                Title = "From Database"
            };
        }
    }
}

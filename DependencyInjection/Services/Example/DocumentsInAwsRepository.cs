namespace DependencyInjection.Services
{
    public class DocumentsInAwsRepository : IDocumentRepository
    {
        public Document GetById(long id)
        {
            return new Document
            {
                Id = id,
                Title = "From Aws"
            };
        }
    }
}

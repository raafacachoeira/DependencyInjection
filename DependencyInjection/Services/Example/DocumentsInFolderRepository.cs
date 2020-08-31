namespace DependencyInjection.Services
{
    public class DocumentsInFolderRepository : IDocumentRepository
    {
        public Document GetById(long id)
        {
            return new Document
            {
                Id = id,
                Title = "From Folder"
            };
        }
    }
}

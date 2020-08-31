namespace DependencyInjection.Services
{
    public interface IDocumentRepository
    {
        Document GetById(long id);
    }
}

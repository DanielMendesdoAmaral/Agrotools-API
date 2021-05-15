using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Author Get(string id);
    }
}

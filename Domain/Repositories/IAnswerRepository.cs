using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAnswerRepository
    {
        void Create(Answer answer);
        Answer Get(string questionId);
    }
}

using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IAnswerRepository
    {
        void Create(Answer answer);
        ICollection<Answer> Get(string questionId);
    }
}

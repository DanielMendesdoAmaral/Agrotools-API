using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IQuestionRepository
    {
        void Create(Question question);
        ICollection<Question> Get(string formId);
    }
}

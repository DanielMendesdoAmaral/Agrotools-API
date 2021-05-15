using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IFormRepository
    {
        void Create(Form form);
        ICollection<Form> GetAll();
        Form Get(string id);
        void Update(Form form);
    }
}

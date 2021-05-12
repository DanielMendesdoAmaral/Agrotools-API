using Commom.Entities;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public ICollection<Form> Forms { get; private set; }

        public User(string name)
        {
            Name = name;
        }
    }
}

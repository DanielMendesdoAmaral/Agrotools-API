using Commom.Entities;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Author : Entity
    {
        public string Name { get; private set; }
        //public ICollection<Form> Forms { get; private set; }

        public Author(
            string name
            //List<Form> forms
        )
        {
            Name = name.Trim();
            //Forms = forms;
        }
    }
}

using Commom.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Question : Entity
    {
        public string Title { get; private set; }
        public ICollection<Answer> Answers { get; private set; }
        public Form Form { get; private set; }
        public Guid FormId { get; private set; }

        public Question(string title)
        {
            Title = title;
        }
    }
}

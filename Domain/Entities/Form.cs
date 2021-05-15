using Commom.Entities;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Form : Entity
    {
        public string Title { get; private set; }
        //public ICollection<Question> Questions { get; private set; }
        public string AuthorId { get; private set; }
        public bool Answered { get; private set; }

        public Form(
            string title, 
            //List<Question> questions,
            string authorId
        )
        {
            Title = title.Trim();
            //Questions = questions;
            AuthorId = authorId.Trim();
            Answered = false;
        }

        public void TurnAnswered()
        {
            Answered = true;
        }
    }
}

using Commom.Entities;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Question : Entity
    {
        public string Text { get; private set; }
        //public ICollection<Answer> Answers { get; private set; }
        public string FormId { get; private set; }

        public Question(
            string text,
            //List<Answer> answers,
            string formId
        )
        {
            Text = text.Trim();
            //Answers = answers;
            FormId = formId.Trim();
        }
    }
}

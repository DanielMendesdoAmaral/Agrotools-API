using Commom.Entities;
using System;

namespace Domain.Entities
{
    public class Answer : Entity
    {
        public string Text { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Answer(string text)
        {
            Text = text;
        }
    }
}

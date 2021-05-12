using Commom.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Form : Entity
    {
        public ICollection<Question> Questions { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public bool Answered { get; private set; }

        public Form()
        {

        }
    }
}

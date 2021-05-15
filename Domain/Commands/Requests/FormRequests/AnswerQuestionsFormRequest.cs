using Commom.Commands;
using Domain.Entities;
using Flunt.Validations;
using System.Collections.Generic;

namespace Domain.Commands.Requests.FormRequests
{
    public class AnswerQuestionsFormRequest : CommandRequest
    {
        public string AuthorName { get; set; }
        public string FormId { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public AnswerQuestionsFormRequest()
        {
            Answers = new List<Answer>();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<AnswerQuestionsFormRequest>()
                .Requires()
                .IsTrue((AuthorName.Length > 1) && (AuthorName.Length < 41), "AuthorName", "Insira um nome entre 2 à 40 caracteres!!")
            );
        }
    }
}

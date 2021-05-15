using Commom.Commands;
using Domain.Entities;
using Flunt.Validations;
using System.Collections.Generic;

namespace Domain.Commands.Requests.FormRequests
{
    public class CreateFormRequest : CommandRequest
    {
        public string FormTitle { get; set; }
        public string AuthorName { get; set; }
        public ICollection<string> Questions { get; set; }

        public CreateFormRequest(
            string formTitle,
            string authorName,
            ICollection<string> questions
        )
        {
            FormTitle = formTitle;
            AuthorName = authorName;
            Questions = questions;
        }

        public override void Validate()
        {
            AddNotifications(new Contract<CreateFormRequest>()
                .Requires()
                .IsTrue((FormTitle.Length > 2) && (FormTitle.Length < 21), "FormTitle", "Um título satisfatório deve ter de 3 à 21 caracteres!")
                .IsTrue((AuthorName.Length > 1) && (AuthorName.Length < 41), "AuthorName", "Insira um nome entre 2 à 40 caracteres!!")
                .IsTrue(Questions.Count > 0, "QuestionsCount", "Crie pelo menos 1 pergunta para este formulário!")
            );
        }
    }
}

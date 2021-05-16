using System.Collections.Generic;

namespace Domain.Queries.Responses.FormRequests
{
    public class GetAnswersFormResponse
    {
        public string QuestionText { get; set; }
        public ICollection<AnswerResponse> Answers { get; set; }
    }
}

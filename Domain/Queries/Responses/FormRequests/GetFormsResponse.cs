using System;

namespace Domain.Queries.Responses.FormRequests
{
    public class GetFormsResponse
    {
        public string FormId { get; set; }
        public string FormTitle { get; set; }
        public string AuthorName { get; set; }
        public int QuestionsNumber { get; set; }
        public string CreatedAt { get; set; }
        public bool Answered { get; set; }
    }
}

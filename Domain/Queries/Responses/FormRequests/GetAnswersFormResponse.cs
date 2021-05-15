namespace Domain.Queries.Responses.FormRequests
{
    public class GetAnswersFormResponse
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string AnsweredAt { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string AuthorName { get; set; }
    }
}

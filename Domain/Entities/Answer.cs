using Commom.Entities;

namespace Domain.Entities
{
    public class Answer : Entity
    {
        public string Text { get; private set; }
        public string QuestionId { get; private set; }
        public string AuthorId { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public Answer(
            string text,
            string questionId,
            string authorId,
            decimal latitude,
            decimal longitude
        )
        {
            Text = text.Trim();
            QuestionId = questionId.Trim();
            AuthorId = authorId.Trim();
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

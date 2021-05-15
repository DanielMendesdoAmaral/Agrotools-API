using Commom.Queries;

namespace Domain.Queries.Requests.FormRequests
{
    public class GetQuestionsFormRequest : QueryRequest
    {
        public string FormId { get; set; }

        public override void Validate() { }
    }
}

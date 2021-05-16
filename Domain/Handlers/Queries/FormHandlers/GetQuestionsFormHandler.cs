using Commom.Queries;
using Domain.Queries.Requests.FormRequests;
using Domain.Queries.Responses.FormRequests;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Queries.FormHandlers
{
    public class GetQuestionsFormHandler : IRequestHandler<GetQuestionsFormRequest, GenericQueryResult>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFormRepository _formRepository;

        public GetQuestionsFormHandler(IQuestionRepository questionRepository, IFormRepository formRepository)
        {
            _questionRepository = questionRepository;
            _formRepository = formRepository;
        }

        public Task<GenericQueryResult> Handle(GetQuestionsFormRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var questions = _questionRepository.Get(request.FormId);
                var form = _formRepository.Get(request.FormId);

                var result = new
                {
                    FormTitle = form.Title,
                    FormId = form.Id,
                    Questions = questions.Select(
                        q => new GetQuestionsFormResponse()
                        {
                            QuestionId = q.Id,
                            Text = q.Text
                        }
                    )
                    .OrderBy(q => q.Text)
                };

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch 
            {
                return Task.FromResult(new GenericQueryResult(500, "Erro no servidor! Desculpe-nos.", null));
            }
        }
    }
}

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
    public class GetFormsHandler : IRequestHandler<GetFormsRequest, GenericQueryResult>
    {
        private readonly IFormRepository _formRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IQuestionRepository _questionRepository;

        public GetFormsHandler(IFormRepository formRepository, IAuthorRepository authorRepository, IQuestionRepository questionRepository)
        {
            _formRepository = formRepository;
            _authorRepository = authorRepository;
            _questionRepository = questionRepository;
        }

        public Task<GenericQueryResult> Handle(GetFormsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var forms = _formRepository.GetAll();

                if (forms == null)
                    return Task.FromResult(new GenericQueryResult(404, "Não existe nenhum form cadastrado!", null));

                var result = forms.Select(
                    f =>
                    new GetFormsResponse()
                    {
                        FormId = f.Id,
                        FormTitle = f.Title,
                        AuthorName = _authorRepository.Get(f.AuthorId).Name,
                        QuestionsNumber = _questionRepository.Get(f.Id).Count,
                        CreatedAt = f.CreatedAt.ToString("dd/MM/yyyy"),
                        Answered = f.Answered
                    }
                );

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch 
            {
                return Task.FromResult(new GenericQueryResult(500, "Erro no servidor! Desculpe-nos.", null));
            }
        }
    }
}

using Commom.Commands;
using Domain.Commands.Requests.FormRequests;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Commands.FormHandlers
{
    public class AnswerQuestionsFormHandler : IRequestHandler<AnswerQuestionsFormRequest, GenericCommandResult>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IFormRepository _formRepository;

        public AnswerQuestionsFormHandler(
            IAuthorRepository authorRepository,
            IAnswerRepository answerRepository,
            IFormRepository formRepository,
            IQuestionRepository questionRepository
        )
        {
            _authorRepository = authorRepository;
            _answerRepository = answerRepository;
            _formRepository = formRepository;
        }

        public Task<GenericCommandResult> Handle(AnswerQuestionsFormRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var author = new Author(request.AuthorName);

                _authorRepository.Create(author);

                foreach(var answer in request.Answers)
                {
                    _answerRepository.Create(
                        new Answer(
                            answer.Text,
                            answer.QuestionId,
                            author.Id,
                            answer.Latitude,
                            answer.Longitude
                        )
                    );
                }

                var form = _formRepository.Get(request.FormId);

                if (!form.Answered)
                {
                    form.TurnAnswered();

                    _formRepository.Update(form);
                }

                return Task.FromResult(new GenericCommandResult(200, "Respostas ao formulário registrada com sucesso!", null));
            }
            catch
            {
                return Task.FromResult(new GenericCommandResult(500, "Erro no servidor! Desculpe-nos.", null));
            }
        }
    }
}

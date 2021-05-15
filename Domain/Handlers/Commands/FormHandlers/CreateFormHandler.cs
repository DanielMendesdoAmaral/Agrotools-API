using Commom.Commands;
using Domain.Commands.Requests.FormRequests;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Commands.FormHandlers
{
    public class CreateFormHandler : IRequestHandler<CreateFormRequest, GenericCommandResult>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IFormRepository _formRepository;
        private readonly IQuestionRepository _questionRepository;

        public CreateFormHandler(
            IAuthorRepository authorRepository,
            IFormRepository formRepository,
            IQuestionRepository questionRepository
        )
        {
            _authorRepository = authorRepository;
            _formRepository = formRepository;
            _questionRepository = questionRepository;
        }

        public Task<GenericCommandResult> Handle(CreateFormRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var author = new Author(request.AuthorName);
                _authorRepository.Create(author);

                var form = new Form(request.FormTitle, author.Id);
                _formRepository.Create(form);

                foreach(var question in request.Questions)
                {
                    _questionRepository.Create(
                        new Question(
                            question,
                            form.Id
                        )
                    );
                }

                return Task.FromResult(new GenericCommandResult(200, "Form criado com sucesso!", null));
            }
            catch
            {
                return Task.FromResult(new GenericCommandResult(500, "Erro no servidor! Desculpe-nos.", null));
            }
        }
    }
}

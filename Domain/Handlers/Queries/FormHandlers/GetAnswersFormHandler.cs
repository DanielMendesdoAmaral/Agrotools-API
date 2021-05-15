using Commom.Queries;
using Domain.Queries.Requests.FormRequests;
using Domain.Queries.Responses.FormRequests;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Queries.FormHandlers
{
    public class GetAnswersFormHandler : IRequestHandler<GetAnswersFormRequest, GenericQueryResult>
    {
        private readonly IFormRepository _formRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IAuthorRepository _authorRepository;

        public GetAnswersFormHandler(
            IFormRepository formRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IAuthorRepository authorRepository
        )
        {
            _formRepository = formRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _authorRepository = authorRepository;
        }

        public Task<GenericQueryResult> Handle(GetAnswersFormRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var questionsAndAnswers = new List<GetAnswersFormResponse>();

                var form = _formRepository.Get(request.FormId);

                if(!form.Answered)
                    return Task.FromResult(new GenericQueryResult(404, "Este formulário ainda não foi respondido! Que tal ser o primeiro?", null));

                var questions = _questionRepository.Get(form.Id);

                foreach(var question in questions)
                {
                    var answer = _answerRepository.Get(question.Id);

                    questionsAndAnswers.Add(new GetAnswersFormResponse()
                    {
                        QuestionText = question.Text,
                        AnswerText = answer.Text,
                        AnsweredAt = answer.CreatedAt.ToString("dd/MM/yyyy"),
                        Latitude = answer.Latitude,
                        Longitude = answer.Longitude,
                        AuthorName = _authorRepository.Get(answer.AuthorId).Name
                    });
                }

                var result = new
                {
                    FormTitle = form.Title,
                    AuthorName = _authorRepository.Get(form.AuthorId).Name,
                    QuestionsAndAnswers = questionsAndAnswers
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

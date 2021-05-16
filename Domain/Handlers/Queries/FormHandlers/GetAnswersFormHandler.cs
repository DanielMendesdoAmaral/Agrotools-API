using Commom.Queries;
using Domain.Queries.Requests.FormRequests;
using Domain.Queries.Responses.FormRequests;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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
                    var answers = _answerRepository.Get(question.Id);

                    questionsAndAnswers.Add(new GetAnswersFormResponse()
                    {
                        QuestionText = question.Text,
                        Answers = answers.Select(
                            a => new AnswerResponse()
                            {
                                AnswerText = a.Text,
                                AnsweredAt = a.CreatedAt.ToString("dd/MM/yyyy"),
                                Latitude = a.Latitude,
                                Longitude = a.Longitude,
                                AuthorName = _authorRepository.Get(a.AuthorId).Name
                            }
                        ).OrderBy(a => a.AnsweredAt).ToList()
                    });
                }

                var result = new
                {
                    FormTitle = form.Title,
                    AuthorName = _authorRepository.Get(form.AuthorId).Name,
                    QuestionsAndAnswers = questionsAndAnswers.OrderBy(qa => qa.QuestionText)
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

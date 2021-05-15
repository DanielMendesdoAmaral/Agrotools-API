using API.Controllers;
using Domain.Commands.Requests.FormRequests;
using Domain.Queries.Requests.FormRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FormController : BaseController
    {
        public FormController(IMediator mediator) : base(mediator){}

        [HttpGet("list")]
        public async Task<ObjectResult> GetAll()
        {
            var request = new GetFormsRequest();
            return await Result(request);
        }

        [HttpGet("get-questions/{formId}")]
        public async Task<ObjectResult> GetQuestions(string formId)
        {
            var request = new GetQuestionsFormRequest();
            request.FormId = formId;
            return await Result(request);
        }

        [HttpGet("get-questions-and-answers/{formId}")]
        public async Task<ObjectResult> GetQuestionsAndAnswers(string formId)
        {
            var request = new GetAnswersFormRequest();
            request.FormId = formId;
            return await Result(request);
        }

        [HttpPost("create")]
        public async Task<ObjectResult> Post(
            [FromBody] CreateFormRequest request
        )
        {
            return await Result(request);
        }

        [HttpPost("answer")]
        public async Task<ObjectResult> AnswerQuestionsForm(
            [FromBody] AnswerQuestionsFormRequest request
        )
        {
            return await Result(request);
        }
    }
}

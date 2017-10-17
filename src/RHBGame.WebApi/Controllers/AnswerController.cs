using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/answers")]
    public sealed partial class AnswerController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public AnswerController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        // List of answers shown by selected question
        [Route("findbyquestion"), HttpPost]
        public async Task<IEnumerable<Answer>> FindByQuestionAsync([Required] FindByQuestionParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);
            
            // Returns a list of answers by going through questions with the requested questions id
            return
                await _repository.Questions
                    .Where(x => x.Id == parameters.QuestionId)
                    .SelectMany(x => x.Answers)
                    .ToListAsync();
        }

        [Route("create"), HttpPost]
        public async Task CreateAsync([Required] CreateParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            var answer = new Answer()
            {
                Text = parameters.Answer,
                PlayerId = parameters.PlayerId,
                QuestionId = parameters.QuestionId,
                Created = DateTime.UtcNow
            };

            _repository.Answers.Add(answer);

            await _repository.SaveChangesAsync();
        }
    }
}

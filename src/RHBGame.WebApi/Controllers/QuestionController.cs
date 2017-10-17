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
    [RoutePrefix("api/questions")]
    public sealed partial class QuestionController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public QuestionController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [Route("list"), HttpPost]
        public async Task<IEnumerable<Question>> ListAsync([Required] ListParams parameters)
        {
            // Check if user is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            // Returns list of Questions
            return await _repository.Questions.ToListAsync();
        }

        [Route("findbyid"), HttpPost]
        public async Task<Question> FindByIdAsync([Required] FindByIdParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            return await _repository.Questions.FindAsync(parameters.QuestionId);
        }

        [Route("findbytopic"), HttpPost]
        public async Task<IEnumerable<Question>> FindByTopicAsync([Required] FindByTopicParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);
            
            var questions = (from t in _repository.Questions where t.TopicId == parameters.TopicId select t).ToList();

            // TODO: Implement find and return all questions by selected topid id

            return questions;
        }
    }
}
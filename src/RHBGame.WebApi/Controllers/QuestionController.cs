using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/questions")]
    public class QuestionController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public QuestionController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [Route("list"), HttpGet]
        public async Task<IEnumerable<Question>> ListAsync(String authToken)
        {
            // Check if user is authenticated
            var player = await _authentication.AuthenticateAsync(authToken);
            // Returns list of Questions
            return await _repository.Questions.ToListAsync();
        }
    }
}

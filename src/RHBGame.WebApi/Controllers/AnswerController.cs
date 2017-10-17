using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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

        [Route("list"), HttpPost]
        public async Task<IEnumerable<Answer>> ListAsync([Required] ListParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            return await _repository.Answers.ToListAsync();
        }

        [Route("create"), HttpPost]
        public Task<Answer> CreateAsync([Required] CreateParams parameters)
        {
            throw new NotImplementedException();
        }
    }
}

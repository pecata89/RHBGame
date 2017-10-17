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

        [Route("list"), HttpGet]
        public async Task<IEnumerable<Answer>> ListAsync()
        {
            return await _repository.Answers.ToListAsync();
        }

        [Route("add"), HttpPost]
        public Task<Answer> AddAsync([Required] AddParams parameters)
        {
            throw new NotImplementedException();
        }
    }
}

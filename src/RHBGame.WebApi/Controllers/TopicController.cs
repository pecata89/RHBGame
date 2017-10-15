using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/topics")]
    public class TopicController : ApiController
    {
        private readonly RHBGameRepository _repository;

        public TopicController(RHBGameRepository repository)
        {
            _repository = repository;
        }

        [Route("list"), HttpGet]
        public async Task<IEnumerable<Topic>> ListAsync()
        {
            return await _repository.Topics.ToListAsync();
        }

        [Route("list/{id}"), HttpGet]
        public async Task<Topic> GetTopicAsync(Int32 id)
        {
            return await _repository.Topics.FindAsync(id);
        }
    }
}

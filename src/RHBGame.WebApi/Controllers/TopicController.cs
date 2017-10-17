using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/topics")]
    public sealed partial class TopicController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public TopicController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }
        
        [Route("list"), HttpGet]
        public async Task<IEnumerable<Topic>> ListAsync()
        {
            return await _repository.Topics.ToListAsync();
        }
        
        [Route("findbyid"), HttpPost]
        public async Task<Topic> FindByIdAsync([Required] FindByIdParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            return await _repository.Topics.FindAsync(parameters.TopicId);
        }
    }
}

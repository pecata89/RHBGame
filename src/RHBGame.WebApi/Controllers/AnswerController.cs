using System;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/answers")]
    public class AnswerController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public AnswerController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }


        public async Task Test(String authToken, Int32 data)
        {
            var player = await _authentication.AuthenticateAsync(authToken);

        }
    }
}

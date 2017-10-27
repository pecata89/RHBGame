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
    [RoutePrefix("api/comments")]
    public sealed partial class CommentController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public CommentController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [Route("list"), HttpPost]
        public async Task<IEnumerable<Comment>> ListAsync([Required] ListParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);
            
            return await _repository.Comments.ToListAsync();
        }

        // List of comments shown by selected answer
        [Route("findbyanswer"), HttpPost]
        public async Task<IEnumerable<Comment>> FindByAnswerAsync([Required] FindByAnswerParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            if (!await _repository.Answers.AnyAsync(x => x.Id == parameters.AnswerId))
            {
                throw new SystemException("Question doesn't exist.");
            }

            // Select all the comments for the provided answer
            return await _repository.Comments.Where(x => x.AnswerId == parameters.AnswerId).ToListAsync();
        }

        [Route("create"), HttpPost]
        public async Task CreateAsync([Required] CreateParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            var comment = new Comment()
            {
                Text = parameters.Comment,
                AnswerId = parameters.AnswerId,
                //PlayerId = parameters.PlayerId,
                Created = DateTime.UtcNow
            };

            _repository.Comments.Add(comment);

            await _repository.SaveChangesAsync();
        }

        [Route("update"), HttpPost]
        public async Task UpdateAsync([Required] UpdateParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            var comment = await _repository.Comments.FindAsync(parameters.CommentId);

            // Pass the new comment to the object
            comment.Text = parameters.Comment;

            await _repository.SaveChangesAsync();
        }

        [Route("findbyid"), HttpPost]
        public async Task<Comment> FindByIdAsync([Required] FindByIdParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            return await _repository.Comments.FindAsync(parameters.CommentId);
        }

        [Route("remove"), HttpPost]
        public async Task RemoveAsync([Required] RemoveParams parameters)
        {
            // Check if player is authenticated
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            var comment = await _repository.Comments.FindAsync(parameters.CommentId);

            _repository.Comments.Remove(comment);

            await _repository.SaveChangesAsync();
        }
    }
}

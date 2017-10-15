using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;
using RHBGame.WebApi.Contracts;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix( "api/players" )]
    public sealed class PlayerController : ApiController
    {
        private readonly RHBGameRepository _repository;


        public PlayerController(RHBGameRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Authenticates the player using it's username and password. If the authentication is successful
        /// the method will return a security token that must be included in all other API calls that require
        /// the caller to be authenticated. If the authentication fails the method will return null.
        /// </summary>
        [Route("login"), HttpPost]
        public async Task<String> LoginAsync(String username, String password)
        {
            var player = await _repository.Players.FirstAsync(x => x.Username == username);

            if (player != null && PasswordHelper.CheckHash(password, player.PasswordHash, player.PasswordSalt))
            {
                // TADA, the player's username and password are correct. We need to create a session token (security)
                // that we will send the to the player so when he/she communicates later on with the API
                // we will know who he or she is.
                var randomBytes = new Byte[20];

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(randomBytes);
                }

                var currentTime = DateTime.UtcNow;
                var session = new Session
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Created = currentTime,
                    LastActivity = currentTime,
                    PlayerId = player.Id
                };

                _repository.Sessions.Add(session);

                await _repository.SaveChangesAsync();

                // Returns the token of the logged user
                // This way we will know who makes the requests
                return session.Token;
            }

            // Authentication failed, return null as to the specification
            return null;
        }


        [Route( "create" ), HttpPost]
        public async Task CreateAsync([Required]PlayerCreateInfo info)
        {
            // Here the object of the new player is created
            
            // Where to add validation check?
            var player = new Player
            {
                Name = info.Name,
                PasswordSalt = PasswordHelper.CreateRandomSalt(16),
                Username = info.Username,
                Email = info.Email,
                Gender = info.Gender,
                Birthdate = info.BirthDate,
                Created = DateTime.UtcNow
            };

            // Create random salt and hash for the password (we are not storing it in plain text)
            player.PasswordHash = PasswordHelper.ComputeHash(info.Password, player.PasswordSalt);

            // Check for username duplication
            var duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Username == info.Username);

            if (duplicate != null)
            {
                // We have a duplicate
                throw new SystemException("The provided username already exists.");
            }

            // We check for email duplication
            duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Email == info.Email);

            if (duplicate != null)
            {
                throw new SystemException("The provided email already exists.");
            }

            _repository.Players.Add(player);

            await _repository.SaveChangesAsync();
        }
    }
}
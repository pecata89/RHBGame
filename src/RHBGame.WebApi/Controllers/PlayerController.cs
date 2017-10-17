using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    [RoutePrefix("api/players")]
    public sealed partial class PlayerController : ApiController
    {
        private readonly RHBGameRepository _repository;
        private readonly AuthenticationHelper _authentication;

        public PlayerController(RHBGameRepository repository, AuthenticationHelper authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        /// <summary>
        /// Authenticates the player using it's username and password. If the authentication is successful
        /// the method will return a security token that must be included in all other API calls that require
        /// the caller to be authenticated. If the authentication fails the method will return null.
        /// </summary>
        [Route("login"), HttpPost]
        public async Task<String> LoginAsync([Required] LoginParams parameters)
        {
            var player = await _repository.Players.FirstAsync(x => x.Username == parameters.Username);

            if (player != null && PasswordHelper.CheckHash(parameters.Password, player.PasswordHash, player.PasswordSalt))
            {
                // The player's username and password are correct. We need to create a session token (security)
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

                // Check if user with that id already have a session.
                // To avoid duplication of session autentication token.
                var duplication = await _repository.Sessions.FirstOrDefaultAsync(x => x.Player.Id == player.Id);

                // Check if the player that wants to login have session, let him login again.
                // Update session token, created and last activity properties.
                   
                // If the player is different, redirect to login page.
                // Check that the session token and player id properties are not in the database.

                // If there is a player with the found id
                if (duplication != null)
                {
                    throw new SystemException("This player already have a session.");
                }

                _repository.Sessions.Add(session);

                await _repository.SaveChangesAsync();

                // Returns the token of the logged user
                // This way we will know who makes the requests
                return session.Token;
            }

            // Authentication failed, return null as to the specification
            return null;
        }

        [Route("update"), HttpPut]
        public async Task UpdateAsync([Required] UpdateParams parameters)
        {
            await _authentication.AuthenticateAsync(parameters.AuthToken);

            var player = await _repository.Players.FirstAsync(x => x.Id == parameters.PlayerId);
            
            player.Name = parameters.Name;
            player.Gender = parameters.Gender;
            player.Birthdate = parameters.Birthdate;
            player.Edited = DateTime.UtcNow;
            
            // The sql statement is being executed
            await _repository.SaveChangesAsync();
        }

        [Route("create"), HttpPost]
        public async Task CreateAsync([Required] CreateParams parameters)
        {
            // Create new Player object
            var player = new Player
            {
                Name = parameters.Name,
                Username = parameters.Username,
                PasswordSalt = PasswordHelper.CreateRandomSalt(16),
                Email = parameters.Email,
                Gender = parameters.Gender,
                Birthdate = parameters.Birthdate,
                Created = DateTime.UtcNow
            };

            // Create random salt and hash for the password (we are not storing it in plain text)
            player.PasswordHash = PasswordHelper.ComputeHash(parameters.Password, player.PasswordSalt);

            // Check for username duplication
            var duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Username == parameters.Username);

            if (duplicate != null)
            {
                throw new SystemException("The provided username already exists.");
            }

            // Check for email duplication
            duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Email == parameters.Email);

            if (duplicate != null)
            {
                throw new SystemException("The provided email already exists.");
            }

            // Populates the Players table in the database
            _repository.Players.Add(player);

            await _repository.SaveChangesAsync();
        }
    }
}
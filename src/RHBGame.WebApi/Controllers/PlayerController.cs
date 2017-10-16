using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
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
        public async Task<String> LoginAsync([Required]PlayerLoginInfo login)
        {
            var player = await _repository.Players.FirstAsync(x => x.Username == login.Username);

            if (player != null && PasswordHelper.CheckHash(login.Password, player.PasswordHash, player.PasswordSalt))
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

                // Check if user with that username is already in the database.
                // To avoid duplication of session autentication token.
                var duplication = await _repository.Sessions.FirstAsync(x => x.Player.Id == session.PlayerId);

                // If there is a player with the found id
                if (duplication != null)
                {
                    return "A player with id already have a session.";
                }

                _repository.Sessions.Add(session);

                await _repository.SaveChangesAsync();

                // Returns the token of the logged user
                // This way we will know who makes the requests
                return session.Token;
            }

            // Authentication failed, return null as to the specification
            return "Username not found.";
        }

        [Route("update"), HttpPost]
        public async Task<IHttpActionResult> EditAsync([Required]PlayerUpdateInfo update)
        {
            var authentication = await _authentication.AuthenticateAsync(update.AuthToken);
            var player = await _repository.Players.FirstAsync(x => x.Id == update.PlayerId);

            if (player == null)
            {
                Json("There was an error while updating player information.");
            }
            
            player.Name = update.Name;
            player.Gender = update.Gender;
            player.Birthdate = update.Birthdate;
            player.Edited = DateTime.UtcNow;

            // Update the database
            _repository.Entry(player).State = EntityState.Modified;

            await _repository.SaveChangesAsync();
            
            return Json(update.Name + "'s info was edited.");
        }
        
        [Route("create"), HttpPost]
        public async Task CreateAsync([Required]PlayerCreateInfo info)
        {
            // Create new Player object
            var player = new Player
            {
                Name = info.Name,
                Username = info.Username,
                PasswordSalt = PasswordHelper.CreateRandomSalt(16),
                Email = info.Email,
                Gender = info.Gender,
                Birthdate = info.Birthdate,
                Created = DateTime.UtcNow
            };

            // Create random salt and hash for the password (we are not storing it in plain text)
            player.PasswordHash = PasswordHelper.ComputeHash(info.Password, player.PasswordSalt);

            // Check for username duplication
            var duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Username == info.Username);

            if (duplicate != null)
            {
                // Throw json exeption
                Json("The provided username already exists.");
            }

            // Check for email duplication
            duplicate = await _repository.Players.FirstOrDefaultAsync(x => x.Email == info.Email);

            if (duplicate != null)
            {
                Json("The provided email already exists.");
            }

            // Populates the Players table in the database
            _repository.Players.Add(player);

            await _repository.SaveChangesAsync();

            Json(info.Username + " was successfully created.");
        }
    }
}
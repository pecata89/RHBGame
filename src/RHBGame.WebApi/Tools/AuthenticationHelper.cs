using System;
using System.Data.Entity;
using System.Threading.Tasks;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi
{
    public sealed class AuthenticationHelper
    {
        private readonly RHBGameRepository _repository;
        private readonly TimeSpan _timeOut = TimeSpan.FromMinutes(20);

        public AuthenticationHelper(RHBGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> AuthenticateAsync(String token)
        {
            var session = await _repository.Sessions
                .Include(x => x.Player)
                .FirstOrDefaultAsync(x => x.Token == token);

            // Check if the session is expired
            if (session == null || DateTime.UtcNow - session.LastActivity > _timeOut)
            {
                throw new SystemException("The provided session token is expired or invalid.");
            }

            // Update the last activity
            session.LastActivity = DateTime.UtcNow;
            await _repository.SaveChangesAsync();

            return session.Player;
        }
    }
}
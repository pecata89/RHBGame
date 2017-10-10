using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using RHBGame.Data;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Controllers
{
    public sealed class PlayerController : ApiController
    {
        private readonly RHBGameRepository _repository;


        public PlayerController(RHBGameRepository repository)
        {
            _repository = repository;
        }


        public Task<List<Player>> GetAllPlayersAsync() => _repository.Players.ToListAsync();


        public Task<Player> GetPlayerByIDAsync(Int32 id) => _repository.Players.FirstAsync( x => x.PlayerId == id );


        public async Task InsertPlayerAsync(Player player)
        {
            _repository.Players.Add(player);

            await _repository.SaveChangesAsync();
        }
    }
}

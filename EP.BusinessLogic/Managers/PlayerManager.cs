using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using System.Collections.Generic;

namespace EP.BusinessLogic.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private readonly IPlayerService _playerService;

        public PlayerManager(IPlayerService playerService)
        {
            _playerService = playerService;
        }       

        public List<PlayersViewModel> GetPlayersList()
        {
            return _playerService.GetPlayersList();
        }        

        public PlayerViewModel GetPlayer(int id, int userId)
        {
            var player = _playerService.Get(g => g.Id == id);
            var result = new PlayerViewModel();

            if (player != null)
            {
                var mapper = Mappings.GetMapper();
                result = mapper.Map<PlayerViewModel>(player);
            }

            return result;
        }

        public PlayersViewModel GetUserPlayer(int userId)
        {
            return _playerService.GetUserPlayer(userId);
        }

        public bool Assign(int id, int userId)
        {
            return _playerService.Assign(id, userId);
        }
    }

    public interface IPlayerManager
    {        
        PlayerViewModel GetPlayer(int id, int userId);
        List<PlayersViewModel> GetPlayersList();       
        PlayersViewModel GetUserPlayer(int userId);
        bool Assign(int id, int userId);
    }
}

using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class PlayerService : BaseService<Player>, IPlayerService
    {
        public PlayerService(IDataContext dataContext) : base(dataContext)
        {
        }
        
        public List<PlayersViewModel> GetPlayersList()
        {
            return Dbset.Select(s => new PlayersViewModel
            {
                Id = s.Id,
                FullName = s.FirstName + " " + s.LastName,
                HasOwner = s.UserId.HasValue,
                Discipline = s.MainDiscipline
            }).ToList();
        }

        public PlayersViewModel GetUserPlayer(int userId)
        {
            var player = Dbset.FirstOrDefault(f => f.UserId == userId);

            if (player == null)
                return new PlayersViewModel();

            return new PlayersViewModel
            {
                Discipline = player.MainDiscipline,
                FullName = player.FirstName + " " + player.LastName,
                HasOwner = true,
                Id = player.Id
            };
        }

        public bool Assign(int id, int userId)
        {
            if (!Dbset.Any(a => a.UserId == userId) && !Dbset.Any(a => a.Id == id && a.UserId.HasValue))
            {
                if (!DataContext.AssignPlayers.Any(a => a.UserId == userId))
                {
                    DataContext.AssignPlayers.Add(new AssignPlayer
                    {
                        PlayerId = id,
                        UserId = userId,
                        RequestDate = DateTime.Now
                    });

                    DataContext.SaveChanges();

                    return true;
                }
            }

            return false;
        }
    }

    public interface IPlayerService : IService<Player>
    {        
        List<PlayersViewModel> GetPlayersList();
        PlayersViewModel GetUserPlayer(int userId);
        bool Assign(int id, int userId);
    }
}

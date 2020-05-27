using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{   
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        public UserProfileService(IDataContext dataContext) : base(dataContext)
        {
        }        

        public List<UserProfile> GetOtherUserForFriendShip(List<int> ids)
        {
            return Dbset.Where(w => !ids.Contains(w.UserId)).ToList();
        }

        public InformerData GetInformerData(int userId)
        {
            var mapper = Mappings.GetMapper();
            var tournament = DataContext.Tournaments.OrderByDescending(o => o.TournamentDate).FirstOrDefault();

            var result = new InformerData
            {
                LastTeams = DataContext.Teams.OrderByDescending(o => o.Id)
                    .Take(Constants.DEFAULT_FEED_ITEMS_COUNT).Select(s => new TeamsViewModel
                    {
                        Id = s.Id,
                        LogoUrl = s.LogoUrl,
                        Name = s.Name,
                        Discipline = s.DisciplineId
                    }).ToList(),
                Requests = DataContext.RequestToFriends.Count(c => c.WithID == userId)
            };

            if (tournament != null)
            {
                result.Tournament = mapper.Map<ActiveTournamentsView>(tournament);
            }

            return result;
        }

        public bool CheckUsersEntity(int[] usersId)
        {
            return Dbset.Any(a => !usersId.Contains(a.UserId));
        }

        public void AddCode(int userId, string code)
        {
            DataContext.ActivationCodes.Add(new ActivationCode
            {
                Code = code,
                UserId = userId,
                SendingDate = DateTime.Now
            });

            DataContext.SaveChanges();
        }
        
        public bool ActivateUser(int userId, string code)
        {
            var entity = DataContext.ActivationCodes.FirstOrDefault(a => a.UserId == userId && a.Code == code);

            if (entity != null)
            {
                DataContext.ActivationCodes.Remove(entity);
                var user = Get(g => g.UserId == userId);

                if (user != null)
                {
                    user.IsActive = true;
                    Update(user);

                    return true;
                }
            }

            return false;
        }

        public bool CodeWasSent(int userId)
        {
            return DataContext.ActivationCodes.Any(a => a.UserId == userId);
        }
    }

    public interface IUserProfileService : IService<UserProfile>
    {
        List<UserProfile> GetOtherUserForFriendShip(List<int> ids);
        InformerData GetInformerData(int userId);
        bool CheckUsersEntity(int[] usersId);
        void AddCode(int userId, string code);
        bool ActivateUser(int userId, string code);
        bool CodeWasSent(int userId);
    }
}

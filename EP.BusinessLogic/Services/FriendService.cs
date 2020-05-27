using EP.EntityData.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public interface IFriendService : IService<Friend>
    {
        void AcceptFriendShip(int userId, int otherUserid);
        bool IsAlreadyFriend(int userId, int otherUserid);
        void RemoveFriendShip(int userId, int friendId);
        List<UserProfile> GetFriends(int id);
    }

    public class FriendService : BaseService<Friend>, IFriendService
    {
        public FriendService(IDataContext dataContext) : base(dataContext)
        {
        }

        public void AcceptFriendShip(int userId, int friendId)
        {
            if (!Dbset.Any(a => (a.WhoID == userId && a.WithID == friendId) && (a.WhoID == friendId && a.WithID == userId)))
            {
                AddRange(new List<Friend>() {
                    new Friend { WhoID = userId, WithID = friendId, StartFriend = DateTime.Now },
                    new Friend { WhoID = friendId, WithID = userId, StartFriend = DateTime.Now }
                });
            }
        }

        public bool IsAlreadyFriend(int userId, int otherUserid)
        {
            return Dbset.Any(a => (a.WhoID == userId && a.WithID == otherUserid) && (a.WhoID == otherUserid && a.WithID == userId));
        }

        public void RemoveFriendShip(int userId, int friendId)
        {
            Delete(Dbset.Where(a => (a.WhoID == userId && a.WithID == friendId) || (a.WhoID == friendId && a.WithID == userId)));
        }

        public List<UserProfile> GetFriends(int id)
        {
            return Dbset.Where(w => w.WithID == id).Select(s => s.Who).ToList();
        }
    }
}

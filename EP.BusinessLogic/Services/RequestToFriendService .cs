using EP.EntityData.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public interface IRequestToFriendService : IService<RequestToFriend>
    {
        bool RequestToFriend(int userId, int friendId);
        bool IsRequested(int userId, int id);
        void AbortRequest(int userId, int friendId);
        void CleanRequest(int userId, int friendId);
        List<UserProfile> GetRequestFromUser(int Id);
        List<UserProfile> GetRequestForUser(int Id);
    }

    public class RequestToFriendService : BaseService<RequestToFriend>, IRequestToFriendService
    {
        public RequestToFriendService(IDataContext dataContext) : base(dataContext)
        {
        }

        public bool RequestToFriend(int userId, int friendId)
        {
            if (!Dbset.Any(a => (a.WhoID == userId && a.WithID == friendId) || (a.WhoID == friendId && a.WithID == userId)))
            {
                Add(new RequestToFriend { WhoID = userId, WithID = friendId, RequestDate = DateTime.Now });

                return true;
            }

            return false;
        }

        public bool IsRequested(int userId, int id)
        {
            return Dbset.Any(a => a.WhoID == userId && a.WithID == id);
        }

        public void AbortRequest(int userId, int friendId)
        {
            Delete(Dbset.Where(w => w.WhoID == userId && w.WithID == friendId));
        }

        public void CleanRequest(int userId, int friendId)
        {
            Delete(Dbset.Where(w => w.WhoID == friendId && w.WithID == userId));
        }

        public List<UserProfile> GetRequestFromUser(int Id)
        {
            return Dbset.Where(w => w.WhoID == Id).Select(s => s.With).ToList();
        }

        public List<UserProfile> GetRequestForUser(int Id)
        {
            return Dbset.Where(w => w.WithID == Id).Select(s => s.Who).ToList();
        }
    }
}

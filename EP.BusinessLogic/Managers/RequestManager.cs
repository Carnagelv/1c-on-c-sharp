using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using System.Collections.Generic;

namespace EP.BusinessLogic.Managers
{
    public interface IRequestManager
    {
        List<UserProfile> GetRequestFromUser(int Id);
        List<UserProfile> GetRequestForUser(int Id);
    }

    public class RequestManager : IRequestManager
    {
        private readonly IRequestToFriendService _requestToFriendService;

        public RequestManager(IRequestToFriendService requestToFriendService)
        {
            _requestToFriendService = requestToFriendService;
        }

        public List<UserProfile> GetRequestFromUser(int Id)
        {
            return _requestToFriendService.GetRequestFromUser(Id);
        }

        public List<UserProfile> GetRequestForUser(int Id)
        {
            return _requestToFriendService.GetRequestForUser(Id);
        }
    }
}

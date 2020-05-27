FriendFactory.$inject = ['$http'];

function FriendFactory($http) {
    var factory = {};
    
    factory.getFriends = function (id) {
        return $http.get('/Friend/Get', { params: { Id: id } });
    };

    factory.deleteFromFriend = function (id) {
        return $http.post('/Friend/DeleteFromFriend', { id });
    };

    factory.requestToFriend = function (id) {
        return $http.post('/Friend/RequestToFriend', { id });
    };

    factory.abortRequest = function (id) {
        return $http.post('/Friend/AbortRequest', { id });
    };

    factory.acceptRequest = function (id) {
        return $http.post('/Friend/AcceptRequest', { id });
    };

    factory.declineRequest = function (id) {
        return $http.post('/Friend/DeclineRequest', { id });
    };  

    return factory;
}
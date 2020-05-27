PlayerFactory.$inject = ['$http'];

function PlayerFactory($http) {
    var factory = {};  

    factory.getPlayer = function (id) {
        return $http.get('/Player/Get', { params: { id } });
    };

    factory.getPlayers = function () {
        return $http.get('/Player/GetList');
    };

    factory.assignPlayer = function (id) {
        return $http.post('/Player/Assign', { id });
    };

    return factory;
}
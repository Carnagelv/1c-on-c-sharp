TournamentFactory.$inject = ['$http'];

function TournamentFactory($http) {
    var factory = {};      

    factory.getTournaments = function () {
        return $http.get('/Tournament/GetTournaments');
    };

    factory.getTournamentById = function (id) {
        return $http.get('/Tournament/GetTournament', { params: { id }});
    };

    factory.takePartIn = function (id) {
        return $http.post('/Tournament/TakePartIn', { id });
    };

    return factory;
}
MatchFactory.$inject = ['$http'];

function MatchFactory($http) {
    var factory = {};   

    factory.getMatchById = function (id) {
        return $http.get('/Match/Get', { params: { id } });
    };

    factory.getMatches = function (filters) {
        cfg = {
            params: {
                Season: filters.season,
                Tournament: filters.tournament,
                Team: filters.team,
                IncludeItems: filters.includeItems
            }
        };

        return $http.get('/Match/GetList', cfg);
    };

    return factory;
}
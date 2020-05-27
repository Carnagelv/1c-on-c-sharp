TeamFactory.$inject = ['$http'];

function TeamFactory($http) {
    var factory = {};
    
    factory.checkTitle = function (title, discipline, id) {
        return $http.get('/Team/CheckTitle', { params: { title, discipline, id } });
    };

    factory.getDisciplines = function () {
        return $http.get('/Team/GetDisciplines');
    };    

    factory.getTeamById = function (id) {
        return $http.get('/Team/Get', { params: { id } });
    };

    factory.getTeams = function () {
        return $http.get('/Team/GetTeams');
    };

    factory.createTeam = function (teamData) {
        return $http.post('/Team/Create', teamData);
    };

    factory.getTeamForEdit = function (id) {
        return $http.get('/Team/GetTeamForEdit', { params: { id } });
    };

    factory.editTeam = function (teamData) {
        return $http.post('/Team/Edit', teamData);
    };

    factory.assignTeam = function (id) {
        return $http.post('/Team/AssignTeam', { teamId: id });
    };

    return factory;
}
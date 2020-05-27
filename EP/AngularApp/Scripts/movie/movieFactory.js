MovieFactory.$inject = ['$http'];

function MovieFactory($http) {
    var factory = {};

    factory.getMovies = function (seasons, firstEnter) {
        return $http.get('/Movie/GetList', { params: { seasons, firstEnter } });
    };

    return factory;
}
NewsFactory.$inject = ['$http'];

function NewsFactory($http) {
    var factory = {};
    
    factory.getNews = function (discipline) {
        return $http.get('/News/GetNews', { params: { discipline } });
    };

    factory.toggleLike = function (id) {
        return $http.post('/News/ToggleLike', { id });
    };

    factory.getNewsById = function (id) {
        return $http.get('/News/GetNewsById', { params: { id } });
    };

    factory.addComment = function (text, id) {
        return $http.post('/News/AddComment', { text, id });
    };

    factory.deleteComment = function (id) {
        return $http.post('/News/DeleteComment', { id });
    };

    return factory;
}
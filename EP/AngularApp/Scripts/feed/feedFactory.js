FeedFactory.$inject = ['$http'];

function FeedFactory($http) {
    var factory = {};
    
    factory.getFeed = function () {
        return $http.get('/Feed/Get');
    };

    return factory;
}
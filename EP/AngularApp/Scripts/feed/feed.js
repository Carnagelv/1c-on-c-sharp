(function () {
    angular.module('EP.Feed', ['EP'])
        .controller('feedCtrl', FeedCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory('feedFactory', FeedFactory);
})();
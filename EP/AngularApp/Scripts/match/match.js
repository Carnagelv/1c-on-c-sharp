(function () {
    angular.module('EP.Match', ['EP'])
        .controller('matchCtrl', MatchCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory('matchFactory', MatchFactory);
})();
(function () {
    angular.module('EP.Team', ['EP'])
        .controller('teamCtrl', TeamCtrl)
        .controller('teamModalCtrl', TeamModalCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory('teamFactory', TeamFactory)
        .factory('mainFactory', MainFactory);
})();
(function () {
    angular.module('EP.Movie', ['EP'])
        .controller('movieCtrl', MovieCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('movieFactory', MovieFactory)
        .factory('mainFactory', MainFactory);
})();
(function () {
    angular.module('EP.Player', ['EP'])
        .controller('playerCtrl', PlayerCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('playerFactory', PlayerFactory)
        .factory('mainFactory', MainFactory);
})();
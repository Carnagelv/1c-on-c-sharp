(function () {
    angular.module('EP.Message', ['EP'])
        .controller('messageCtrl', MessageCtrl)
        .controller('mainCtrl', MainCtrl)
        .controller('messageModalCtrl', MessageModalCtrl)
        .factory('mainFactory', MainFactory)
        .factory('messageFactory', MessageFactory);
})();
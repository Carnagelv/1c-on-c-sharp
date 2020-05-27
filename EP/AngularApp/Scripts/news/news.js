(function () {
    angular.module('EP.News', ['EP'])
        .controller('newsCtrl', NewsCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory("newsFactory", NewsFactory)
        .factory("mainFactory", MainFactory);
})();
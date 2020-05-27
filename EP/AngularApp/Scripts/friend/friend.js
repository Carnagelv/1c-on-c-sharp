(function () {
    angular.module('EP.Friend', ['EP'])
        .controller('friendCtrl', FriendCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory("friendFactory", FriendFactory)
        .factory("mainFactory", MainFactory);
})();
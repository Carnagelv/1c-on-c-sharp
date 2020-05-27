(function () {
    angular.module('EP.User', ['EP', 'ngFileUpload'])
        .controller('userCtrl', UserCtrl)
        .controller('mainModalCtrl', MainModalCtrl)
        .controller('friendCtrl', FriendCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory('userFactory', UserFactory)
        .factory('friendFactory', FriendFactory)
        .factory('mainFactory', MainFactory);
})();
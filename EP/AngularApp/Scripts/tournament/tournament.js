(function () {
    angular.module('EP.Tournament', ['EP'])
        .controller('tournamentCtrl', TournamentCtrl)
        .controller('mainCtrl', MainCtrl)
        .factory('mainFactory', MainFactory)
        .factory('tournamentFactory', TournamentFactory);
})();
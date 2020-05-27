MatchCtrl.$inject = ['$scope', 'matchFactory'];

function MatchCtrl($scope, matchFactory) {
    $scope.sections = {
        list: 1,
        details: 2
    };
    $scope.filters = {
        season: 0,
        tournament: 0,
        team: 0,
        includeItems: false
    };
    $scope.filterItems = {
        seasons: [],
        tournaments: [],
        teams: []
    };

    $scope.matches = [];
    $scope.selectedMatch = {};
    $scope.isLoading = false;
    $scope.activeSection = $scope.sections.list;

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getMatchById(id)
            : getMatches(true);
    }

    function getMatches(withFilterItems) {
        $scope.filters.includeItems = withFilterItems;
        $scope.isLoading = true;
        matchFactory.getMatches($scope.filters).then(function (response) {
            $scope.matches = response.data.result.Matches;

            if (withFilterItems) {
                $scope.filterItems.seasons = response.data.result.FilterItems.Seasons;
                $scope.filterItems.tournaments = response.data.result.FilterItems.Tournaments;
                $scope.filterItems.teams = response.data.result.FilterItems.Teams;
            }

            $scope.isLoading = false;
        });
    }

    $scope.updateMatches = function () {
        getMatches(false);
    };

    $scope.getMatchById = function (id) {
        $scope.isLoading = true;
        matchFactory.getMatchById(id).function(function (response) {
            $scope.selectedMatch = response.data.match;
            $scope.isLoading = false;
        });
    };

    $scope.stepBack = function () {
        if (!response.data.result.FilterItems.Seasons.length) {
            getMatches(true);
        }
    };

    init();
}
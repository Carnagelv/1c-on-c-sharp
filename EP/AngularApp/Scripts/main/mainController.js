MainCtrl.$inject = ['$scope', 'mainFactory', 'disciplineEnum'];

function MainCtrl($scope, mainFactory, disciplineEnum) {
    $scope.informer = {
        lastTeams: [],
        tournament: {}
    };
    $scope.disciplines = disciplineEnum;
    $scope.informerLoading = true;

    function init() {
        mainFactory.getInformers().then(function (response) {
            $scope.informer.lastTeams = response.data.lastTeams;
            $scope.informer.tournament = response.data.tournament;
            updateRequestsCount(response.data.requestsToFriend);
            $scope.informerLoading = false;
        });
    }

    function updateRequestsCount(count) {
        if (count > 0) {
            $('#requestsCount').text('+' + count);
        }
    }

    $scope.getTeamById = function (id) {
        window.location.href = '/Team?Id=' + id;
    };

    init();
}
TournamentCtrl.$inject = ['$scope', 'tournamentFactory', 'disciplineEnum', 'mainFactory'];

function TournamentCtrl($scope, tournamentFactory, disciplineEnum, mainFactory) {
    $scope.tournaments = {
        inActive: [],
        active: []
    };
    $scope.sections = {
        list: 1,
        details: 2
    };
    $scope.isLoading = true;
    $scope.activeSection = $scope.sections.list;
    $scope.selectedTournament = null;
    $scope.disciplines = disciplineEnum;

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getTournamentById(id)
            : getTournaments();
    }

    function getTournaments() {
        $scope.activeSection = $scope.sections.list;
        $scope.isLoading = true;        
        tournamentFactory.getTournaments().then(function (response) {
            $scope.tournaments.inActive = response.data.tournaments.InActive;
            $scope.tournaments.active = response.data.tournaments.Active;
            $scope.isLoading = false;
        });
    }

    $scope.getTournamentById = function (id) {
        $scope.selectedTournament = null;
        $scope.activeSection = $scope.sections.details;
        $scope.isLoading = true;        
        tournamentFactory.getTournamentById(id).then(function (response) {
            $scope.selectedTournament = response.data.tournament;
            $scope.isLoading = false;
        });
    };

    $scope.returnBackToList = function () {
        if ($scope.tournaments.inActive.length === 0) {
            getTournaments();
        }

        $scope.activeSection = $scope.sections.list;
    };

    $scope.takePartIn = function () {
        tournamentFactory.takePartIn($scope.selectedTournament.Id).then(function (response) {           
            mainFactory.showNotify(response.data.result.Message);

            if (response.data.result.Success) {
                $scope.selectedTournament.TeamCount++;
                $scope.selectedTournament.Participants.push(response.data.result.Participant);

                for (var i = 0; i < $scope.tournaments.active.length; i++) {
                    if ($scope.tournaments.active[0].Id === $scope.selectedTournament.Id) {
                        $scope.tournaments.active[0].TeamCount++; 
                    }
                }
            }
        });
    };

    $scope.showRoster = function (mouseOver, number) {
        if (mouseOver) {
            $($('.team-roster')[number]).css('display', 'block');
            $($('.team-logo')[number]).css('opacity', '.1');
        } else {
            $($('.team-roster')[number]).css('display', 'none');
            $($('.team-logo')[number]).css('opacity', '1');
        }
    };

    init();
}
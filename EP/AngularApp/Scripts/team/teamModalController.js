TeamModalCtrl.$inject = ['$scope', '$mdDialog', 'mainFactory', 'teamFactory', 'modalData'];

function TeamModalCtrl($scope, $mdDialog, mainFactory, teamFactory, modalData) {

    $scope.teamData = modalData.teamData || {
        id: 0,
        title: '',
        discipline: 0
    };

    $scope.disciplines = modalData.disciplines;
    $scope.notUniqTitle = false;

    function changeName(id, title, teams) {
        teams = teams || [];

        for (var i = 0; i < teams.length; i++) {
            if (teams[i].Id === id) {
                teams[i].Name = title;
                break;
            }
        }
    }

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.createTeam = function () {
        teamFactory.checkTitle($scope.teamData.title, $scope.teamData.discipline).then(function (response) {
            $scope.notUniqTitle = response.data.success;

            if (!$scope.notUniqTitle) {
                teamFactory.createTeam($scope.teamData).then(function (response) {
                    if (response.data.success && response.data.team.Id !== 0) {
                        mainFactory.showNotify("Komanda veiksmīgi tika izveidota");
                        modalData.teams.push(response.data.team);
                        modalData.userTeams.push(response.data.team);
                        $mdDialog.cancel();
                    } else {
                        mainFactory.showNotify("Kļūda! Mēģiniet vēl reiz");
                    }
                });
            }
        });
    };

    $scope.editTeam = function () {
        teamFactory.checkTitle($scope.teamData.title, $scope.teamData.discipline, $scope.teamData.id).then(function (response) {
            $scope.notUniqTitle = response.data.success;

            if (!$scope.notUniqTitle) {
                teamFactory.editTeam($scope.teamData).then(function (response) {
                    if (response.data.edited) {
                        mainFactory.showNotify("Komanda veiksmīgi tika rediģēta");
                        changeName($scope.teamData.id, $scope.teamData.title, modalData.teams);
                        changeName($scope.teamData.id, $scope.teamData.title, modalData.userTeams);
                        $mdDialog.cancel();
                    } else {
                        mainFactory.showNotify("Kļūda! Mēģiniet vēl reiz");
                    }
                });
            }
        });
    };
}
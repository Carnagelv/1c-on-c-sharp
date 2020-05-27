PlayerCtrl.$inject = ['$scope', 'mainFactory', 'playerFactory'];

function PlayerCtrl($scope, mainFactory, playerFactory) {
    $scope.sections = {
        list: 1,
        details: 2
    };

    $scope.players = [];
    $scope.userPlayer = {};
    $scope.isLoading = false;
    $scope.selectedPlayer = { Id: 0 };
    $scope.activeSection = $scope.sections.list;

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getPlayerById(id)
            : getPlayers();
    }

    function getPlayers() {
        $scope.isLoading = true;       
        playerFactory.getPlayers().then(function (response) {
            $scope.players = response.data.players;
            $scope.userPlayer = response.data.userPlayer;
            $scope.isLoading = false;
        });
    }

    function getDisciplineName() {
        switch ($scope.selectedPlayer.Discipline) {
            case 1: $scope.selectedPlayer.DisciplineName = 'Futbols';
                break;
            case 2: $scope.selectedPlayer.DisciplineName = 'Basketbols';
                break;
            case 3: $scope.selectedPlayer.DisciplineName = 'KiberSports';
                break;
            default: 
                $scope.selectedPlayer.DisciplineName = 'Futbols';
        }
    }

    $scope.getPlayerById = function (id) {
        $scope.isLoading = true;
        $scope.activeSection = $scope.sections.details;
        playerFactory.getPlayer(id).then(function (response) {
            $scope.selectedPlayer = response.data.player;
            getDisciplineName();
            $scope.isLoading = false;
        });
    };

    $scope.stepBack = function () {
        if ($scope.players.length === 0) {
            getPlayers();
        }

        $scope.activeSection = $scope.sections.list;
    };

    $scope.assignPlayer = function (id) {
        playerFactory.assignPlayer(id).then(function (response) {
            if (response.data.success) {
                $scope.selectedPlayer.CanAssign = false;
                mainFactory.showNotify("Pieteikums nosūtīts");
            } else {
                mainFactory.showNotify("Spēlētāju nedrīkst pievienot");
            }
        });
    };

    init();
}
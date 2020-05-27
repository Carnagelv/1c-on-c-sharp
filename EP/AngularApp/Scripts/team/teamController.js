TeamCtrl.$inject = ['$scope', 'mainFactory', 'teamFactory'];

function TeamCtrl($scope, mainFactory, teamFactory) {
    $scope.sections = {
        list: 1,
        details: 2
    };

    $scope.teams = [];
    $scope.userTeams = [];
    $scope.isLoading = false;
    $scope.selectedTeam = null;
    $scope.activeSection = $scope.sections.list;
    $scope.visiting = [];

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getTeamById(id)
            : getTeams();
    }

    function getTeams() {
        $scope.isLoading = true;       
        teamFactory.getTeams().then(function (response) {
            $scope.teams = response.data.teams;
            $scope.userTeams = response.data.userTeams;
            $scope.isLoading = false;
        });
    }

    function getDisciplineName() {
        switch ($scope.selectedTeam.Discipline) {
            case 1: $scope.selectedTeam.DisciplineName = 'Futbols';
                break;
            case 2: $scope.selectedTeam.DisciplineName = 'Basketbols';
                break;
            case 3: $scope.selectedTeam.DisciplineName = 'KiberSports';
                break;
            default: 
                $scope.selectedTeam.DisciplineName = 'Futbols';
        }
    }

    $scope.createTeam = function () {
        mainFactory.showWait();
        teamFactory.getDisciplines().then(function (response) {
            mainFactory.showModal(TeamModalCtrl, "CreateTeamModal.html", { disciplines: response.data.disciplines, teams: $scope.teams, userTeams: $scope.userTeams });
            mainFactory.hideWait();
        });
    };

    $scope.editTeam = function (id) {
        mainFactory.showWait();
        teamFactory.getTeamForEdit(id).then(function (response) {
            mainFactory.showModal(TeamModalCtrl, "CreateTeamModal.html", {
                teamData: {
                    title: response.data.team.Title,
                    discipline: response.data.team.Discipline.Id,
                    id: response.data.team.Id
                },
                disciplines: [response.data.team.Discipline],
                teams: $scope.teams,
                userTeams: $scope.userTeams
            });
            mainFactory.hideWait();
        });
    };

    $scope.getTeamById = function (id) {
        $scope.isLoading = true;
        $scope.activeSection = $scope.sections.details;
        teamFactory.getTeamById(id).then(function (response) {
            $scope.selectedTeam = response.data.team;
            $scope.visiting = response.data.visiting;
            getDisciplineName();
            $scope.isLoading = false;

            drawTable();
        });
    };

    $scope.stepBack = function () {
        if ($scope.teams.length === 0) {
            getTeams();
        }

        $scope.activeSection = $scope.sections.list;
    };

    $scope.assignTeam = function (id) {
        teamFactory.assignTeam(id).then(function (response) {
            if (response.data.success) {
                $scope.selectedTeam.CanAssign = false;
                mainFactory.showNotify("Pieteikums nosūtīts");
            } else {
                mainFactory.showNotify("Komandu nedrīkst pievienot");
            }
        });
    };

    function drawTable () {
        google.charts.load('current', { 'packages': ['bar'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = [['Gads', 'Sacensību skaits', 'Komandas apmeklējumu skaits']];

            $scope.visiting.forEach(function (visit, i) {
                data.push([visit.Year, visit.TournamentCount, visit.VisitingCount]);
            });

            var options = {
                chart: {
                    title: 'Sacensību apmeklējums',
                    subtitle: 'Sacensību skaits, apmeklējumu skaits: 2016 - 2019'                    
                },
                legend: {
                    textStyle: {
                        fontSize: 12
                    }
                },
                bar: {
                    groupWidth: "50%"
                }
            };

            var chart = new google.charts.Bar(document.getElementById('columnchart'));

            chart.draw(google.visualization.arrayToDataTable(data), google.charts.Bar.convertOptions(options));
        }
    };

    init();
}
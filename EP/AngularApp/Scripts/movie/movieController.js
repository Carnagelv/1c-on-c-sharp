MovieCtrl.$inject = ['$scope', 'mainFactory', 'movieFactory', '$sce'];

function MovieCtrl($scope, mainFactory, movieFactory, $sce) {    
    $scope.movies = [];
    $scope.isLoading = false;
    $scope.movieSeasons = [];
    $scope.selectedSeasons = [];

    function init() {
        getMovies();
    }

    function getMovies() {
        $scope.isLoading = true;
        movieFactory.getMovies($scope.selectedSeasons, $scope.movieSeasons.length === 0).then(function (response) {
            $scope.movies = response.data.movies;

            if ($scope.movieSeasons.length === 0) {
                $scope.movieSeasons = response.data.movieSeasons;
            }

            for (var i = 0; i < $scope.movies.length; i++) {
                $scope.movies[i].Link = $sce.trustAsHtml($scope.movies[i].Link);
            }
            
            $scope.isLoading = false;
        });
    }

    $scope.selectSeason = function () {
        $scope.selectedSeasons = [];

        for (var i = 0; i < $scope.movieSeasons.length; i++) {
            if ($scope.movieSeasons[i].IsSelected) {
                $scope.selectedSeasons.push($scope.movieSeasons[i].Season);
            }
        }

        getMovies();
    };

    init();
}
FeedCtrl.$inject = ['$scope', 'feedFactory'];

function FeedCtrl($scope, feedFactory) {    
    $scope.feed = {
        news: [],
        likes: [],
        comments: [],
        users: []
    };
    $scope.isLoading = false;

    function init() {
        $scope.isLoading = true;
        feedFactory.getFeed().then(function (response) {
            $scope.feed.news = response.data.feed.News || [];
            $scope.feed.likes = response.data.feed.Likes || [];
            $scope.feed.comments = response.data.feed.Comments || [];
            $scope.feed.users = response.data.feed.Users || [];
            $scope.isLoading = false;
        });
    }

    $scope.goToNews= function (id) {
        window.location.href = "/News?Id=" + id;
    };

    $scope.goToUser = function (id) {
        window.location.href = "/User?Id=" + id;
    };

    init();
}
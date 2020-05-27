NewsCtrl.$inject = ['$scope', 'mainFactory', 'newsFactory'];

function NewsCtrl($scope, mainFactory, newsFactory) {
    $scope.categories = {
        football: 1,
        basketball: 2,
        cyberSport: 3,
        beFirst: 4,
        all: 5
    };
    $scope.sections = {
        list: 1,
        details: 2
    };

    $scope.news = [];
    $scope.selectedNews = null;
    $scope.comment = { text: "" };
    $scope.activeSection = $scope.sections.list;
    $scope.isLoading = false;
    $scope.category = $scope.categories.all;
    $scope.currentLanguage = 0;

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getNewsItem(id)
            : getNews();
    }

    function getNews() {
        $scope.isLoading = true;
        newsFactory.getNews($scope.category).then(function (response) {
            $scope.news = response.data.news;
            $scope.isLoading = false;
        });
    }

    $scope.toggleLike = function (item) {        
        item.likeCount = item.IsYouLiked ? item.LikeCount-- : item.LikeCount++;
        item.IsYouLiked = !item.IsYouLiked;

        newsFactory.toggleLike(item.Id).then(function () {
            var message = item.IsYouLiked ? "Jums patīk ziņa '" : "Jums vairs nepatīk ziņa '";
            mainFactory.showNotify(message + item.Title + "'");
        });
    };

    $scope.getNewsItem = function (id) {
        $scope.activeSection = $scope.sections.details;
        $scope.isLoading = true;

        newsFactory.getNewsById(id).then(function (response) {
            $scope.selectedNews = response.data.selectedNews;
            $scope.isLoading = false;
        });
    };

    $scope.addComment = function () {
        newsFactory.addComment($scope.comment.text, $scope.selectedNews.Id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Komentārs tika veiksmīgi pievienots");
                $scope.selectedNews.Commentaries.push(response.data.comment);
                $scope.comment.text = "";
                $scope.selectedNews.CommentCount++;
            } else {
                if (response.data.comment === undefined)
                    mainFactory.showNotify("Kļūda! Jūsu kommntārs ir tukšs");
                else
                    mainFactory.showNotify("Kļūda! Ziņa neeksistē");
            }
        });
    };

    $scope.deleteComment = function (id) {
        newsFactory.deleteComment(id).then(function (response) {
            if (response.data.success) {
                var removed = $scope.selectedNews.Commentaries.find(f => f.Id === id);

                if (removed !== null) {
                    $scope.selectedNews.Commentaries.splice($scope.selectedNews.Commentaries.indexOf(removed), 1);
                    $scope.selectedNews.CommentCount--;

                    mainFactory.showNotify("Komentārs tika nodzēst");
                }               
            }
        });
    };

    $scope.stepBack = function () {
        if ($scope.news.length === 0) {
            getNews();
        } else {
            for (var i = 0; i < $scope.news.length; i++) {
                if ($scope.news[i].Id === $scope.selectedNews.Id) {
                    $scope.news[i].LikeCount = $scope.selectedNews.LikeCount;
                    $scope.news[i].IsYouLiked = $scope.selectedNews.IsYouLiked;
                    break;
                }
            }
        }

        $scope.activeSection = $scope.sections.list;
    };

    $scope.changeLanguage = function () {
            var text = $scope.selectedNews.Text;

            $scope.selectedNews.Text = $scope.selectedNews.TextRu;
            $scope.selectedNews.TextRu = text;
    };

    init();
}
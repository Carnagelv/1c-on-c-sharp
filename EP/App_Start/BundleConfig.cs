using System.Web.Optimization;

namespace EP
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/libraries").Include(
                        "~/Scripts/jquery-3.3.1.min.js",
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-aria.min.js",
                        "~/Scripts/angular-material.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-messages.min.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-sanitize.min.js",
                        "~/AngularApp/Scripts/main/mainFactory.js",
                        "~/AngularApp/Scripts/main/mainController.js",
                        "~/AngularApp/Scripts/main/main.js"));

            bundles.Add(new ScriptBundle("~/Bundles/user").Include(
                        "~/Scripts/ng-file-upload.min.js",
                        "~/AngularApp/Scripts/user/userFactory.js",
                        "~/AngularApp/Scripts/friend/friendFactory.js",
                        "~/AngularApp/Scripts/user/userController.js",
                        "~/AngularApp/Scripts/main/mainModalController.js",
                        "~/AngularApp/Scripts/friend/friendController.js",
                        "~/AngularApp/Scripts/user/user.js"));

            bundles.Add(new ScriptBundle("~/Bundles/friends").Include(
                        "~/AngularApp/Scripts/friend/friendFactory.js",
                        "~/AngularApp/Scripts/friend/friendController.js",
                        "~/AngularApp/Scripts/friend/friend.js"));

            bundles.Add(new ScriptBundle("~/Bundles/news").Include(
                        "~/AngularApp/Scripts/news/newsFactory.js",
                        "~/AngularApp/Scripts/news/newsController.js",
                        "~/AngularApp/Scripts/news/news.js"));

            bundles.Add(new ScriptBundle("~/Bundles/teams").Include(
                        "~/AngularApp/Scripts/team/teamFactory.js",
                        "~/AngularApp/Scripts/team/teamController.js",
                        "~/AngularApp/Scripts/team/teamModalController.js",
                        "~/AngularApp/Scripts/team/team.js"));

            bundles.Add(new ScriptBundle("~/Bundles/feeds").Include(
                        "~/AngularApp/Scripts/feed/feedFactory.js",
                        "~/AngularApp/Scripts/feed/feedController.js",
                        "~/AngularApp/Scripts/feed/feed.js"));

            bundles.Add(new ScriptBundle("~/Bundles/messages").Include(
                        "~/AngularApp/Scripts/message/messageFactory.js",
                        "~/AngularApp/Scripts/message/messageController.js",
                        "~/AngularApp/Scripts/message/messageModalController.js",
                        "~/AngularApp/Scripts/message/message.js"));

            bundles.Add(new ScriptBundle("~/Bundles/tournaments").Include(
                        "~/AngularApp/Scripts/tournament/tournamentFactory.js",
                        "~/AngularApp/Scripts/tournament/tournamentController.js",
                        "~/AngularApp/Scripts/tournament/tournament.js"));

            bundles.Add(new ScriptBundle("~/Bundles/players").Include(
                        "~/AngularApp/Scripts/player/playerFactory.js",
                        "~/AngularApp/Scripts/player/playerController.js",
                        "~/AngularApp/Scripts/player/player.js"));

            bundles.Add(new ScriptBundle("~/Bundles/movies").Include(
                        "~/AngularApp/Scripts/movie/movieFactory.js",
                        "~/AngularApp/Scripts/movie/movieController.js",
                        "~/AngularApp/Scripts/movie/movie.js"));

            bundles.Add(new ScriptBundle("~/Bundles/matches").Include(
                        "~/AngularApp/Scripts/match/matchFactory.js",
                        "~/AngularApp/Scripts/match/matchController.js",
                        "~/AngularApp/Scripts/match/match.js"));

            bundles.Add(new StyleBundle("~/Content/libraries").Include(
                      "~/Content/css/angular-material.min.css",
                      "~/Content/css/bootstrap-grid.min.css",
                      "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/main").Include(
                      "~/Content/css/main.css"));

            bundles.Add(new StyleBundle("~/Content/user").Include(
                      "~/Content/css/user.css"));

            bundles.Add(new StyleBundle("~/Content/friend").Include(
                      "~/Content/css/friend.css"));

            bundles.Add(new StyleBundle("~/Content/news").Include(
                      "~/Content/css/news.css"));

            bundles.Add(new StyleBundle("~/Content/team").Include(
                      "~/Content/css/team.css"));

            bundles.Add(new StyleBundle("~/Content/tournament").Include(
                      "~/Content/css/match.css",
                      "~/Content/css/tournament.css"));

            bundles.Add(new StyleBundle("~/Content/movie").Include(
                      "~/Content/css/movie.css"));

            bundles.Add(new StyleBundle("~/Content/match").Include(
                      "~/Content/css/match.css"));

            bundles.Add(new StyleBundle("~/Content/player").Include(
                      "~/Content/css/player.css"));
        }
    }
}

(function () {
    angular.module('EP', ['ngSanitize', 'ngMaterial', 'ngMessages', 'ngRoute'])
        .controller('MenuCtrl', function ($scope, $mdSidenav) {
            $scope.toggleMenu = function () {
                $mdSidenav('left').toggle();
            };
            $scope.isActiveLink = function (link) {
                return new URL(window.location.href).pathname.indexOf(link) !== -1; 
            };
        })
        .constant('templateUrl', '/AngularApp/AngularTemplates/')
        .constant('disciplineEnum', {
            football: 1,
            basketball: 2,
            cyberSport: 3
        })
        .config(function ($mdThemingProvider) {
            var newBlue = $mdThemingProvider.extendPalette('blue', {
                '500': '#1976D2',
                '600': '#1F70A8',
                'contrastDefaultColor': 'light'
            });
            $mdThemingProvider.definePalette('newBlue', newBlue);
            $mdThemingProvider.theme('esi-pirmais')
                .primaryPalette('newBlue', {
                    'default': '600',
                    'hue-1': '500'
                });
            $mdThemingProvider.setDefaultTheme('esi-pirmais');
        });

    initNgModules();
})();

function initNgModules() {
    angular.element(document).ready(function () {
        var elements = angular.element(document.querySelectorAll("[ng-module]"));
        _.each(elements, function (element, index, list) {
            var appName = element.attributes["ng-module"].value;
            if (appName) {
                angular.bootstrap(angular.element(element), [appName]);
            }
        });
    });
}
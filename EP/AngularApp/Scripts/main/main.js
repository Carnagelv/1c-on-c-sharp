(function () {
    angular.module('OneC', ['ngSanitize', 'ngMaterial', 'ngMessages', 'ngRoute'])
        .controller('mainCtrl', MainCtrl)
        .controller('mainModalCtrl', MainModalCtrl)
        .factory('mainFactory', MainFactory)
        .constant('templateUrl', '/AngularApp/AngularTemplates/')        
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
MainFactory.$inject = ['$mdToast', '$mdDialog', '$location', '$anchorScroll', 'templateUrl', '$http'];

function MainFactory($mdToast, $mdDialog, $location, $anchorScroll, templateUrl, $http) {
    var factory = {};

    factory.showWait = function () {
       $mdDialog.show({
            onShowing: function (scope, element) {
                $(element).addClass('wait-modal');
            },
            templateUrl: templateUrl + "WaitModal.html",
            parent: angular.element(document.body),
            fullscreen: false,
            hasBackdrop: true
        });
    };    

    factory.hideWait = function () {
         $mdDialog.cancel();
    };

    factory.showModal = function (ctrl, templateName, data) {
        $mdDialog.show({
            controller: ctrl,
            templateUrl: templateUrl + templateName,
            parent: angular.element(document.body),
            escapeToClose: true,
            fullscreen: false,
            multiple: false,
            resolve: {
                modalData: function () {
                    return data;
                }
            }
        });
    };

    factory.showNotify = function (message) {
        $mdToast.show(
            $mdToast.simple()
                .textContent(message)
                .position('top right')
                .hideDelay(1000)
        );
    };

    factory.saveCatalog = function (name) {
        return $http.post('/Catalog/SaveCatalog', { name });
    };

    factory.loadTable = function () {
        return $http.get('/Catalog/LoadTable');
    };

    return factory;
}
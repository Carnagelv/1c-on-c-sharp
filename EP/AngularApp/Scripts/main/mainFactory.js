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

    factory.saveRow = function (rows, tableId) {
        return $http.post('/Catalog/SaveRow', { rows, tableId });
    };

    factory.loadTable = function () {
        return $http.get('/Catalog/LoadTable');
    };

    factory.getColumns = function (id) {
        return $http.get('/Catalog/GetColumns', { params: { id }});
    };

    factory.deleteRow = function (id) {
        return $http.get('/Catalog/DeleteRow', { params: { id } });
    };

    factory.saveColumn = function (name, tableId, parentId) {
        return $http.post('/Catalog/SaveColumn', { name, tableId, parentId });
    };

    return factory;
}
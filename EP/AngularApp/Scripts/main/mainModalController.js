MainModalCtrl.$inject = ['$scope', '$mdDialog', 'mainFactory', 'modalData'];

function MainModalCtrl($scope, $mdDialog, mainFactory, modalData) {
    $scope.modalData = modalData;
    $scope.modalData.columns = [];

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.saveCatalog = function () {
        mainFactory.saveCatalog($scope.modalData.catalogName).then(function (response) {
            if (response.data.success) {
                $scope.modalData.loadTable();
                $scope.cancel();
            } else {
                mainFactory.showNotify('Error! Name is empty or not unique');
            }
        });
    };

    $scope.getColums = function () {
        mainFactory.getColumns($scope.modalData.tableId).then(function (response) {
            $scope.modalData.columns = response.data.columns;
        });
    };

    $scope.saveRow = function () {
        mainFactory.saveRow($scope.modalData.columns, $scope.modalData.tableId).then(function (response) {
            if (response.data.success) {
                $scope.modalData.loadTable();
                $scope.cancel();
            } else {
                mainFactory.showNotify('Error! Some data has incorrect format');
            }
        });
    };

    $scope.saveColumn = function () {
        $scope.modalData.parentId = $scope.modalData.parentId || 0;

        mainFactory.saveColumn($scope.modalData.columnName, $scope.modalData.tableId, $scope.modalData.parentId).then(function (response) {
            if (response.data.success) {
                $scope.modalData.loadTable();
                $scope.cancel();
            } else {
                mainFactory.showNotify('Error! Name is empty or not unique');
            }
        });
    };
}
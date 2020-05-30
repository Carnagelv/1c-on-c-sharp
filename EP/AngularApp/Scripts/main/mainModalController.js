MainModalCtrl.$inject = ['$scope', '$mdDialog', 'mainFactory', 'modalData'];

function MainModalCtrl($scope, $mdDialog, mainFactory, modalData) {
    $scope.modalData = modalData;

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
}
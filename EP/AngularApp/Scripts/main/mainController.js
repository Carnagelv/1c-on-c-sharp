MainCtrl.$inject = ['$scope', 'mainFactory'];

function MainCtrl($scope, mainFactory) {
    $scope.tables = {};

    function loadTable() {
        mainFactory.loadTable().then(function (response) {
            $scope.tables = response.data.tables;
        });
    }

    $scope.createCatalog = function () {
        var data = {
            loadTable: function () {
                loadTable();
            }
        };

        mainFactory.showModal(MainModalCtrl, "CreateCatalog.html", data);        
    };

    loadTable();
}
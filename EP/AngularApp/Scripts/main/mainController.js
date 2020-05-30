MainCtrl.$inject = ['$scope', 'mainFactory'];

function MainCtrl($scope, mainFactory) {
    $scope.tables = {};

    function loadTable() {
        mainFactory.loadTable().then(function (response) {
            $scope.tables = response.data.tables;
        });
    }

    function createInitialData() {
        var data = {
            loadTable: function () {
                loadTable();
            }
        };

        return data;
    }

    $scope.createCatalog = function () {
        mainFactory.showModal(MainModalCtrl, "CreateCatalog.html", createInitialData());
    };

    $scope.addRow = function (tableId) {
        var data = createInitialData();
        data.tableId = tableId;

        mainFactory.showModal(MainModalCtrl, "AddRow.html", data);
    };

    loadTable();
}
MainCtrl.$inject = ['$scope', 'mainFactory'];

function MainCtrl($scope, mainFactory) {
    $scope.tables = {};

    function loadTable() {
        mainFactory.loadTable().then(function (response) {
            $scope.tables = response.data.tables;

            $scope.tables.forEach(function (table) {
                table.doubleRowSpan = table.Columns.length > 1;
                table.childColumns = getChildColumns(table.Columns);
            });
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

    function getChildColumns(columns) {
        var result = [];
        var tableId = columns[0].TableId;

        columns.forEach(function (column) {
            if (column.ChildColumns.length > 0) {
                for (var i = 0; i < column.ChildColumns.length; i++) {
                    result.push({
                        Name: column.ChildColumns[i].Name,
                        TableId: tableId,
                        ParentId: column.ChildColumns[i].ParentId,
                        Id: column.ChildColumns[i].Id,
                        ShowAdd: i + 1 === column.ChildColumns.length
                    });
                }
            } else {
                if (columns[0].Name !== column.Name) {
                    result.push({
                        Name: null,
                        TableId: tableId,
                        ParentId: column.Id,
                        Id: 0,
                        ShowAdd: true
                    });
                }
            }
        });

        return result;
    }

    $scope.createCatalog = function () {
        mainFactory.showModal(MainModalCtrl, "CreateCatalog.html", createInitialData());
    };

    $scope.addRow = function (tableId) {
        var data = createInitialData();
        data.tableId = tableId;

        mainFactory.showModal(MainModalCtrl, "AddRow.html", data);
    };

    $scope.deleteRow = function (id) {
        mainFactory.deleteRow(id).then(function (response) {
            if (response.data.success) {
                loadTable();
            } else {
                mainFactory.showNotify('Error! Row does not exist');
            }
        });
    };

    $scope.addColumn = function (tableId, parentId) {
        var data = createInitialData();
        data.tableId = tableId;
        data.parentId = parentId;

        mainFactory.showModal(MainModalCtrl, "AddColumn.html", data);
    };

    $scope.addValue = function (rowId, columnId) {
        var data = createInitialData();
        data.rowId = rowId;
        data.columnId = columnId;
        data.valueId = null;

        mainFactory.showModal(MainModalCtrl, "AddRowValue.html", data);
    };

    $scope.deleteValue = function (valueId) {
        mainFactory.deleteValue(valueId).then(function (response) {
            if (response.data.success) {
                loadTable();
            } else {
                mainFactory.showNotify('Error! Please, reload the page');
            }
        });
    };

    $scope.editValue = function (valueId, valueName, rowId, columnId) {
        var data = createInitialData();
        data.rowId = rowId;
        data.columnId = columnId;
        data.newValue = valueName;
        data.valueId = valueId;

        mainFactory.showModal(MainModalCtrl, "AddRowValue.html", data);
    };

    loadTable();
}
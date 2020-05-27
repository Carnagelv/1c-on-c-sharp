MessageModalCtrl.$inject = ['$scope', 'messageFactory', 'modalData', '$mdDialog', 'mainFactory'];

function MessageModalCtrl($scope, messageFactory, modalData, $mdDialog, mainFactory) {
    $scope.users = modalData.users;
    $scope.selectedUser = {
        Id: 0,
        Name: ''
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.createDialog = function () {
        messageFactory.createDialog($scope.selectedUser.Id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Dialogs veiksmīgi tika izveidots");
                location.href = "/Message?Id=" + response.data.id;
            } else {
                mainFactory.showNotify("Kļūda! Mēģiniet vēl reiz");
            }
        });
    };
}
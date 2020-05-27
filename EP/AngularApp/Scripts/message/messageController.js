MessageCtrl.$inject = ['$scope', 'messageFactory', 'mainFactory'];

function MessageCtrl($scope, messageFactory, mainFactory) {
    $scope.sections = {
        list: 1,
        item: 2
    };

    $scope.dialogs = [];
    $scope.currentDialog = [];
    $scope.isLoading = false;
    $scope.model = {
        message: '',
        id: 0
    };
    $scope.currentSection = $scope.sections.list;

    function init() {
        var id = new URL(window.location.href).searchParams.get('Id');

        id !== null && id > 0
            ? $scope.getDialogById(id)
            : getDialogs();       
    }

    function getDialogs() {
        $scope.isLoading = true;
        messageFactory.getDialogs().then(function (response) {
            $scope.dialogs = response.data.dialogs;
            $scope.isLoading = false;
        });
    }

    setInterval(function () {
        scrollAndRequest();
    }, 5000);

    function scrollAndRequest() {
        if ($scope.currentSection === $scope.sections.item) {
            messageFactory.hasNewMessage($scope.model.dialog).then(function (response) {
                if (response.data.success) {
                    $scope.updateDialogById($scope.model.dialog);
                }
            });

            var objDiv = document.getElementById("chat");

            if (objDiv !== null)
                objDiv.scrollTop = objDiv.scrollHeight;
        }
    }

    $scope.getDialogById = function (id) {
        $scope.currentSection = $scope.sections.item;
        $scope.isLoading = true;
        messageFactory.getDialogById(id).then(function (response) {
            $scope.currentDialog = response.data.dialog;
            $scope.model.dialog = id;
            $scope.isLoading = false;
        });
    };

    $scope.updateDialogById = function (id) {
        messageFactory.getDialogById(id).then(function (response) {
            $scope.currentDialog = response.data.dialog;
        });
    };

    $scope.createDialog = function () {
        mainFactory.showWait();
        messageFactory.getUsersForDialog().then(function (response) {
            mainFactory.showModal(MessageModalCtrl, "CreateDialogModal.html", { users: response.data.users });
            mainFactory.hideWait();
        });
    };

    $scope.sentMessage = function () {
        messageFactory.sentMessage($scope.model.message, $scope.model.dialog).then(function (response) {
            $scope.currentDialog = response.data.dialog;
            $scope.model.message = '';
        });
    };    

    init();
}
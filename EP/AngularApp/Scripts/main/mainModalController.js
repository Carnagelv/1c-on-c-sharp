MainModalCtrl.$inject = ['$scope', '$mdDialog', 'userFactory', 'mainFactory'];

function MainModalCtrl($scope, $mdDialog, userFactory, mainFactory) {
    $scope.isLoading = false;
    $scope.currentStep = 1;
    $scope.userData = {
        UserName: '',
        FirstName: '',
        LastName: '',
        Password: '',
        RePassword: ''
    };
    $scope.isLoginSection = true;
    $scope.isResetPasswordSection = false;

    $scope.$watch(function () {
        $scope.isLoading = userFactory.isLoading;
    });

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.isExistdIdentity = function () {
        return $scope.userData.Password === $scope.userData.RePassword;
    };

    $scope.toRegisterUser = function () {
        $scope.isLoading = true;
        userFactory.registerUser($scope.userData);
    };

    $scope.toLoginUser = function () {
        $scope.isLoading = true;
        userFactory.loginUser($scope.userData);
    };

    $scope.nextStep = function () {
        $scope.currentStep++;
    };

    $scope.stepBack = function () {
        $scope.currentStep--;
    };

    $scope.openResetPassword = function () {
        $scope.isLoginSection = false;
        $scope.isResetPasswordSection = true;
    };

    $scope.resetPassword = function () {
        userFactory.resetPassword($scope.userData.UserName).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Instrukcijas tika nosūtītas");
                $scope.cancel();
            } else {
                mainFactory.showNotify("Klūda. Uzrakstiet administrācijai");
            }
        });
    };
}
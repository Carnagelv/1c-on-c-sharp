UserCtrl.$inject = ['$scope', 'mainFactory', 'userFactory', 'Upload'];

function UserCtrl($scope, mainFactory, userFactory, Upload) {
    $scope.userData = {};
    $scope.otherData = {};
    $scope.userDataBackup = {};
    $scope.isSectionUser = true;
    $scope.isSectionSetting = false;
    $scope.isSectionChangePassword = false;
    $scope.isSectionChangeEmail = false;
    $scope.isLoading = true;
    $scope.isSaved = false;
    $scope.kods = '';

    $scope.disciplines = [
        { value: 1, name: 'Futbols' },
        { value: 2, name: 'Basketbols' },
        { value: 3, name: 'KiberSports' }
    ];

    $scope.positions = [
        { value: 1, name: 'Vārtsargs' },
        { value: 2, name: 'Aizsargs' },
        { value: 3, name: 'Pussargs' },
        { value: 4, name: 'Uzbrucējs' },
        { value: 5, name: 'Treneris' }
    ];

    function fillOtherFields() {
        var position = $scope.positions.find(f => f.value === $scope.userData.Position);
        var discipline = $scope.disciplines.find(f => f.value === $scope.userData.Discipline);

        $scope.userData.PositionName = position !== undefined ? position.name : null;
        $scope.userData.DisciplineName = discipline !== undefined ? discipline.name : null;
    }

    function ucfirst(text) {
        return text.length > 0 ? text.charAt(0).toUpperCase() + text.slice(1) : "";
    }

    $scope.registration = function () {
        mainFactory.showWait();
        mainFactory.showModal(MainModalCtrl, "RegistrationModal.html");
        mainFactory.hideWait();
    };

    $scope.login = function () {
        mainFactory.showWait();
        mainFactory.showModal(MainModalCtrl, "LoginModal.html");
        mainFactory.hideWait();
    };

    $scope.init = function (id) {
        userFactory.getUser(id).then(function (response) {
            $scope.userData = response.data.user;
            $scope.otherData = response.data.other;
            $scope.headerName = $scope.userData.CreateDate === null ? "-" : $scope.userData.FirstName + " " + $scope.userData.LastName;

            fillOtherFields();

            $scope.isLoading = false;
        });
    };

    $scope.userSettings = function () {
        $scope.isSectionUser = false;
        $scope.isSectionSetting = true;
        $scope.isSaved = false;

        $scope.userDataBackup = angular.copy($scope.userData);
        $scope.userData.Discipline = $scope.userData.Discipline || 1;
        $scope.userData.Position = $scope.userData.Position || 1;

        $scope.userData.DateOfBirth = $scope.userData.DateOfBirth === null
            ? new Date(1463800000000)
            : new Date($scope.getCleanDate($scope.userData.DateOfBirth));        
    };

    $scope.saveSettings = function () {
        $scope.userData.FirstName = ucfirst($scope.userData.FirstName);
        $scope.userData.LastName = ucfirst($scope.userData.LastName);

        userFactory.saveSettings($scope.userData).then(function (response) {
            if (response.data.success) {
                $scope.isSaved = true;
                mainFactory.showNotify("Izmaiņas tika saglabāti");
                fillOtherFields();
            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };

    $scope.backToProfile = function () {
        $scope.isSectionUser = true;
        $scope.isSectionSetting = false;
        $scope.isSectionChangePassword = false;
        $scope.isSectionChangeEmail = false;

        if (!$scope.isSaved) {
            $scope.userData = $scope.userDataBackup;
        }
    };

    $scope.showMainSection = true;
    $scope.showSocialNetwork = false;
    $scope.showCyberSport = false;

    $scope.social = function () {
        $scope.showMainSection = false;
        $scope.showSocialNetwork = true;
    };

    $scope.cyber = function () {
        $scope.showMainSection = false;
        $scope.showCyberSport = true;
    };

    $scope.scrollTo = function (toHas) {
        mainFactory.scrollTo(toHas);
    };

    $scope.backToMain = function () {
        $scope.showMainSection = true;
        $scope.showSocialNetwork = false;
        $scope.showCyberSport = false;
    };

    $scope.getCleanDate = function (date) {
        return typeof(date) === "string" 
            ? parseInt(date.replace("/Date(", "").replace(")/", ""))
            : date;
    };

    $scope.activateUser = function () {
        userFactory.activateUser($scope.kods).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Jūsu profils ir aktīvs!");
                window.location.reload();
            } else {
                mainFactory.showNotify("Klūda. Uzrakstiet administrācijai");
            }
        });
    };

    $scope.changeEmail = function () {
        $scope.isSectionSetting = false;
        $scope.isSectionChangePassword = false;
        $scope.isSectionChangeEmail = true;
        $scope.isSectionUser = false;
        $scope.isSectionSetting = false;

        $scope.userData.newEmail = $scope.userData.oldPassword = "";
    };

    $scope.changePassword = function () {
        $scope.isSectionSetting = false;
        $scope.isSectionChangeEmail = false;
        $scope.isSectionChangePassword = true;
        $scope.isSectionUser = false;
        $scope.isSectionSetting = false;

        $scope.userData.oldPassword = "";
        $scope.userData.newPassword = "";
        $scope.userData.newPasswordRepeat = "";
    };

    $scope.actionChangeEmail = function () {
        userFactory.checkNewEmail($scope.userData.newEmail).then(function (response) {
            if (response.data.success) {
                userFactory.changeEmail($scope.userData.newEmail, $scope.userData.oldPassword).then(function (secondResponse) {
                    if (secondResponse.data.success) {
                        mainFactory.showNotify("Lietotāja vārds tika nomainīts");
                    } else {
                        mainFactory.showNotify("Klūda. Uzrakstiet administrācijai");
                    }
                });
            } else {
                mainFactory.showNotify("Lietotāja vārds jau ir aizņēmts");
            }
        });
    };

    $scope.checkChangePassword = function () {
        return $scope.userData.oldPassword === undefined || $scope.userData.oldPassword.length < 8 || $scope.userData.newPassword === undefined
            || $scope.userData.newPassword.length < 8 || $scope.userData.newPasswordRepeat.length < 8 || $scope.userData.newPasswordRepeat === undefined
            || $scope.userData.newPassword !== $scope.userData.newPasswordRepeat;
    };

    $scope.actionChangePassword = function () {
        userFactory.changePassword($scope.userData.newPassword, $scope.userData.oldPassword).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Parole tika nomainīta");
            } else {
                mainFactory.showNotify("Klūda. Uzrakstiet administrācijai");
            }
        });
    };

    $scope.uploadPhoto = function (file, errFiles) {
        if (errFiles !== undefined && errFiles.length) {
            $scope.errFile = "";
            for (var i = 0; i < errFiles.length; i++) {
                switch (errFiles[i].$error) {
                    case "maxSize":
                        $scope.errFile += "Fotogrāfijas izmērs ir lielāks nekā 1MB <br>";
                        break;
                    case "pattern":
                        $scope.errFile += "Fotogrāfijai ir nepareizs formāts";
                        break;
                }
            }
        } else {
            if (file) {
                file.upload = Upload.upload({
                    url: '/User/UploadPhoto',
                    data: {
                        Photo: file
                    }
                });

                file.upload.then(function (response) {
                    if (response.data.success) {
                        mainFactory.showNotify('Fotogrāfija tika atjaunota');
                        $scope.userData.Photo = $scope.userDataBackup.Photo = response.data.photo;
                    } else {
                        mainFactory.showNotify("Klūda. Uzrakstiet administrācijai");
                    }
                });
            }
        }
    };
}
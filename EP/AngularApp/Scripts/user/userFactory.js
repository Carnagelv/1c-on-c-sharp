UserFactory.$inject = ['$http', 'mainFactory'];

function UserFactory($http, mainFactory) {
    var factory = {
        isLoading: false
    };

    function readResponse(data) {
        if (data.success) {
            window.location.href = "/Feed";
        } else {            
            mainFactory.showNotify(data.errorMessage);
            factory.isLoading = false;
        }
    }

    factory.registerUser = function (userData) {
        factory.isLoading = true;
        $http.post('/Home/Register', { user: userData }).then(function (response) {
            readResponse(response.data);
        });
    };

    factory.loginUser = function (userData) {
        factory.isLoading = true;
        $http.post('/Home/Login', { UserName: userData.UserName, Password: userData.Password }).then(function (response) {
            readResponse(response.data);
        });
    };

    factory.getUser = function (id) {
        return $http.get('/User/Get', { params: { Id: id } });
    };

    factory.saveSettings = function (userData) {
        return $http.post('/User/Save', userData);
    };

    factory.activateUser = function (kods) {
        return $http.post('/User/Activate', { code: kods });
    };

    factory.checkNewEmail = function (email) {
        return $http.get('/User/CheckNewEmail', { params: { email } });
    };

    factory.changeEmail = function (email, password) {
        return $http.post('/User/ChangeEmail', { email, password });
    };

    factory.changePassword = function (password, oldPassword) {
        return $http.post('/User/ChangePassword', { password, oldPassword });
    };

    factory.resetPassword = function (email) {
        return $http.post('/User/ResetPassword', { email });
    };

    return factory;
}
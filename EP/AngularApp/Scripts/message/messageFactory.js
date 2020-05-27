MessageFactory.$inject = ['$http'];

function MessageFactory($http) {
    var factory = {};
    
    factory.getDialogs = function () {
        return $http.get('/Message/GetDialogs');
    };

    factory.createDialog = function (userId) {
        return $http.post('/Message/CreateDialog', { recipientId: userId });
    };

    factory.getUsersForDialog = function () {
        return $http.get('/Message/GetUsersForDialog');
    };

    factory.getDialogById = function (dialogId) {
        return $http.get('/Message/GetDialog', { params: { dialogId } });
    };

    factory.sentMessage = function (message, dialogId) {
        return $http.post('/Message/SentMessage', { dialogId, message });
    };

    factory.hasNewMessage = function (dialogId) {
        return $http.get('/Message/IsExistNewMessage', { params: { dialogId } });
    };

    return factory;
}
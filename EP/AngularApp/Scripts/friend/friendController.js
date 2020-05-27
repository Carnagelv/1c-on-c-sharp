FriendCtrl.$inject = ['$scope', 'mainFactory', 'friendFactory'];

function FriendCtrl($scope, mainFactory, friendFactory) {
    $scope.isLoading = true;

    $scope.userType = {
        friend: 1,
        requestFromDecline: 2,
        requestFromAccept: 3,
        abort: 4,
        requestTo: 5
    };

    $scope.friendsPageType = {
        userPage: 1,
        userFriends: 2
    };

    function hideUserProfile(id, userType) {
        if (userType === $scope.userType.friend) {
            let removed = $scope.friends.find(f => f.UserId === id);

            $scope.otherUsers.push(removed);
            $scope.friends.splice($scope.friends.indexOf(removed), 1);
        } else if (userType === $scope.userType.requestFromDecline) {
            let removed = $scope.requestsTo.find(f => f.UserId === id);

            $scope.otherUsers.push(removed);
            $scope.requestsTo.splice($scope.requestsTo.indexOf(removed), 1);
        } else if (userType === $scope.userType.requestFromAccept) {
            let removed = $scope.requestsTo.find(f => f.UserId === id);

            $scope.friends.push(removed);
            $scope.requestsTo.splice($scope.requestsTo.indexOf(removed), 1);
        } else if (userType === $scope.userType.abort) {
            let removed = $scope.requestsFrom.find(f => f.UserId === id);

            $scope.otherUsers.push(removed);
            $scope.requestsFrom.splice($scope.requestsFrom.indexOf(removed), 1);
        } else if (userType === $scope.userType.requestTo) {
            let removed = $scope.otherUsers.find(f => f.UserId === id);

            $scope.requestsFrom.push(removed);
            $scope.otherUsers.splice($scope.otherUsers.indexOf(removed), 1);
        }
    }

    $scope.init = function (id) {
        friendFactory.getFriends(id).then(function (response) {
            $scope.friends = response.data.friends;
            $scope.requestsFrom = response.data.requestsFrom;
            $scope.requestsTo = response.data.requestsTo;
            $scope.otherUsers = response.data.otherUsers;

            $scope.isLoading = false;
        });
    };

    $scope.requestToFriend = function (id, pageType) {
        friendFactory.requestToFriend(id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Pieteikums tika nosūtīts");

                if (pageType === $scope.friendsPageType.userFriends)
                    hideUserProfile(id, $scope.userType.requestTo);
                else
                    $scope.userData.IsRequested = true;

            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };

    $scope.abortRequest = function (id, pageType) {
        friendFactory.abortRequest(id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Pieteikums tika atcelts");

                if (pageType === $scope.friendsPageType.userFriends) 
                    hideUserProfile(id, $scope.userType.abort);
                else
                    $scope.userData.IsRequested = false;

            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };

    $scope.acceptRequest = function (id, pageType) {
        friendFactory.acceptRequest(id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Pieteikums tika apstiprināts");

                if (pageType === $scope.friendsPageType.userFriends) {
                    hideUserProfile(id, $scope.userType.requestFromAccept);
                } else {
                    $scope.userData.IsAsked = false;
                    $scope.userData.IsFriend = true;
                }
            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };

    $scope.declineRequest = function (id, pageType) {
        friendFactory.declineRequest(id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Pieteikums tika atcelts");

                if (pageType === $scope.friendsPageType.userFriends)
                    hideUserProfile(id, $scope.userType.requestFromDecline);
                else
                    $scope.userData.IsAsked = false;

            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };

    $scope.deleteFromFriend = function (id, pageType) {
        friendFactory.deleteFromFriend(id).then(function (response) {
            if (response.data.success) {
                mainFactory.showNotify("Draugs tika nodzēst");

                if (pageType === $scope.friendsPageType.userFriends) 
                    hideUserProfile(id, $scope.userType.friend);
                else 
                    $scope.userData.IsFriend = false;

            } else {
                mainFactory.showNotify(response.data.errorMessage);
            }
        });
    };
}
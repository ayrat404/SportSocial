'use strict';

angular.module('admin').controller('ConferenceNewCtrl', ['$scope', 'utilsSrvc', 'adminRqst',
function ($scope, utilsSrvc, adminRqst) {
    // сообщения
    // ---------------
    $scope.msg = {
        created: false
    };

    // ошибки
    // ---------------
    $scope.er = {
        server: false,  // сервер недоступен
        create: false   // ошибка при создании
    }
    
    // создать конференцию
    // ---------------
    $scope.createConference = function (data, isInvalid) {
        if (isInvalid) {
            $scope.er.create = true;
            return;
        }
        $scope.er.server = false;
        $scope.er.create = true;
        adminRqst.createConference(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.msg.created = true;
                    $scope.model.title = '';
                    $scope.model.description = '';
                    $scope.model.link = '';
                    $scope.model.date = '';
                } else {
                    $scope.er.create = true;
                }
            }, function() {
            $scope.er.server = true;
        });
    }

}]);

'use strict';

angular.module('admin').controller('ConferenceNewCtrl', ['$scope', 'utilsSrvc', 'adminRqst',
function ($scope, utilsSrvc, adminRqst) {
    // сообщения
    // ---------------
    $scope.msg = {
        success: false  // сообщение об успешной операции
    };

    $scope.model = {};

    // начало валидации полей
    // ---------------
    $scope.vs = {
        date        :   false,
        title       :   false,
        description :   false
    }

    // ошибки
    // ---------------
    $scope.er = {
        server  :   false,  // сервер недоступен
        success :   false   // ошибка при создании
    }
    
    // создать конференцию
    // ---------------
    $scope.sendForm = function (data, isInvalid) {
        if (isInvalid) {
            for (var vs in $scope.vs) {
                $scope.vs[vs] = true;
            }
            return;
        }
        $scope.er.server = false;
        $scope.er.success = true;
        adminRqst.createConference(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.msg.success = true;
                    $scope.model.title = '';
                    $scope.model.description = '';
                    $scope.model.link = '';
                    $scope.model.date = '';
                } else {
                    $scope.er.success = true;
                }
            }, function() {
            $scope.er.server = true;
        });
    }

}]);

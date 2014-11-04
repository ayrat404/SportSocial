'use strict';

angular.module('admin').controller('ConferenceEditCtrl', ['$scope', 'utilsSrvc', 'adminRqst', '$routeParams',
function ($scope, utilsSrvc, adminRqst, $routeParams) {
    var id = null; // id редактируемой конференции

    // сообщения
    // ---------------
    $scope.msg = {
        success: false  // сообщение об успешной операции
    };

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
        server  :   false,   // сервер недоступен
        success :   false,  // ошибка при создании
        id      :   false    // отсутствует/не существует id конференции
    }

    // лоадеры
    // ---------------
    $scope.ld = {
        conference  :   false,  // loader загрузки данных о конференции
        edit        :   false   // loader отправки данных на редактирование конференции
    }
    
    // проверяем передачу id конференции
    // ---------------
    if ($routeParams === undefined || $routeParams === null) {
        $scope.er.id = true;
        return;
    }

    // записываем id, переданный через url
    // ---------------
    id = $routeParams.id;

    // загружаем данные о конференции
    // ---------------
    $scope.ld.conference = true;
    adminRqst.getConferences({ id: id })
        .then(function (res) {
            if (res.data !== '') {
                $scope.model = res.data;
            } else {
                $scope.er.id = true;
            }
            $scope.er.server = false;
        }, function() {
            $scope.er.server = true;
        }).finally(function () {
            $scope.ld.conference = false;
        });
    
    // отредатировать конференцию
    // ---------------
    $scope.ld.edit = true;
    $scope.sendForm = function (data, isInvalid) {
        data.id = id;
        if (isInvalid) {
            for (var vs in $scope.vs) {
                $scope.vs[vs] = true;
            }
            return;
        }
        $scope.er.server = false;
        $scope.er.success = true;
        adminRqst.editConference(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.msg.success = true;
                } else {
                    $scope.er.success = true;
                }
                $scope.er.server = false;
            }, function() {
                $scope.er.server = true;
            }).finally(function() {
                $scope.ld.edit = false;
            });
    }

}]);

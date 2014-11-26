'use strict';

// Контроллер авторизации пользователя
// ---------------
angular.module('blog').controller('AuthorizationCtrl',
    ['$scope',
     '$interval',
     '$window',
     'loginRqst',
     'utilsSrvc',
function ($scope, $interval, $window, authorizationRqst, utilsSrvc) {
    $scope.loading      =   false;  // показать/скрыть лоадер
    $scope.er = {                   // ошибки
        s404    :   false,          // сервер не доступен
        server  :   ''              // ошибка с сервера с сообщением
    }

    // Отправка данных, запрос кода подтверждения
    // ---------------------
    $scope.signIn = function (data) {
        $scope.er.server = '';
        $scope.er.s404 = false;
        $scope.loading = true;
        $scope.formDisabled = true;
        authorizationRqst.signIn(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $window.location.href = res.data.redirect;
                } else {
                    $scope.er.server = res.data.error;
                }
                $scope.loading = true;
                $scope.formDisabled = true;
            }, function () {
                $scope.er.s404 = true;
                $scope.loading = true;
                $scope.formDisabled = true;
            });
    }

}]);

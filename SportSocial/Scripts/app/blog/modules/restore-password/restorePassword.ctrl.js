'use strict';

// Контроллер восстановления пароля
// ---------------
angular.module('blog').controller('RestorePasswordCtrl',
    ['$scope',
     'loginRqst',
     'utilsSrvc',
     '$interval',
function ($scope, loginRqst, utilsSrvc, $interval) {

    // лоадер
    // ---------------
    $scope.isLoading = false;

    // ошибки
    // ---------------
    $scope.er = {
        s404    :   false,      // сервер недоступен
        server  :   ''          // ошибка с сервера
    }

    // запросить код для восстановления пароля
    // ---------------
    $scope.requestCode = function(data) {
        beforeSend();

        $scope.smsBlockShow = true;
        $scope.timerForSms = 200;
        countdownTimer();

        loginRqst.requestRestoreCode(utilsSrvc.token.add({ phone: data.phone }))
            .then(function(res) {
                if (res.data.success) {
                    $scope.smsBlockShow = true;
                    $scope.timerForSms = res.data.canResendSms;
                    countdownTimer();
                } else {
                    $scope.er.server = res.data.errorMessage;
                }
            }, function() {
            $scope.er.s404 = true;
        }).finally(function() {
                $scope.isLoading = false;
            });
    }

    // отправить данные для восстановления пароля
    // ---------------
    $scope.restorePasswrod = function (data) {
        data.phone = $scope.f.phone;
        beforeSend();

        $scope.success = true;

        loginRqst.restorePassword(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.success = true;
                } else {
                    $scope.er.server = res.data.errorMessage;
                }
            }, function() {
            $scope.er.s404 = true;
        }).finally(function() {
            $scope.isLoading = false;
        });
    }

    // повторяющиеся действия перед отправкой запроса
    // ---------------
    function beforeSend() {
        $scope.isLoading = true;
        $scope.er.server = '';
        $scope.er.s404 = true;
    }

    // таймер для ввода кода
    // ---------------
    function countdownTimer() {
        var smsTime = $interval(function () {
            if ($scope.timerForSms == 0) {
                $interval.cancel(smsTime);
            } else {
                $scope.timerForSms = $scope.timerForSms - 1;
            }
        }, 1000);
    }
}]);

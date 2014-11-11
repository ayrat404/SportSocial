'use strict';

// Контроллер регистрации пользователя
// ---------------
angular.module('blog').controller('RegistrationCtrl',
    ['$scope',
     '$interval',
     '$window',
     'loginRqst',
     'utilsSrvc',
function ($scope, $interval, $window, registrationRqst, utilsSrvc) {
    $scope.smsBlockShow =   false;  // показать/скрыть смс блок
    $scope.loading      =   false;  // показать/скрыть лоадер
    $scope.disableInp   =   false;  // отключить поля ввода
    $scope.disabledCode =   false;  // отключить поле ввода смс
    $scope.timerForSms  =   0;      // таймер для смс
    $scope.er = {                   // ошибки
        server  :   false,          // сервер не доступен
        custom  :   {               // ошибка с сервера с сообщением
            show    : false,
            msg     : ''
        }
    }


    // Отправка данных, запрос кода подтверждения
    // ---------------------
    $scope.requestCode = function (data) {
        toggleForm(true);
        registrationRqst.requestCode(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $scope.smsBlockShow = true;
                    $scope.timerForSms = res.data.canResendSms;
                    countdownTimer();
                } else {
                    $scope.er.custom.show = true;
                    $scope.er.custom.msg = res.data.errorMessage;
                }
                toggleForm(false);
            }, function() {
                $scope.er.server = true;
                toggleForm(false);
            });
    }

    // Отправка кода подтверждения, окончательная регистрация
    // ---------------------
    $scope.registration = function (data) {
        toggleForm(true);
        data.phone = $scope.user.phone;
        registrationRqst.registration(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $window.location.reload();
                } else {
                    $scope.er.custom.show = true;
                    $scope.er.custom.msg = res.data.errorMessage;
                }
                toggleForm(false);
        }, function () {
                $scope.er.server = true;
                toggleForm(false);
            });
    }

    // Показать/скрыть лоадер, убрать ошибки
    // ---------------------
    function toggleForm(isSend) {
        if (isSend) {
            $scope.disableInp = true;
            $scope.disabledCode = true;
            $scope.loader = true;
            $scope.er.server = false;
            $scope.er.custom.show = false;
        } else {
            $scope.disableInp = false;
            $scope.disabledCode = false;
            $scope.loader = false;
        }
    }

    // Таймер повторной отправки смс
    // ---------------------
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

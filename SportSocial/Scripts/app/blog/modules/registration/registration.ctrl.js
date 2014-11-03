'use strict';

angular.module('blog').controller('RegistrationCtrl',
    ['$scope',
     '$interval',
     'registrationRqst',
     'utilsSrvc',
function ($scope, $interval, registrationRqst, utilsSrvc) {
    $scope.smsBlockShow =   false;  // показать/скрыть смс блок
    $scope.loading      =   false;  // показать/скрыть лоадер
    $scope.disableInp   =   false;  // отключить поля ввода
    $scope.disabledCode =   false;  // отключить поле ввода смс
    $scope.timerForSms  =   0;      // таймер для смс
    $scope.er = {                   // ошибки
        server  :   false,          // сервер не доступен
        custom  :   {               // ошибка с сервера с сообщением
            show: false,
            msg: ''
        }
    }


    // Отправка данных, запрос кода подтверждения
    // ---------------------
    $scope.requestCode = function (data) {
        toggleForm(true);
        data = angular.extend(data, utilsSrvc.token.get().obj);
        registrationRqst.requestCode(data)
            .then(function (res) {
                if (res.data.success) {
                    $scope.smsBlockShow = true;
                    $scope.timerForSms = res.data.canResendSms;
                    countdownTimer();
                } else {
                    $scope.er.custom.show = true;
                    $scope.er.custom.msg = res.data.error;
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
        data = angular.extend(data, utilsSrvc.token.get().obj);
        data.phone = $scope.user.phone;
        registrationRqst.registration(data)
            .then(function (res) {
                if (res.data.success) {
                    $window.location.href = res.data.redirect;
                } else {
                    $scope.er.custom.show = true;
                    $scope.er.custom.msg = res.data.error;
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

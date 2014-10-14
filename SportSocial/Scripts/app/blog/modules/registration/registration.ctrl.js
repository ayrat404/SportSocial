'use strict';

angular.module('app').controller('RegistrationCtrl',
    ['$scope',
     '$interval',
     'registrationRqst',
     'tokenRqst',
function ($scope, $interval, registrationRqst, tokenRqst) {
    $scope.smsBlockShow =   false;  // показать/скрыть смс блок
    $scope.loading      =   false;  // показать/скрыть лоадер
    $scope.serverError  =   false;  // ошибка с сервера
    $scope.disableInp   =   false;  // отключить поля ввода
    $scope.disabledCode =   false;  // отключить поле ввода смс
    $scope.timerForSms  =   0;      // таймер для смс


    // Отправка данных, запрос кода подтверждения
    // ---------------------
    $scope.requestCode = function (data) {
        toggleForm();
        $scope.disableInp = true;
        data = angular.extend(data, tokenRqst.obj);
        registrationRqst.requestCode(data)
            .then(function(res) {
                if (res.success) {
                    $scope.smsBlockShow = true;
                    $scope.timerForSms = res.canResendSms;
                    countdownTimer();
                    toggleForm(false);
                } else {
                    $scope.disableInp = false;
                    handleError(res.error);
                    toggleForm(false);
                }
            }, function() {
                $scope.serverError = true;
                $scope.disableInp = false;
        });
    }

    // Отправка кода подтверждения, окончательная регистрация
    // ---------------------
    $scope.registration = function (data) {
        $scope.disabledCode = true;
        toggleForm(true);
        data = angular.extend(data, tokenRqst.obj);
        registrationRqst.registration(data)
            .then(function (res) {
                if (res.success) {
                    $window.location.href = res.redirect;
                } else {
                    handleError(res.error);
                }
                $scope.disabledCode = false;
                toggleForm(false);
            }, function () {
                $scope.serverError = true;
                $scope.disabledCode = false;
                toggleForm(false);
            });
    }

    // Показать/скрыть лоадер, убрать ошибки
    // ---------------------
    function toggleForm(isSend) {
        if (isSend) {
            $scope.loader = true;
            $scope.serverError = false;
        } else {
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

    // Валидация с сервера
    // ---------------------
    function handleError(error) {
        
    }

}]);

'use strict';

angular.module('app').controller('RegistrationCtrl',
    ['$scope',
     '$interval',
     'registrationRqst',
     'tokenRqst',
function ($scope, $interval, registrationRqst, tokenRqst) {
    $scope.smsBlockShow =   false;  // показать/скрыть смс блок
    $scope.loading      =   false;  // показать/скрыть лоадер
    $scope.disableInp   =   false;  // отключить поля ввода
    $scope.disabledCode =   false;  // отключить поле ввода смс
    $scope.timerForSms  =   0;      // таймер для смс
    $scope.serverError = {          // ошибка с сервера
        isShow  :   false,
        message :   ''
    }


    // Отправка данных, запрос кода подтверждения
    // ---------------------
    $scope.requestCode = function (data) {
        toggleForm(true);
        data = angular.extend(data, tokenRqst.obj);
        registrationRqst.requestCode(data)
            .then(function (res) {
                if (res.data.success) {
                    $scope.smsBlockShow = true;
                    $scope.timerForSms = res.data.canResendSms;
                    countdownTimer();
                } else {
                    showError(res.data.error);
                }
            }, function() {
            showError({ text: 'Что то пошло не так, попробуйте позже' });
        }).finally(function () {
                toggleForm(false);
        });
    }

    // Отправка кода подтверждения, окончательная регистрация
    // ---------------------
    $scope.registration = function (data) {
        toggleForm(true);
        data = angular.extend(data, tokenRqst.obj);
        data.phone = $scope.user.phone;
        registrationRqst.registration(data)
            .then(function (res) {
                if (res.data.success) {
                    $window.location.href = res.data.redirect;
                } else {
                    showError(res.data.error);
                }
            }, function () {
                showError({ text: 'Что то пошло не так, повторите позже' });
            }).finally(function () {
                toggleForm(false);
        });
    }

    // Показать/скрыть лоадер, убрать ошибки
    // ---------------------
    function toggleForm(isSend) {
        if (isSend) {
            $scope.disabledInp = true;
            $scope.disabledCode = true;
            $scope.loader = true;
            $scope.serverError.isShow = false;
        } else {
            $scope.disabledInp = false;
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

    // Валидация с сервера
    // ---------------------
    function showError(error) {
        $scope.serverError.isShow = true;
        $scope.serverError.message = error.text;
    }

}]);

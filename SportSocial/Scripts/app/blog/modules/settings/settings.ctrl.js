'use strict';

angular.module('blog').controller('SettingsCtrl',
    ['$scope',
     'settingsRqst',
     'utilsSrvc',
     '$window',
     '$timeout',
function ($scope, settingsRqst, utilsSrvc, $window, $timeout) {

    // объекты для блока смены пароля
    // ---------------
    $scope.password = {
        fn: {},
        er: {
            s404: false,    // сервер недоступен
            server: ''      // не прошли валидацию на сервере
        },
        success: false      // пароль успешно изменен
    }

    // сменить пароль
    // ---------------
    $scope.password.fn.changePassword = function (data) {
        $scope.password.er.s404 = false;
        $scope.password.er.server = '';
        settingsRqst.changePassword(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.password.success = true;
                    $scope.password.btnIsDisabled = true;
                    $timeout(function() {
                        $scope.password.success = false;
                        $scope.password.btnIsDisabled = false;
                    }, 3000);
                } else {
                    $scope.password.er.server.show = res.data.errorMessage;
                }
            }, function() {
            $scope.password.er.s404 = true;
        });
    }

    // объекты для блока смены номера телефона
    // ---------------
    $scope.phone = {
        fn: {},
        er: {
            s404: false,
            server: ''
        },
        smsBlockShow: false,    // показать блок ввода кода
        timer: 0,               // значение таймера для ввода кода
        success: false          // телефон успешно изменен
    }

    // запросить код подтверждения нового телефона
    // ---------------
    $scope.phone.fn.requestCode = function (data) {
        $scope.phone.er.s404 = false;
        $scope.phone.er.server = '';
        settingsRqst.requestCode(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $scope.phone.smsBlockShow = true;
                    $scope.phone.timer = res.data.canResendSms;
                    countdownTimer();
                    $timeout(function () {
                        $scope.phone.success = false;
                        $scope.phone.btnIsDisabled = false;
                    }, 3000);
                } else {
                    $scope.phone.er.server.show = res.data.errorMessage;
                }
            }, function () {
                $scope.phone.er.s404 = true;
            });
    }

    // отправка телефона и кода подтверждения
    // ---------------
    $scope.phone.fn.confirmPhone = function(data) {
        settingsRqst.confirmCode(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $scope.phone.smsBlockShow = true;
                    $scope.phone.timer = res.data.canResendSms;
                    countdownTimer();
                    $timeout(function () {
                        $scope.phone.success = false;
                        $scope.phone.btnIsDisabled = false;
                    }, 3000);
                } else {
                    $scope.phone.er.server.show = res.data.errorMessage;
                }
            }, function () {
                $scope.phone.er.s404 = true;
            });
    }

    // таймер для ввода кода
    // ---------------
    function countdownTimer() {
        var smsTime = $interval(function () {
            if ($scope.phone.timer == 0) {
                $interval.cancel(smsTime);
            } else {
                $scope.timer = $scope.timer - 1;
            }
        }, 1000);
    }
}]);

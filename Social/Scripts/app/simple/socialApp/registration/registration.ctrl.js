'use strict';

angular.module('socialApp.controllers')
    .controller('RegistrationCtrl', [
        '$scope',
        'mixpanel',
        'registrationSrvc',
        function (
            $scope,
            mixpanel,
            registrationSrvc) {

            $scope.$root.title = 'Fortress | Регистрация';
            $scope.loading = false;

            $scope.step = 1;

            // send first data
            // ---------------
            $scope.sendFirst = function () {
                registrationSrvc.registerFirst($scope.first)
                    .then(function(res) {
                        $scope.step = 2;
                    }, function(res) {

                    });
            }

            // send two data
            // ---------------
            $scope.sendTwo = function () {
                registrationSrvc.registerTwo($scope.two)
                    .then(function(res) {

                    }, function(res) {

                    });
            }

            // mixpanel tracking
            // ----------------
            $scope.$on('$viewContentLoaded', function () {
                mixpanel.ev.visitPage($scope.$root.title);
            });
        }
    ]);
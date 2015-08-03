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
            

            // mixpanel tracking
            // ----------------
            $scope.$on('$viewContentLoaded', function () {
                mixpanel.ev.visitPage($scope.$root.title);
            });
        }
    ]);
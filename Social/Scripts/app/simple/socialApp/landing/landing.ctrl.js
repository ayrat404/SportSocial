'use strict';

angular.module('socialApp.controllers')
    .controller('LandingCtrl', [
        '$scope',
        'mixpanel',
        'registrationSrvc',
        function (
            $scope,
            mixpanel,
            registrationSrvc) {

            $scope.$root.title = 'Fortress | Добро пожаловать';
            $scope.loading = false;

            // mixpanel tracking
            // ----------------
            $scope.$on('$viewContentLoaded', function () {
                mixpanel.ev.visitPage($scope.$root.title);
            });
        }
    ]);
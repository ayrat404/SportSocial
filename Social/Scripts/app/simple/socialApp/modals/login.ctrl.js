'use strict';

angular.module('socialApp.controllers')
    // submit modal
    // ---------------
    .controller('LoginSubmitModalCtrl', [
        '$scope',
        '$state',
        '$modalInstance',
        'loginSrvc',
        function(
            $scope,
            $state,
            $modalInstance,
            loginSrvc) {

            $scope.serverValidation = {};

            // login
            // ---------------
            $scope.submit = function() {
                loginSrvc.logIn($scope.login).then(function (res) {
                    $modalInstance.dismiss();
                    // todo after login success
                }, function (res) {
                    $scope.serverValidation = res.errors;
                });
            }

            // to registration page
            // ---------------
            $scope.toRegistration = function() {
                $modalInstance.close();
                $state.go('registration');
            }
        }
    ]);
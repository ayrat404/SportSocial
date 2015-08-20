'use strict';

angular.module('socialApp.controllers')
    // submit phone modal
    // ---------------
    .controller('RestorePasswordSubmitPhoneModalCtrl', [
        '$scope',
        '$modalInstance',
        'restorePasswordSrvc',
        'modalSrvc',
        function(
            $scope,
            $modalInstance,
            restorePasswordSrvc,
            modalSrvc) {

            $scope.serverValidation = {};
            $scope.submit = function() {
                restorePasswordSrvc.sendPhone({ phone: $scope.phone }).then(function (res) {
                    $modalInstance.close();
                    modalSrvc.show({ name: 'restorePasswordSubmitNewData', data: { phone: $scope.phone } });
                }, function (res) {
                    $scope.serverValidation = res.errors;
                });
            }
        }
    ])
    // submit new password modal
    // ---------------
    .controller('RestorePasswordSubmitNewModalCtrl', [
        '$scope',
        '$modalInstance',
        'loginSrvc',
        'modalSrvc',
        'modalData',
        function(
            $scope,
            $modalInstance,
            restorePasswordSrvc,
            modalSrvc,
            modalData) {

            $scope.serverValidation = {};
            $scope.restore.phone = modalData.phone;     // pass phone from prev modal
            $scope.submit = function () {
                restorePasswordSrvc.sendNewPassword($scope.restore).then(function (res) {
                    $modalInstance.close();
                    modalSrvc.show({ name: 'loginSubmit' });
                }, function(res) {
                    $scope.serverValidation = res.errors;
                });
            }
        }
    ]);
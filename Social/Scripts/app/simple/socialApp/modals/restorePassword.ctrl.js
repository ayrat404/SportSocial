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

            $scope.submit = function() {
                restorePasswordSrvc.sendPhone({ phone: $scope.phone }).then(function (res) {
                    $modalInstance.close();
                    modalSrvc.show({ name: 'restorePasswordSubmitNewData' });
                }, function (res) {
                    if (res.msg) $scope.restoreParamsError = true;
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
        function(
            $scope,
            $modalInstance,
            restorePasswordSrvc,
            modalSrvc) {

            $scope.submit = function () {
                $scope.serverErrorMsg = false;
                restorePasswordSrvc.sendNewPassword({ phone: $scope.phone }).then(function (res) {
                    $modalInstance.close();
                    modalSrvc.show({ name: 'loginSubmit' });
                }, function(res) {
                    if (res.msg)
                        $scope.serverErrorMsg = res.msg;
                });
            }
        }
    ]);
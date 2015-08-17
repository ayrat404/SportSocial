'use strict';

angular.module('socialApp.controllers')
    // submit modal
    // ---------------
    .controller('SupportSubmitModalCtrl', [
        '$scope',
        '$modalInstance',
        'supportSrvc',
        'modalSrvc',
        function(
            $scope,
            $modalInstance,
            supportSrvc,
            modalSrvc) {

            $scope.submit = function() {
                supportSrvc.send($scope.support).then(function(res) {
                    $modalInstance.dismiss();
                    modalSrvc.show({ name: 'supportSuccess' });
                });
            }
        }
    ])

    // success modal
    // ---------------
    .controller('SupportSuccessModalCtrl', [
        '$scope',
        '$modalInstance',
        function(
            $scope,
            $modalInstance) {

            $scope.close = function() {
                $modalInstance.dismiss();
            }
        }
    ]);
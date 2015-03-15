'use strict';

// Контроллер оплаты
// ---------------
angular.module('blog').controller('PaymentCtrl',
    ['$scope',
     'utilsSrvc',
     'paymentRqst',
function ($scope, utilsSrvc, paymentRqst) {

    var desc = {
        1: 'asdasdsd',
        2: '1231231232',
        3: '132213312132',
        4: '45gdsfsdfgsdfgsdfg',
        5: 'asdasdsd',
        6: '1231231232',
        7: '132213312132',
        8: '45gdsfsdfgsdfgsdfg',
        9: 'asdasdsd',
        10: '1231231232',
        11: '132213312132',
        12: '45gdsfsdfgsdfgsdfg',
    }

    $scope.tarif = 0;
    $scope.fn = {};

    // change description at slider
    // ---------------
    $scope.$watch('tarif', function (val) {
            $scope.sliderText = desc[val];
    });

    // выбрать тариф
    // ---------------
    $scope.fn.selectTarif = function () {
        //$scope.password.er.s404 = false;
        //$scope.password.er.server = '';
        paymentRqst.selectTarif(utilsSrvc.token.add({ product: $scope.tarif }))
            .then(function (res) {
                if (res.data.success) {
                    //$scope.password.success = true;
                    //$scope.password.btnIsDisabled = true;
                    $timeout(function () {
                        //$scope.password.success = false;
                        //$scope.password.btnIsDisabled = false;
                    }, 3000);
                } else {
                    //$scope.password.er.server = res.data.errorMessage;
                }
            }, function () {
                //$scope.password.er.s404 = true;
            });
    }

    

}]);

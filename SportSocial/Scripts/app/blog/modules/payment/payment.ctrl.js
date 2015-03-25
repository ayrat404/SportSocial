'use strict';

// Контроллер оплаты
// ---------------
angular.module('blog').controller('PaymentCtrl',
    ['$scope',
     'utilsSrvc',
     'paymentRqst',
function ($scope, utilsSrvc, paymentRqst) {

    // tarifs description
    // ---------------
    var desc = {
        1: 'asdas<br/>dsd',
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

    // change description at slider
    // ---------------
    $scope.$watch('tarif', function (val) {
            $scope.sliderText = desc[val];
    });

}]);

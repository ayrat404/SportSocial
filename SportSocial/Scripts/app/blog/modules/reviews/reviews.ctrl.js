'use strict';

// Контроллер для лайков
// ---------------
angular.module('blog').controller('ReviewsCtrl',
    ['$scope',
     'reviewsRqst',
     'utilsSrvc',
function ($scope, reviewsRqst, utilsSrvc) {
    $scope.m = {};

    $scope.minlength = 100; // review minlength

    $scope.types = [
        { name: 'Предложить идею', id: 1 },
        { name: 'Задать вопрос', id: 2 },
        { name: 'Оставить благодарность', id: 3 }
    ];
    $scope.m.type = 1;


    // create review
    // ---------------
    $scope.createReview = function(data) {
        reviewsRqst.create(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.success) {
                    // prepend to list
                }
            });
    }

}]);

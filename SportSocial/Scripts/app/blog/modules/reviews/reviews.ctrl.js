'use strict';

// Контроллер для лайков
// ---------------
angular.module('blog').controller('ReviewsCtrl',
    ['$scope',
     'reviewsRqst',
     'utilsSrvc',
function ($scope, reviewsRqst, utilsSrvc) {

    $scope.minlength = 100; // review minlength

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

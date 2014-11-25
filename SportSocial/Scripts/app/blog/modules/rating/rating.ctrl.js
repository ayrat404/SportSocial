'use strict';

// Контроллер для лайков
// ---------------
angular.module('blog').controller('RatingCtrl',
    ['$scope',
     'ratingRqst',
     'utilsSrvc',
function ($scope, ratingRqst, utilsSrvc) {

    // отправка запроса
    // ---------------
    $scope.changeRating = function (action) {
        var data = {
            id          :   $scope.id,  // id сущности
            actionType  :   action,     // лайк или дислайк
            entityType  :   $scope.type // тип сущности
        }
        ratingRqst.send(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    if (action == 'like') {
                        $scope.count = $scope.count + 1;
                        $scope.isLiked = 'True';
                    } else {
                        $scope.count = $scope.count - 1;
                        $scope.isDisliked = 'True';
                    }
                }
            });
    }
}]);

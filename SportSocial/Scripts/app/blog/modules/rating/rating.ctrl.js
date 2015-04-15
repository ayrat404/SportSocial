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
    $scope.changeRating = function (action, isLiked, isDisliked) {
        var data = {
            id          :   $scope.id,  // id сущности
            actionType  :   action,     // лайк или дислайк
            entityType  :   $scope.type // тип сущности
        }
        ratingRqst.send(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    if (action == 'like') {
                        if (isDisliked) {
                            $scope.count = $scope.count + 2;
                        } else if (isLiked) {
                            $scope.count = $scope.count - 1;
                        } else {
                            $scope.count = $scope.count + 1;
                        }
                        $scope.isLiked = 'True';
                        $scope.isDisliked = 'False';
                    } else {
                        if (isLiked) {
                            $scope.count = $scope.count - 2;
                        } else if (isDisliked) {
                            $scope.count = $scope.count + 1;
                        } else {
                            $scope.count = $scope.count - 1;
                        }
                        $scope.isDisliked = 'True';
                        $scope.isLiked = 'False';
                    }
                }
            });
    }
}]);

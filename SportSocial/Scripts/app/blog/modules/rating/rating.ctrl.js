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
                        var tmpIsLiked = 'True';
                        if (isDisliked == 'True') {
                            $scope.count = $scope.count + 2;
                        } else if (isLiked == 'True') {
                            $scope.count = $scope.count - 1;
                            tmpIsLiked = 'False';
                        } else {
                            $scope.count = $scope.count + 1;
                        }
                        $scope.isLiked = tmpIsLiked;
                        $scope.isDisliked = 'False';
                    } else {
                        var tmpIsDisliked = 'True';
                        if (isLiked == 'True') {
                            $scope.count = $scope.count - 2;
                        } else if (isDisliked == 'True') {
                            $scope.count = $scope.count + 1;
                            tmpIsDisliked = 'False';
                        } else {
                            $scope.count = $scope.count - 1;
                        }
                        $scope.isDisliked = tmpIsDisliked;
                        $scope.isLiked = 'False';
                    }
                }
            });
    }
}]);

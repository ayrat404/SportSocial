'use strict';

// Контроллер добавления комментариев
// ---------------
angular.module('blog').controller('LikeDislikeCtrl',
    ['$scope',
     'likeDislikeRqst',
     'utilsSrvc',
     '$window',
     '$timeout',
function ($scope, likeDislikeRqst, utilsSrvc, $window, $timeout) {
    var data = {
        id: $scope.id,
        entityType: $scope.type
    };

    // отправка запроса
    // ---------------
    $scope.changeRating = function (action) {
        data.actionType = action;
        likeDislikeRqst.send(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    if (action == 'like') {
                        $scope.count = $scope.count + 1;
                        //changeCount(+$scope.count + 1);
                        $scope.isLiked = 'true';
                    } else {
                        $scope.count = $scope.count - 1;
                        //changeCount(+$scope.count - 1);
                        $scope.isDisliked = 'true';
                    }
                    $scope.isRated = true;
                }
            });
    }

    // изменение счетчика с анимацией
    // ---------------
    //function changeCount(count) {
    //    var classIn,
    //        classOut;
    //    if (count < $scope.count) {
    //        classIn = 'fadeInDown';
    //        classOut = 'fadeOutDown';
    //    } else {
    //        classIn = 'fadeInUp';
    //        classOut = 'fadeOutUp';
    //    }
    //    utilsSrvc.animation.add(elms.$count, classOut, function () {
    //        $scope.count = count;
    //        $scope.$digest();
    //        utilsSrvc.animation.add(elms.$count, classIn);
    //    });
    //}
}]);

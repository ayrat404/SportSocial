﻿'use strict';

// лайки/дизлайки
// ---------------
angular
    .module('blog')
    .directive('likeDislike', ['likeDislikeRqst', 'utilsSrvc', function (likeDislikeRqst, utilsSrvc) {
        return {
            restrict: 'A',
            replace: true,
            templateUrl: '/Scripts/templates/blog/article/like-dislike.html',
            scope: {
                count       :   '@',    // суммарное количество лайков/дислайков (like-dislike)
                type        :   '@',    // тип (статья, комментарий)
                id          :   '@',    // id сущности
                isLiked     :   '@',    // уже был поставлен лайк
                isDisliked  :   '@'     // уже был поставлен дислайк
            },
            link: function (scope, element, attrs) {
                var data = {
                        id: scope.id,
                        type: scope.type
                    },
                    $el = angular.element(element).find('.js-count');

                // отправка запроса
                // ---------------
                scope.changeRating = function (action) {
                    data.action = action;
                    likeDislikeRqst.send(utilsSrvc.token.add(data))
                        .then(function(res) {
                            if (res.data.success) {
                                if (action == 'like') {
                                    changeCount(+scope.count + 1);
                                } else {
                                    changeCount(+scope.count - 1);
                                }
                            }
                        });
                }

                // изменение счетчика с анимацией
                // ---------------
                function changeCount(count) {
                    var classIn,
                        classOut;
                    if (count < scope.count) {
                        classIn = 'fadeInDown';
                        classOut = 'fadeOutDown';
                    } else {
                        classIn = 'fadeInUp';
                        classOut = 'fadeOutUp';
                    }
                    utilsSrvc.animation.add($el, classOut, function () {
                        scope.count = count;
                        scope.$digest();
                        utilsSrvc.animation.add($el, classIn);
                    });
                }
            }
        }
    }]);

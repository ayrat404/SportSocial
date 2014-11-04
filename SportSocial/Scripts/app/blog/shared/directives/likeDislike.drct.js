'use strict';

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
                like        :   '@',    // количество лайков
                dislike     :   '@',    // количество дислайков
                type        :   '@',    // тип (статья, комментарий)
                id          :   '@',    // id сущности
                isLiked     :   '@',    // уже был поставлен лайк
                isDisliked  :   '@'     // уже был поставлен дислайк
            },
            link: function (scope, element, attrs) {
                var data = {
                    id: scope.id,
                    type: scope.type
                };

                // like/dislike
                // ---------------
                scope.rating = function(action) {
                    data.action = action;
                    changeRating();
                }

                // отправка запроса
                // ---------------
                function changeRating() {
                    likeDislikeRqst.send(utilsSrvc.token.add(data))
                        .then(function(res) {
                            if (res.data.success) {
                                if (data.action === 'like') {
                                    scope.like = res.data.status ? scope.like - 1 : scope.like + 1;
                                } else if (data.action === 'dislike') {
                                    scope.dislike = res.data.status ? scope.dislike - 1 : scope.dislike + 1;
                                }
                            }
                        });
                }
            }
        }
    }]);

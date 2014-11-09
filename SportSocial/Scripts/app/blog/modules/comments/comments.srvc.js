'use strict';

angular.module('blog').factory('commentsRqst', ['$http', 'serializeObj', function ($http) {
    var send = function (action, obj) {
        return $http({
            method: 'POST',
            url: '/Comments/' + action,
            data: obj
        });
    };
    var get = function (action, obj) {
        return $http({
            method: 'GET',
            url: '/Comments/' + action,
            params: obj
        });
    }
    return {
        // Создание комментария
        // ---------------
        createComment: function (obj) {
            return send('Comment', obj);
        },
        // Загрузка всех комментариев
        // ---------------
        loadComments: function (obj) {
            return get('LoadComments', obj);
        }
    };
}]);

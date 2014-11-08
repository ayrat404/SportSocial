'use strict';

angular.module('blog').factory('articleRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (url, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Blog/' + url,
            data    :   obj
        });
    };
    return {
        // Создание статьи
        // --------------
        createArticle: function(obj) {
            return send('New', obj);
        },
        // Создание комментария
        // ---------------
        createComment: function(obj) {
            return send('Comment', obj);
        },
        // Загрузка комментариев
        // ---------------
        loadComments: function(obj) {
            return send('LoadComments', obj);
        }
    };
}]);

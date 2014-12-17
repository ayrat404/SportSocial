'use strict';

angular.module('blog').factory('articleRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (action, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Blog/' + action,
            data    :   obj
        });
    };

    // в отдельный модуль, когда понадобится ещё в одном месте
    var getYoutubeImg = function(obj) {
        return $http({
            method: 'POST',
            url: '/File/youtubeurl',
            data: obj
        });
    }
    return {
        // Создание статьи
        // --------------
        createArticle: function(obj) {
            return send('New', obj);
        },
        // Редактирование статьи
        // ---------------
        editArticle: function(obj) {
            return send('Edit', obj);
        },
        // Загрузка видео
        // ---------------
        getYoutubeImg: function (obj) {
            return getYoutubeImg(obj);
        }
    };
}]);

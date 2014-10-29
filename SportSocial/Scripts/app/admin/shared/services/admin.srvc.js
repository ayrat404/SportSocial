'use strict';

angular.module('admin').factory('adminRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (url, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Admin/' + url,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    return {
        // Получить список статей
        // --------------
        getArticles: function(obj) {
            return send('GetArticles', obj);
        },
        // Опубликовать статью
        // ---------------
        publishArticle: function(obj) {
            return send('PublishArticle', obj);
        },
        // Отклонить статью
        // ---------------
        rejectArticle: function(obj) {
            return send('RejectArticle', obj);
        },
        // Получить список конференций
        // --------------
        getConferences: function(obj) {
            return send('getConferences', obj);
        },
        // Создать конференцию
        // ---------------
        createConference: function(obj) {
            return send('CreateConference', obj);
        }
    };
}]);

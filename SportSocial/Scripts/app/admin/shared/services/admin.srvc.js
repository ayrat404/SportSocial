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
        // Изменить статус статьи
        // ---------------
        changeArticleStatus: function (obj) {
            return send('ChangeArticleStatus', obj);
        },
        // Получить список конференций
        // --------------
        getConferences: function (obj) {
            return send('GetConferences', obj);
        },
        // Создать конференцию
        // ---------------
        createConference: function(obj) {
            return send('CreateConference', obj);
        },
        // Изменить статус конференции
        // ---------------
        changeConferenceStatus: function(obj) {
            return send('ChangeConferenceStatus', obj);
        }
    };
}]);

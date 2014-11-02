'use strict';

angular.module('admin').factory('adminRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (action, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Admin/' + action,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };

    var get = function(action, obj) {
        return $http({
            method  :   'GET',
            url     :   '/Admin/' + action,
            params  :   obj,
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }

    return {
        // Получить список статей
        // --------------
        getArticles: function(obj) {
            return get('GetArticles', obj);
        },
        // Изменить статус статьи
        // ---------------
        changeArticleStatus: function (obj) {
            return send('ChangeArticleStatus', obj);
        },
        // Получить список конференций
        // --------------
        getConferences: function (obj) {
            return get('GetConferences', obj);
        },
        // Создать конференцию
        // ---------------
        createConference: function(obj) {
            return send('CreateConference', obj);
        },
        // Редактировать конференцию
        // ---------------
        editConference: function (obj) {
            return send('EditConference', obj);
        },
        // Изменить статус конференции
        // ---------------
        changeConferenceStatus: function(obj) {
            return send('ChangeConferenceStatus', obj);
        }
    };
}]);

'use strict';

angular.module('blog').factory('conferenceRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (url, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Conference/' + url,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    var get = function(url, obj) {
        return $http({
            method: 'GET',
            url: '/Conference/' + url,
            data: serializeObj(obj),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }
    return {
        // Запрос времени до начала конференции
        // --------------
        requestTime: function(obj) {
            return get('Time', obj);
        }
    };
}]);

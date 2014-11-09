'use strict';

angular.module('blog').factory('conferenceRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (action, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Conference/' + action,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    var get = function(action, obj) {
        return $http({
            method: 'GET',
            url: '/Conference/' + action,
            params: serializeObj(obj),
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

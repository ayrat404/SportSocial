'use strict';

angular.module('app').factory('conferenceRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (url, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Conference/' + url,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    return {
        // Запрос времени до начала конференции
        // --------------
        requestTime: function(obj) {
            return send('Time', obj);
        }
    };
}]);

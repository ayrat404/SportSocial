'use strict';

angular.module('app').factory('authorizationRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (url, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Login/' + url,
            data    :   serializeObj(obj),
            headers :   { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    return {
        // Авторизация
        // --------------
        signIn: function(obj) {
            return send('SignIn', obj);
        }
    };
}]);

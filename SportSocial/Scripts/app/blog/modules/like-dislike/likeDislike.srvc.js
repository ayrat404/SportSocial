'use strict';

// лайки/дизлайки
// ---------------
angular
    .module('blog')
    .factory('likeDislikeRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
        var send = function (action, obj) {
            return $http({
                method: 'POST',
                url: '/Rating/' + action,
                data: serializeObj(obj),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }

        return {
            send: function (obj) {
                return send('Rate', obj);
            }
        }
    }]);

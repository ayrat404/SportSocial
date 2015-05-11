'use strict';

// лайки/дизлайки
// ---------------
angular
    .module('blog')
    .factory('reviewsRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
        var send = function (action, obj) {
            return $http({
                method: 'POST',
                url: '/Review/' + action,
                data: serializeObj(obj),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }

        return {
            create: function (obj) {
                return send('Create', obj);
            }
        }
    }]);

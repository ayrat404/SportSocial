'use strict';

angular.module('blog').factory('feedbackRqst', ['$http', 'serializeObj', function ($http, serializeObj) {
    var send = function (action, obj) {
        return $http({
            method  :   'POST',
            url     :   '/Feedback/' + action,
            data    :   obj
        });
    };
    return {
        // Отправка отзыва
        // --------------
        sendFeedback: function(obj) {
            return send('Send', obj);
        }
    };
}]);

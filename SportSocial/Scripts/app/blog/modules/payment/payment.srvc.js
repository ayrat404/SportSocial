'use strict';

angular.module('blog').factory('paymentRqst', ['$http', 'serializeObj', function ($http) {
    var send = function (action, obj) {
        return $http({
            method: 'POST',
            url: '/pay/' + action,
            data: obj
        });
    };
    return {
        // Выбор тарифа
        // ---------------
        selectTarif: function(obj) {
            return send('Index', obj);
        }
    };
}]);

'use strict';

angular.module('blog').factory('bonusRqst', ['$http', 'serializeObj', function ($http, serializeObj) {

    var get = function(action, obj) {
        return $http({
            method: 'GET',
            url: '/Bonus/' + action,
            data: obj
        });
    }

    return {
        getVideo: function(obj) {
            get('GetVideo', obj);
        }
    };
}]);

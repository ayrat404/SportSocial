'use strict';

angular.module('app').factory('serializeObj', [function () {
    var serialize = function (obj, combineSameNamed) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                if (combineSameNamed && Object.prototype.toString.call(obj[p]) === '[object Array]') {
                    for (var i = 0; i < obj[p].length; i++) {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p][i]));
                    }
                } else {
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
            }
        return str.join("&");
    };

    return serialize;
}]);
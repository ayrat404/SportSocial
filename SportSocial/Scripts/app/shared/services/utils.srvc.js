'use strict';

angular.module('shared').factory('utilsSrvc', [function () {
    var tokenName = '__RequestVerificationToken',
        tokenEl = angular.element('input[type="hidden"][name=' + tokenName + ']'),
        tokenVal = tokenEl.length ? tokenEl.val() : null,
        tokenObj = {};
    tokenObj[tokenName] = tokenVal;

    // Получить antiforgery token
    // ---------------
    function getToken() {
        return {
            obj: tokenObj,
            val: tokenVal
        }
    }

    // Добавить antiforgery token
    // ---------------
    function addToken(obj) {
        return angular.extend(obj, tokenObj);
    }


    return {
        token: {
            get: getToken,
            add: addToken
        }
    }
}]);
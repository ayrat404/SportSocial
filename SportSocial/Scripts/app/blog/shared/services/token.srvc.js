'use strict';

angular.module('app').factory('tokenRqst', [ function () {
    var tokenName = '__RequestVerificationToken',
        tokenEl = angular.element('input[type="hidden"][name=' + tokenName + ']'),
        tokenVal = tokenEl.length ? tokenEl.val() : null,
        tokenObj = {};
    tokenObj[tokenName] = tokenVal;
    return {
        obj :   tokenObj,
        val :   tokenVal
    }
}]);
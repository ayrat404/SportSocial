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

    // Работа с анимацей (animate.css)
    // ---------------
    function addAnimation($el, x, callback) {
        $el.addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass(x);
            if (callback != undefined) {
                callback();
            }
        });
    }

    // Склонение слов
    // ---------------
    function formatWords(count, words) {
        var cnt = count.toString().substring(count.toString().length - 1, count.toString().length);
        if (cnt == 1) {
            return words[0];
        } else if (cnt > 1 && cnt < 5) {
            return words[1];
        } else return words[2];
    }

    return {
        token: {
            get: getToken,
            add: addToken
        },
        animation: {
            add: addAnimation
        },
        format: {
            words: formatWords
        }
    }
}]);
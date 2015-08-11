'use strict';

// Фильтр для склонения слова
// ---------------
angular.module('shared').filter('formatWord', ['helpersSrvc', function (helpersSrvc) {
    return function (count, words, withNumber) {
        if (withNumber == true) {
            return count + ' ' + helpersSrvc.format.word(count, words);
        }
        return helpersSrvc.format.word(count, words);
    }
}]);
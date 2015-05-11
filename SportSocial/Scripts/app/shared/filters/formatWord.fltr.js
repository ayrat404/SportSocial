'use strict';

// Фильтр для склонения слова
// ---------------
angular.module('shared').filter('formatWord', ['utilsSrvc', function (utilsSrvc) {
    return function (count, words, withNumber) {
        if (withNumber == true) {
            return count + ' ' + utilsSrvc.format.word(count, words);
        }
        return utilsSrvc.format.word(count, words);
    }
}]);
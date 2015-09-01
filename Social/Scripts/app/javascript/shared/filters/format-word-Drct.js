var formatWord;

formatWord = (function() {
  function formatWord(base) {
    return function(count, words, withNumber) {
      if (withNumber === true) {
        return count + ' ' + base.format.word(count, words);
      }
      return base.format.word(count, words);
    };
  }

  return formatWord;

})();

angular.module('shared').filter('formatWord', ['base', formatWord]);

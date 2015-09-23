(function(){
var cut;

cut = (function() {
  function cut() {
    (function(value, wordwise, max, tail) {
      var lastspace;
      if (!value) {
        return '';
      }
      max = parseInt(max, 10);
      if (!max) {
        return value;
      }
      if (value.length <= max) {
        return value;
      }
      value = value.substr(0, max);
      if (wordwise) {
        lastspace = value.lastIndexOf(' ');
        if (lastspace !== -1) {
          value = value.substr(0, lastspace);
        }
      }
      return value + (tail || ' ...');
    });
  }

  return cut;

})();

angular.module('shared').filter('cut', [cut]);

})();
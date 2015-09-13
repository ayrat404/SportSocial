var ngAutofocus;

ngAutofocus = (function() {
  function ngAutofocus($timeout) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        return attrs.$observe('ngAutofocus', function(val) {
          if (val) {
            return $timeout(function() {
              return element[0].focus();
            });
          }
        });
      }
    };
  }

  return ngAutofocus;

})();

angular.module('shared').directive('ngAutofocus', ['$timeout', ngAutofocus]);

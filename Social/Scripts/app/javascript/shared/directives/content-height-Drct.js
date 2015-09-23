(function(){
var contentHeight;

contentHeight = (function() {
  function contentHeight($window, $timeout) {
    var defaults, elHeight;
    defaults = {
      offsetEl: '.social__header'
    };
    elHeight = function($el, offset) {
      var $offset, offsetHeight, wHeight;
      wHeight = angular.element($window).height();
      $offset = offset.split(',');
      offsetHeight = 0;
      if ($offset.length) {
        angular.forEach($offset, function(val, key) {
          return offsetHeight += angular.element(val).outerHeight();
        });
      }
      if (angular.element(defaults.offsetEl).length) {
        offsetHeight += angular.element(defaults.offsetEl).outerHeight();
      }
      $el.css('height', '');
      if ($el.outerHeight() < wHeight) {
        return $el.css('min-height', wHeight - offsetHeight);
      } else {
        return $el.css('min-height', $el.outerHeight());
      }
    };
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        return $timeout(function() {
          var $el, listener;
          $el = angular.element(element);
          elHeight($el, attrs.contentHeight);
          listener = function() {
            return elHeight($el, attrs.contentHeight);
          };
          angular.element($window).on('resize', listener);
          return scope.$on('$destroy', function() {
            return angular.element($window).off('resize', listener);
          });
        }, 50);
      }
    };
  }

  return contentHeight;

})();

angular.module('shared').directive('contentHeight', ['$window', '$timeout', contentHeight]);

})();
(function(){
var mousewheelScroller;

mousewheelScroller = (function() {
  function mousewheelScroller($timeout) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var $el, listener;
        $el = angular.element(element);
        listener = function(e) {
          var scrollTop;
          scrollTop = $el.scrollTop();
          if (e.originalEvent.type === 'touchmove') {
            console.log('touch');
          } else {
            if (e.originalEvent.wheelDeltaY.toString().slice(0, 1) === '-') {
              $el.scrollTop(scrollTop + 100);
            } else {
              $el.scrollTop(scrollTop - 100);
            }
          }
          if ($el.scrollTop() !== 0) {
            return e.preventDefault();
          }
        };
        $el.on('mousewheel', listener);
        return scope.$on('$destroy', function() {
          return element.off('mousewheel', listener);
        });
      }
    };
  }

  return mousewheelScroller;

})();

angular.module('shared').directive('mousewheelScroller', ['$timeout', mousewheelScroller]);

})();
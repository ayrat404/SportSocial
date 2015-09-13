var scrollToCurPos;

scrollToCurPos = (function() {
  function scrollToCurPos() {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        return attrs.$observe('scrollToCurPos', function(val) {
          if (val) {
            return $('html, body').animate({
              scrollTop: $(element).offset().top
            }, 'slow');
          }
        });
      }
    };
  }

  return scrollToCurPos;

})();

angular.module('shared').directive('scrollToCurPos', [scrollToCurPos]);

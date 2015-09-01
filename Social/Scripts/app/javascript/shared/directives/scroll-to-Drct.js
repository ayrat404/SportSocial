var scrollTo;

scrollTo = (function() {
  function scrollTo() {
    var defaults, navSelector;
    navSelector = '.js-nav';
    defaults = {
      speed: 600,
      offset: 0
    };
    scrollTo = function(to, offset) {
      if ($(to).length) {
        offset = angular.extend(defaults.offset, offset);
        if ($(navSelector).length) {
          offset += $(navSelector).outerHeight();
        }
        return $('html, body').animate({
          scrollTop: $(to).offset().top - offset
        }, defaults.speed);
      }
    };
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var $el;
        $el = $(element);
        return $el.on('click', function(e) {
          e.preventDefault();
          return scrollTo(attrs.scrollTo, attrs.offset);
        });
      }
    };
  }

  return scrollTo;

})();

angular.module('shared').directive('scrollTo', [scrollTo]);

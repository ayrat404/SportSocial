(function(){
var loader;

loader = (function() {
  function loader() {
    var containerClass, delay, loaderWrap, timer;
    containerClass = 'loader-container';
    loaderWrap = 'loader-wrap';
    delay = 0;
    timer = null;
    return {
      restrict: 'A',
      scope: {
        loader: '='
      },
      link: function(scope, element, attrs) {
        return scope.$watch('loader', function(newVal, oldVal) {
          var $el, $loader;
          if (newVal !== void 0) {
            $el = angular.element(element);
            if (newVal === true) {
              $loader = angular.element('<div>', {
                "class": loaderWrap
              }).append(angular.element('<div>', {
                "class": 'loader'
              }).append(angular.element('<div>', {
                "class": 'inner one'
              }), angular.element('<div>', {
                "class": 'inner two'
              }), angular.element('<div>', {
                "class": 'inner three'
              })));
              $el.addClass(containerClass);
              $el.append($loader);
              return $loader.show('slow');
            } else if (newVal === false) {
              $loader = $el.children('.' + loaderWrap);
              $el.removeClass(containerClass);
              if ($loader.length) {
                return $loader.remove();
              }
            }
          }
        });
      }
    };
  }

  return loader;

})();

angular.module('shared').directive('loader', [loader]);

})();
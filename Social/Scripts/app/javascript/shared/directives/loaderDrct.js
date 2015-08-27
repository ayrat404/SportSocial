(function(){
var loader;

loader = (function() {
  function loader() {
    var containerClass, spinnerClass, spinnerText, wrapClass;
    spinnerClass = 'loader timer-loader';
    spinnerText = 'Загрузка...';
    containerClass = 'loader-container';
    wrapClass = 'loader-wrap';
    return {
      restrict: 'A',
      scope: {
        loader: '='
      },
      link: function(scope, element, attrs) {
        return scope.$watch('loader', function(newVal, oldVal) {
          var $el;
          if (newVal !== oldVal) {
            $el = angular.element(element);
            if (newVal === true) {
              $el.addClass(containerClass);
              return $el.append(angular.element('<div>', {
                "class": wrapClass
              }).append(angular.element('<div>', {
                "class": spinnerClass,
                text: spinnerText
              })));
            } else if (newVal === false) {
              $el.removeClass(containerClass);
              return $el.find('.' + wrapClass).remove();
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
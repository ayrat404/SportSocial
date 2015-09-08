var WallVertical;

WallVertical = (function() {
  function WallVertical() {
    return {
      restrict: 'A',
      controller: function() {},
      controllerAs: 'wall',
      link: function(scope, element, attrs) {
        return console.log('wall directive');
      }
    };
  }

  return WallVertical;

})();

angular.module('socialApp.directives').directive('wallVertical', [WallVertical]);

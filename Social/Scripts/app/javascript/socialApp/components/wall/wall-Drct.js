var WallVertical;

WallVertical = (function() {
  function WallVertical() {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {}
    };
  }

  return WallVertical;

})();

angular.module('socialApp.directives').directive('wallVertical', [WallVertical]);

var WallVertical;

WallVertical = (function() {
  function WallVertical() {
    return {
      restrict: 'A',
      controller: function() {
        return this.toggleEdit = function(it, isOwner) {
          if (isOwner) {
            if (it.editing !== true) {
              it.editing = true;
            } else {
              it.editing = false;
            }
          }
          debugger;
        };
      },
      controllerAs: 'wall',
      link: function(scope, element, attrs) {
        return console.log('wall directive');
      }
    };
  }

  return WallVertical;

})();

angular.module('socialApp.directives').directive('wallVertical', [WallVertical]);

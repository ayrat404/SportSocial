var WallVertical;

WallVertical = (function() {
  function WallVertical(modalService) {
    return {
      restrict: 'A',
      controller: function() {
        var _this;
        _this = this;
        return _this.remove = function(id) {
          return modalService.show({
            name: 'journalRemove',
            data: {
              id: id
            }
          });
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

angular.module('socialApp.directives').directive('wallVertical', ['modalService', WallVertical]);

(function(){
var modalClick;

modalClick = (function() {
  function modalClick(modalService) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var listener, modalItem;
        modalItem = attrs.modalClick;
        listener = function() {
          return modalService.show({
            name: modalItem,
            data: eval('(' + attrs.opts + ')')
          });
        };
        element.on('click', listener);
        return scope.$on('$destroy', function() {
          return element.off('click', listener);
        });
      }
    };
  }

  return modalClick;

})();

angular.module('shared').directive('modalClick', ['modalService', modalClick]);

})();
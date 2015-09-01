var modalClick;

modalClick = (function() {
  function modalClick(modalService) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var modalItem;
        modalItem = attrs.modalClick;
        element.on('click', function() {
          return modalService.show({
            name: modalItem
          });
        });
      }
    };
  }

  return modalClick;

})();

angular.module('shared').directive('modalClick', ['modalService', modalClick]);

var mediaShowModal;

mediaShowModal = (function() {
  function mediaShowModal(modalService) {
    return {
      restrict: 'A',
      scope: {
        media: '@id',
        index: '@'
      },
      link: function(scope, element, attrs) {
        var listener;
        listener = function() {
          return modalService.show({
            name: 'mediaShow',
            data: {
              media: scope.media,
              index: +scope.index + 1
            }
          });
        };
        element.on('click', listener);
        return scope.$on('$destroy', function() {
          return element.off('click', listener);
        });
      }
    };
  }

  return mediaShowModal;

})();

angular.module('shared').directive('mediaShowModal', ['modalService', mediaShowModal]);

var JournalModalRemove;

JournalModalRemove = (function() {
  function JournalModalRemove($scope, $modalInstance, journalService, modalData) {
    $scope.remove = function() {
      return journalService.remove(modalData.id).then(function(res) {
        $modalInstance.close();
        if (typeof modalData.success === 'function') {
          return modalData.success(res);
        }
      });
    };
    $scope.close = function() {
      return $modalInstance.close();
    };
  }

  return JournalModalRemove;

})();

angular.module('socialApp.controllers').controller('journalModalRemoveController', ['$scope', '$modalInstance', 'journalService', 'modalData', JournalModalRemove]);

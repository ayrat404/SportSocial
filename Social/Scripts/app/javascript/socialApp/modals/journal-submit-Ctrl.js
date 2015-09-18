var JournalModalSubmit;

JournalModalSubmit = (function() {
  function JournalModalSubmit($scope, $modalInstance, journalService, modalData) {
    $scope.j = {
      text: '',
      tags: [],
      media: []
    };
    if (modalData && modalData.model) {
      angular.copy(modalData.model, $scope.j);
    }
    $scope.removeMedia = function(item) {
      var index;
      index = $scope.j.media.indexOf(item);
      if (index !== -1) {
        return $scope.j.media.splice(index, 1);
      }
    };
    $scope.submit = function() {
      return journalService.submit($scope.j).then(function(res) {
        if (modalData && typeof modalData.success === 'function') {
          modalData.success($scope.j);
        }
        return $modalInstance.close();
      });
    };
  }

  return JournalModalSubmit;

})();

angular.module('socialApp.controllers').controller('journalModalSubmitController', ['$scope', '$modalInstance', 'journalService', 'modalData', JournalModalSubmit]);

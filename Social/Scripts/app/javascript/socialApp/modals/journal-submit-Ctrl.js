var JournalModalSubmit;

JournalModalSubmit = (function() {
  function JournalModalSubmit($scope, $modalInstance, journalService) {
    $scope.j = {
      text: '',
      themes: [],
      media: []
    };
    $scope.removeMedia = function(item) {
      var index;
      index = $scope.j.media.indexOf(item);
      if (index !== -1) {
        return $scope.j.media.splice(index, 1);
      }
    };
    $scope.submit = function() {
      debugger;
      return journalService.submit($scope.j).then(function(res) {
        return $modalInstance.close();
      });
    };
  }

  return JournalModalSubmit;

})();

angular.module('socialApp.controllers').controller('journalModalSubmitController', ['$scope', '$modalInstance', 'journalService', JournalModalSubmit]);

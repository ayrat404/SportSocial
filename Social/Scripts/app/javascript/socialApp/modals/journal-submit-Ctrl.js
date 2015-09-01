var JournalModalSubmit;

JournalModalSubmit = (function() {
  function JournalModalSubmit($scope, $modalInstance, journalSubmitService) {
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
      return journalSubmitService.submit($scope.j).then(function(res) {
        return $modalInstance.close();
      }, function(res) {});
    };
  }

  return JournalModalSubmit;

})();

angular.module('socialApp.controllers').controller('journalModalSubmitController', ['$scope', '$modalInstance', 'journalSubmitService', JournalModalSubmit]);

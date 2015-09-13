var JournalProfileSubmit;

JournalProfileSubmit = (function() {
  function JournalProfileSubmit($scope, journalService) {
    var takeWatcher;
    $scope.j = {
      text: '',
      themes: [],
      media: []
    };
    takeWatcher = function() {
      var textWatcher;
      return textWatcher = $scope.$watch('j.text', function(oldVal, newVal) {
        if (newVal && newVal.length > 3 && oldVal !== newVal) {
          $scope.open = true;
          return textWatcher();
        }
      });
    };
    $scope.open = false;
    takeWatcher();
    $scope.closeForm = function() {
      $scope.open = false;
      $scope.resetForm();
      return takeWatcher();
    };
    $scope.removeMedia = function(item) {
      var index;
      index = $scope.j.media.indexOf(item);
      if (index !== -1) {
        return $scope.j.media.splice(index, 1);
      }
    };
    $scope.resetForm = function() {
      $scope.j.text = '';
      $scope.j.themes = [];
      return $scope.j.media = [];
    };
    $scope.submit = function() {
      return journalService.submit($scope.j).then(function(res) {
        $scope.closeForm();
        return $scope.success({
          $res: res
        });
      }, function(res) {});
    };
  }

  return JournalProfileSubmit;

})();

angular.module('socialApp.controllers').controller('journalProfileSubmitController', ['$scope', 'journalService', JournalProfileSubmit]);

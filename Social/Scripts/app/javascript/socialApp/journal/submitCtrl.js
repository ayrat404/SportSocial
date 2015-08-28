(function(){
var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit($scope, journalSubmitService) {
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
      return journalSubmitService.submit($scope.j).then(function(res) {
        return $scope.closeForm();
      }, function(res) {});
    };
  }

  return JournalSubmit;

})();

angular.module('socialApp.controllers').controller('journalSubmitController', ['$scope', 'journalSubmitService', JournalSubmit]);

})();
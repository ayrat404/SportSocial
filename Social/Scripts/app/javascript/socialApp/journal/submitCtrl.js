(function(){
var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit($scope, journalSubmitService) {
    journalSubmitService.test();
    console.log('journal submit ctrl');
    $scope.j = {
      tags: []
    };
    $scope.getThemes = function(search) {
      return null;
    };
    $scope.format = function($item, $model, $label) {
      debugger;
    };
  }

  return JournalSubmit;

})();

angular.module('socialApp.controllers').controller('journalSubmitController', ['$scope', 'journalSubmitService', JournalSubmit]);

})();
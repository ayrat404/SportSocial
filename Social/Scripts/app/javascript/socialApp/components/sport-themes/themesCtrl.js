(function(){
var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit($scope, journalSubmitService) {
    $scope.getThemes = function(search) {
      return journalSubmitService.get(search).then(function(res) {
        if (res.length) {
          return res;
        } else {
          return search;
        }
      });
    };
    $scope.format = function($item, $model, $label) {
      debugger;
    };
  }

  return JournalSubmit;

})();

angular.module('socialApp.controllers').controller('journalSubmitController', ['$scope', 'journalSubmitService', JournalSubmit]);

})();
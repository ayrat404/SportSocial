(function(){
var journalSubmit;

journalSubmit = (function() {
  function journalSubmit() {
    return {
      restrict: 'AE',
      replace: true,
      templateUrl: '/Scripts/templates/journal/submit.html',
      controller: 'journalSubmitController',
      link: function(scope, element, JournalCtrl) {
        return console.log('journal submit directive');
      }
    };
  }

  return journalSubmit;

})();

angular.module('socialApp.directives').directive('journalSubmit', [journalSubmit]);

})();
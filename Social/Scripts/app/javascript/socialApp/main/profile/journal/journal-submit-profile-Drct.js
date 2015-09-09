var journalProfileSubmit;

journalProfileSubmit = (function() {
  function journalProfileSubmit() {
    return {
      restrict: 'AE',
      replace: true,
      templateUrl: '/template/journal/submit',
      scope: {
        success: '&'
      },
      controller: 'journalProfileSubmitController',
      link: function(scope, element, JournalCtrl) {
        return console.log('journal submit directive');
      }
    };
  }

  return journalProfileSubmit;

})();

angular.module('socialApp.directives').directive('journalProfileSubmit', [journalProfileSubmit]);

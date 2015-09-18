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
      controller: 'journalProfileSubmitController'
    };
  }

  return journalProfileSubmit;

})();

angular.module('socialApp.directives').directive('journalProfileSubmit', [journalProfileSubmit]);

(function(){
var Registration;

Registration = (function() {
  function Registration($scope, mixpanel, registrationService) {
    $scope.$root.title = 'Fortress | Регистрация';
    $scope.loading = false;
    $scope.$on('$viewContentLoaded', function() {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }

  return Registration;

})();

angular.module('socialApp.controllers').controller('registrationController', ['$scope', 'mixpanel', 'registrationService', Registration]);

})();
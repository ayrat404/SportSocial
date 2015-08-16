(function(){
var Registration;
Registration = function () {
  function Registration($scope, mixpanel, registrationService) {
    $scope.$root.title = 'Fortress | \u0420\u0435\u0433\u0438\u0441\u0442\u0440\u0430\u0446\u0438\u044f';
    $scope.loading = false;
    $scope.$on('$viewContentLoaded', function () {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }
  return Registration;
}();
angular.module('socialApp.controllers').controller('registrationController', [
  '$scope',
  'mixpanel',
  'registrationService',
  Registration
]);
}).call(this);
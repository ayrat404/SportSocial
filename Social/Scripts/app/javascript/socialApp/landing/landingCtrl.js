(function(){
var Landing;
Landing = function () {
  function Landing($scope, mixpanel, registrationService) {
    $scope.$root.title = 'Fortress | \u0414\u043e\u0431\u0440\u043e \u043f\u043e\u0436\u0430\u043b\u043e\u0432\u0430\u0442\u044c';
    $scope.loading = false;
    $scope.$on('$viewContentLoaded', function () {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }
  return Landing;
}();
angular.module('socialApp.controllers').controller('landingController', [
  '$scope',
  'mixpanel',
  'registrationService',
  Landing
]);
}).call(this);
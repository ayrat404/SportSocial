(function(){
var Landing;

Landing = (function() {
  function Landing($scope, mixpanel) {
    $scope.$root.title = 'Fortress | Добро пожаловать';
    $scope.loading = false;
    $scope.$on('$viewContentLoaded', function() {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }

  return Landing;

})();

angular.module('socialApp.controllers').controller('landingController', ['$scope', 'mixpanel', Landing]);

})();
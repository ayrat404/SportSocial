var Landing;

Landing = (function() {
  function Landing($scope, $state, $rootScope, mixpanel) {
    $scope.$root.title = 'Fortress | Добро пожаловать';
    $scope.loading = false;
    if ($rootScope.user.id) {
      $state.go('main.profile', {
        userId: $rootScope.user.id
      });
    }
    $scope.$on('$viewContentLoaded', function() {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }

  return Landing;

})();

angular.module('socialApp.controllers').controller('landingController', ['$scope', '$state', '$rootScope', 'mixpanel', Landing]);

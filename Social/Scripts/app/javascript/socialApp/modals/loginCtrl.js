(function(){
var LoginSubmitModal;

LoginSubmitModal = (function() {
  function LoginSubmitModal($scope, $state, $modalInstance, loginService) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return loginService.logIn($scope.login).then(function(res) {
        return $modalInstance.dismiss();
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
    $scope.toRegistration = function() {
      $modalInstance.close();
      return $state.go('registration');
    };
  }

  return LoginSubmitModal;

})();

angular.module('socialApp.controllers').controller('loginSubmitModalController', ['$scope', '$state', '$modalInstance', 'loginService', LoginSubmitModal]);

})();
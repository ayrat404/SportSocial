var LoginSubmitModal;

LoginSubmitModal = (function() {
  function LoginSubmitModal($scope, $state, $modalInstance, loginService, modalData) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return loginService.logIn($scope.login).then(function(res) {
        $modalInstance.dismiss();
        if (typeof modalData.success === 'function') {
          return modalData.success(res);
        }
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
    $scope.toRegistration = function() {
      $modalInstance.close();
      return $state.go('registration');
    };
    $modalInstance.result["catch"](function() {
      if (typeof modalData.cancel === 'function') {
        return modalData.cancel();
      }
    });
  }

  return LoginSubmitModal;

})();

angular.module('socialApp.controllers').controller('loginSubmitModalController', ['$scope', '$state', '$modalInstance', 'loginService', 'modalData', LoginSubmitModal]);

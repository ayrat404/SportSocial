(function(){
var LoginSubmitModal;

LoginSubmitModal = (function() {
  function LoginSubmitModal($scope, $state, $modalInstance, loginService, modalData) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return loginService.logIn($scope.login).then(function(res) {
        $modalInstance.close();
        if (modalData && typeof modalData.success === 'function') {
          return modalData.success(res);
        } else {
          return window.location.reload();
        }
      }, function(res) {
        return $scope.serverValidation = res.data.errors;
      });
    };
    $scope.toRegistration = function() {
      $modalInstance.close();
      return $state.go('registration');
    };
    $modalInstance.result["catch"](function() {
      return modalData != null ? typeof modalData.cancel === "function" ? modalData.cancel() : void 0 : void 0;
    });
  }

  return LoginSubmitModal;

})();

angular.module('socialApp.controllers').controller('loginSubmitModalController', ['$scope', '$state', '$modalInstance', 'loginService', 'modalData', LoginSubmitModal]);

})();
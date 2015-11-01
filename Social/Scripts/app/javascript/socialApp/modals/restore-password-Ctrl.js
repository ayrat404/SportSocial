(function(){
var RestorePasswordSubmitNewModal, RestorePasswordSubmitPhoneModal;

RestorePasswordSubmitPhoneModal = (function() {
  function RestorePasswordSubmitPhoneModal($scope, $modalInstance, restorePasswordService, modalService) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return restorePasswordService.sendPhone({
        phone: $scope.phone
      }).then(function(res) {
        $modalInstance.close();
        return modalService.show({
          name: 'restorePasswordSubmitNewData',
          data: {
            phone: $scope.phone
          }
        });
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
  }

  return RestorePasswordSubmitPhoneModal;

})();

RestorePasswordSubmitNewModal = (function() {
  function RestorePasswordSubmitNewModal($scope, $modalInstance, restorePasswordService, loginService, modalService, modalData) {
    $scope.serverValidation = {};
    $scope.restore = {
      phone: modalData.phone
    };
    $scope.submit = function() {
      return restorePasswordService.sendNewPassword($scope.restore).then(function(res) {
        $modalInstance.close();
        return modalService.show({
          name: 'loginSubmit'
        });
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
  }

  return RestorePasswordSubmitNewModal;

})();

angular.module('socialApp.controllers').controller('restorePasswordSubmitPhoneModalController', ['$scope', '$modalInstance', 'restorePasswordService', 'modalService', RestorePasswordSubmitPhoneModal]).controller('restorePasswordSubmitNewModalController', ['$scope', '$modalInstance', 'restorePasswordService', 'loginService', 'modalService', 'modalData', RestorePasswordSubmitNewModal]);

})();
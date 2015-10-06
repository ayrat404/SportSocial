(function(){
var ChangePhoneGetCodeModal, ChangePhoneSubmitCodeModal;

ChangePhoneGetCodeModal = (function() {
  function ChangePhoneGetCodeModal($scope, $modalInstance, settingsService, modalService) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return settingsService.sendPhoneForCode({
        phone: $scope.phone
      }).then(function(res) {
        return modalService.show({
          name: 'changePhoneSubmitCode',
          data: {
            phone: $scope.phone
          }
        });
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
  }

  return ChangePhoneGetCodeModal;

})();

ChangePhoneSubmitCodeModal = (function() {
  function ChangePhoneSubmitCodeModal($scope, $modalInstance, $modalStack, $state, settingsService, modalService, modalData) {
    $scope.serverValidation = {};
    $scope.model = {
      phone: modalData.phone
    };
    $scope.submit = function() {
      return settingsService.sendPhoneWithCode($scope.model).then(function(res) {
        $modalStack.dismissAll();
        return $state.reload();
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
  }

  return ChangePhoneSubmitCodeModal;

})();

angular.module('socialApp.controllers').controller('changePhoneGetCodeModalController', ['$scope', '$modalInstance', 'settingsService', 'modalService', ChangePhoneGetCodeModal]).controller('changePhoneSubmitCodeModalController', ['$scope', '$modalInstance', '$modalStack', '$state', 'settingsService', 'modalService', 'modalData', ChangePhoneSubmitCodeModal]);

})();
var SupportSubmitModal, SupportSuccessModal;

SupportSubmitModal = (function() {
  function SupportSubmitModal($scope, $modalInstance, supportService, modalService) {
    $scope.serverValidation = {};
    $scope.submit = function() {
      return supportService.send($scope.support).then(function(res) {
        $modalInstance.dismiss();
        return modalService.show({
          name: 'supportSuccess'
        });
      }, function(res) {
        return $scope.serverValidation = res.errors;
      });
    };
  }

  return SupportSubmitModal;

})();

SupportSuccessModal = (function() {
  function SupportSuccessModal($scope, $modalInstance) {
    $scope.close = function() {
      return $modalInstance.dismiss();
    };
  }

  return SupportSuccessModal;

})();

angular.module('socialApp.controllers').controller('supportSubmitModalController', ['$scope', '$modalInstance', 'supportService', 'modalService', SupportSubmitModal]).controller('supportSuccessModalController', ['$scope', '$modalInstance', SupportSuccessModal]);

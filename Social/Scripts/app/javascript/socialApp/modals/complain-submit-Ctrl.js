var ComplainSubmitModal;

ComplainSubmitModal = (function() {
  function ComplainSubmitModal($scope, $modalInstance, complainService, modalData) {
    $scope.o = modalData;
    $scope.submit = function() {
      $scope.o.text = $scope.text;
      return complainService.submit(modalData).then(function(res) {
        $modalInstance.close();
        if (typeof modalData.success === 'function') {
          return modalData.success(res);
        }
      });
    };
    $scope.close = function() {
      return $modalInstance.close();
    };
  }

  return ComplainSubmitModal;

})();

angular.module('socialApp.controllers').controller('complainSubmitModalController', ['$scope', '$modalInstance', 'complainService', 'modalData', ComplainSubmitModal]);

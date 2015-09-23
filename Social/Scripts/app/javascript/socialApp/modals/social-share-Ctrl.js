(function(){
var socialShareModal;

socialShareModal = (function() {
  function socialShareModal($scope, $modalInstance, modalData) {
    $scope.o = modalData;
  }

  return socialShareModal;

})();

angular.module('socialApp.controllers').controller('socialShareModalController', ['$scope', '$modalInstance', 'modalData', socialShareModal]);

})();
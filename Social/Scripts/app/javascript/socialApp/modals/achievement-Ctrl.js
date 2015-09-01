var aboutAchievementModal;

aboutAchievementModal = (function() {
  function aboutAchievementModal($state, $scope, $modalInstance) {
    $scope.goAddAchievement = function() {
      return $modalInstance.close();
    };
  }

  return aboutAchievementModal;

})();

angular.module('socialApp.controllers').controller('aboutAchievementModalController', ['$state', '$scope', '$modalInstance', aboutAchievementModal]);

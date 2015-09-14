var AchievementSubmit;

AchievementSubmit = (function() {
  function AchievementSubmit($scope, $stateParams, $rootScope) {
    var _this, prop;
    $scope.$root.title = 'Fortress | Заявка на награду';
    prop = {
      stepsLength: 3
    };
    _this = this;
    _this.steps = {
      current: 0
    };
    _this.prevStep = function() {
      return _this.steps.current--;
    };
    _this.nextStep = function() {
      return _this.steps.current++;
    };
  }

  return AchievementSubmit;

})();

angular.module('socialApp.controllers').controller('achievementSubmitController', ['$scope', '$stateParams', '$rootScope', AchievementSubmit]);

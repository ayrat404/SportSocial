var AchievementView;

AchievementView = (function() {
  function AchievementView($scope, $stateParams, $rootScope, $window, achievementService) {
    var _this;
    $scope.$root.title = 'Fortress | Заявка на награду';
    _this = this;
    _this.loader = false;
    achievementService.getById(+$stateParams.id).then(function(res) {
      _this.it = res.data;
      _this.it.comments.form = {};
      if ($rootScope.user.id === _this.it.author.id) {
        return _this.it.isOwner = true;
      } else {
        return _this.it.isOwner = false;
      }
    });
    _this.remove = function() {
      return modalService.show({
        name: 'achievementRemove',
        data: {
          id: _this.it.id,
          success: function(res) {
            return $state.go('main.profile', {
              userId: $rootScope.user.id
            });
          }
        }
      });
    };
    _this.share = function() {
      return modalService.show({
        name: 'socialShare',
        data: {
          text: _this.it.text,
          media: _this.it.media
        }
      });
    };
  }

  return AchievementView;

})();

angular.module('socialApp.controllers').controller('achievementViewController', ['$scope', '$stateParams', '$rootScope', '$window', 'achievementService', AchievementView]);

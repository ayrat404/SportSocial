(function(){
var AchievementView;

AchievementView = (function() {
  function AchievementView($state, $scope, $stateParams, $rootScope, $window, modalService, achievementService) {
    var _this, calcBars, k;
    $scope.$root.title = 'Fortress | Просмотр заявки на награду';
    _this = this;
    _this.loader = true;
    _this.pageError = false;
    achievementService.getById({
      id: +$stateParams.id
    }).then(function(res) {
      _this.it = res.data;
      _this.it.comments.form = {};
      if ($rootScope.user.id === _this.it.author.id) {
        _this.it.isOwner = true;
      } else {
        _this.it.isOwner = false;
      }
      return calcBars();
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function(res) {
      return _this.loader = false;
    });
    _this.remove = function() {
      return modalService.show({
        name: 'achievementRemove',
        data: {
          id: _this.it.id,
          success: function() {
            return $state.go('main.achievementList');
          }
        }
      });
    };
    _this.share = function() {
      return modalService.show({
        name: 'socialShare',
        data: {
          url: $state.href('main.achievementView', {
            id: _this.it.id
          }, {
            absolute: true
          }),
          text: _this.it.title,
          media: _this.it.cupImage
        }
      });
    };
    k = 0;
    calcBars = function() {
      k = 100 / (_this.it.voice["for"] + _this.it.voice.against);
      _this.forBarWidth = Math.round(_this.it.voice["for"] * k) + '%';
      return _this.againstBarWidth = Math.round(_this.it.voice.against * k) + '%';
    };
    _this.voice = function(action) {
      if (!_this.it.voice.isVoited && _this.it.author.id !== $rootScope.user.id) {
        return achievementService.voice({
          id: _this.it.id,
          action: action
        }).then(function(res) {
          debugger;
          _this.it.voice["for"] = res.data["for"];
          _this.it.voice.against = res.data.against;
          _this.it.voice.isVoited = true;
          return calcBars();
        });
      }
    };
  }

  return AchievementView;

})();

angular.module('socialApp.controllers').controller('achievementViewController', ['$state', '$scope', '$stateParams', '$rootScope', '$window', 'modalService', 'achievementService', AchievementView]);

})();
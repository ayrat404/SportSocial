(function(){
var AchievementSubmit;

AchievementSubmit = (function() {
  function AchievementSubmit($scope, $state, $stateParams, $rootScope, $window, achievementService, youtubeVideoService) {
    var _this, defaultModel, prop;
    $scope.$root.title = 'Fortress | Заявка на награду';
    prop = {
      stepsLength: 3
    };
    defaultModel = {
      id: -1,
      step: 0,
      video: {}
    };
    _this = this;
    this.prevStep = function() {
      return _this.currentStep--;
    };
    this.nextStep = function() {
      _this.loader = true;
      return achievementService.saveTemp(_this.model).then(function(res) {
        if (res.data && res.data.isPublished) {
          return $state.go('main.achievementView', {
            id: res.data.id
          });
        } else {
          _this.model.id = 1;
          return _this.currentStep += 1;
        }
      })["finally"](function(res) {
        return _this.loader = false;
      });
    };
    this.cancel = function() {
      if (this.model.id !== -1) {
        achievementService.cancelTemp();
      }
      return $state.go('main.achievementList');
    };
    this.checkFirstStep = function() {
      var i, j, ref, result;
      result = false;
      for (i = j = 0, ref = _this.cards.length; 0 <= ref ? j < ref : j > ref; i = 0 <= ref ? ++j : --j) {
        if (_this.cards[i].focus && _this.cards[i].selected) {
          result = true;
          _this.model.type = {
            id: _this.cards[i].id,
            value: _this.cards[i].selected
          };
          _this.second.exampleLink = _this.cards[i].videoUrl;
          break;
        }
      }
      return _this.firstValid = result;
    };
    this.cardFocus = function(card) {
      var i, j, ref;
      for (i = j = 0, ref = _this.cards.length; 0 <= ref ? j < ref : j > ref; i = 0 <= ref ? ++j : --j) {
        if (_this.cards[i].id === card.id) {
          _this.cards[i].focus = true;
        } else {
          _this.cards[i].focus = false;
        }
      }
      return _this.checkFirstStep();
    };
    this.second = {
      isExampleShow: true,
      customLink: '',
      hideExample: function() {
        if (_this.second.isExampleShow) {
          _this.second.ePlayer.pauseVideo();
        }
        return _this.second.isExampleShow = !_this.second.isExampleShow;
      },
      getVideoInfo: function() {
        return youtubeVideoService.getVideoInfo({
          link: _this.model.video.remoteUrl,
          type: 'achievement'
        }).then(function(res) {
          _this.model.video.id = res.data.id;
          return _this.second.isExampleShow = false;
        }, function(res) {
          return _this.model.video.id = null;
        });
      },
      removeVideo: function() {
        _this.second.customLink = '';
        return _this.model.videoId = '';
      }
    };
    _this.loader = true;
    achievementService.getTemp().then(function(res) {
      var i, j, ref;
      _this.cards = res.data.cards;
      _this.marks = res.data.marks;
      if (res.data.model !== null) {
        _this.model = res.data.model;
        for (i = j = 0, ref = _this.cards.length; 0 <= ref ? j < ref : j > ref; i = 0 <= ref ? ++j : --j) {
          if (_this.model.type.id === _this.cards[i].id) {
            _this.cards[i].focus = true;
            _this.cards[i].selected = _this.model.type.value;
            _this.second.exampleLink = _this.cards[i].videoUrl;
            break;
          }
        }
      } else {
        _this.model = defaultModel;
      }
      _this.currentStep = _this.model.step;
      if (_this.currentStep >= 1) {
        _this.second.isExampleShow = false;
      }
      return _this.checkFirstStep();
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function() {
      return _this.loader = false;
    });
  }

  return AchievementSubmit;

})();

angular.module('socialApp.controllers').controller('achievementSubmitController', ['$scope', '$state', '$stateParams', '$rootScope', '$window', 'achievementService', 'youtubeVideoService', AchievementSubmit]);

})();
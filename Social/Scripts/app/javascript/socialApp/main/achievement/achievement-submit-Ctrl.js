var AchievementSubmit;

AchievementSubmit = (function() {
  function AchievementSubmit($scope, $stateParams, $rootScope, $window, achievementService, youtubeVideoService) {
    var _this, defaultModel, prop;
    $scope.$root.title = 'Fortress | Заявка на награду';
    prop = {
      stepsLength: 3
    };
    defaultModel = {
      id: -1,
      step: 0
    };
    _this = this;
    this.prevStep = function() {
      return _this.currentStep--;
    };
    this.nextStep = function() {
      _this.loader = true;
      return achievementService.saveTemp(_this.model).then(function(res) {
        _this.model.id = res.data.id;
        return _this.currentStep++;
      })["finally"](function(res) {
        return _this.loader = false;
      });
    };
    this.cancel = function() {
      if (this.model.id !== -1) {
        achievementService.cancelTemp();
      }
      return $window.history.back();
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
      exampleLink: 'http://www.youtube.com/watch?v=zWc41BbjlZ4',
      isExampleShow: true,
      customLink: '',
      isCustomLoaded: false,
      hideExample: function() {
        if (_this.second.isExampleShow) {
          _this.second.ePlayer.pauseVideo();
        }
        return _this.second.isExampleShow = !_this.second.isExampleShow;
      },
      getVideoInfo: function(link) {
        return youtubeVideoService.getVideoInfo(_this.second.customLink).then(function(res) {
          _this.second.isCustomLoaded = true;
          _this.model.videoId = res.data.id;
          return _this.second.isExampleShow = false;
        });
      },
      removeVideo: function() {
        _this.second.customLink = '';
        _this.model.videoId = '';
        return _this.second.isCustomLoaded = false;
      }
    };
    _this.cards = [
      {
        id: 1,
        img: 'imageUrl1',
        title: 'Подтягивания',
        values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100]
      }, {
        id: 2,
        img: 'imageUrl2',
        title: 'Отжимания',
        values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400]
      }
    ];
    _this.model = defaultModel;
    _this.currentStep = _this.model.step;
  }

  return AchievementSubmit;

})();

angular.module('socialApp.controllers').controller('achievementSubmitController', ['$scope', '$stateParams', '$rootScope', '$window', 'achievementService', 'youtubeVideoService', AchievementSubmit]);

var AchievementList;

AchievementList = (function() {
  function AchievementList($scope, achievementService) {
    var _this;
    $scope.$root.title = 'Fortress | Список заявок';
    _this = this;
    _this.loader = true;
    _this.pageError = false;
    achievementService.getList().then(function(res) {
      return _this.it = res.data;
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function(res) {
      return _this.loader = false;
    });
    _this.list = [
      {
        id: 1,
        iconUrl: 'iconUrl1',
        title: 'Подтягивания. 35 Повторений',
        created: '06 сентября 2015',
        timeSpent: '6',
        voice: {
          "for": 142,
          against: 10
        },
        user: {
          id: 1,
          avatar: 'avatarUrl',
          fullName: 'Mikki Mouse'
        }
      }, {
        id: 2,
        iconUrl: 'iconUrl1',
        title: 'Подтягивания. 35 Повторений',
        created: '06 сентября 2015',
        timeSpent: '6',
        voice: {
          "for": 142,
          against: 10
        },
        user: {
          id: 1,
          avatar: 'avatarUrl',
          fullName: 'Mikki Mouse'
        }
      }, {
        id: 3,
        iconUrl: 'iconUrl1',
        title: 'Подтягивания. 35 Повторений',
        created: '06 сентября 2015',
        timeSpent: '6',
        voice: {
          "for": 142,
          against: 10
        },
        user: {
          id: 1,
          avatar: 'avatarUrl',
          fullName: 'Mikki Mouse'
        }
      }
    ];
  }

  return AchievementList;

})();

angular.module('socialApp.controllers').controller('achievementListController', ['$scope', 'achievementService', AchievementList]);

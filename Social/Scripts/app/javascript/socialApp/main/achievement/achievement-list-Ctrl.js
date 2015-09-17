<<<<<<< HEAD
var AchievementList;

AchievementList = (function() {
  function AchievementList($q, $scope, $state, achievementService) {
    var _this, getList, setUrl;
    $scope.$root.title = 'Fortress | Список заявок';
    _this = this;
    _this.loader = false;
    _this.pageError = false;
    _this.showMoreLoading = false;
    _this.filter = {
      status: 'all',
      actual: 'opened',
      count: 20,
      page: 3
    };
    _this.prop = {};
    angular.extend(_this.filter, $state.params);
    setUrl = function() {
      $state.params = _this.filter;
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    };
    getList = function(filter) {
      setUrl();
      return $q(function(resolve, reject) {
        return achievementService.getList(filter).then(function(res) {
          _this.showMore = res.data.isMore;
          return resolve(res.data.list);
        }, function(res) {
          _this.list = [];
          _this.showMore = false;
          return reject(res);
        });
      });
    };
    _this.updateList = function(isFirstLoad) {
      var filter;
      _this.loader = true;
      filter = angular.extend({}, _this.filter);
      if (isFirstLoad && filter.page > 1) {
        filter.page = 1;
        filter.count = _this.filter.page * _this.filter.count;
      }
      return getList(filter).then(function(list) {
        return _this.list = list;
      })["finally"](function() {
        return _this.loader = false;
      });
    };
    _this.loadMore = function() {
      var filter;
      if (!_this.showMoreLoading) {
        _this.showMoreLoading = true;
        filter = angular.extend({}, _this.filter);
        filter.page = +filter.page + 1;
        return getList(filter).then(function(list) {
          _this.list.push(list);
          return _this.filter.page = filter.page;
        })["finally"](function() {
          return _this.showMoreLoading = false;
        });
      }
    };
    achievementService.getFilterProp().then(function(res) {
      _this.prop = res.data;
      return _this.updateList(true);
    }, function() {
      return _this.pageError = true;
    });
    $scope.$watch(function() {
      return _this.filter.actual;
    }, function(newVal, oldVal) {
      if (newVal !== oldVal) {
        return _this.updateList();
      }
    });
  }

  return AchievementList;

})();

angular.module('socialApp.controllers').controller('achievementListController', ['$q', '$scope', '$state', 'achievementService', AchievementList]);
=======
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
>>>>>>> 7571a00bb8d0183181ce35ab0608d63e896e928a

var AchievementList;

AchievementList = (function() {
  function AchievementList($q, $scope, $state, achievementService) {
    var _this, getList, k, ref, setUrl, v;
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
    ref = $state.params;
    for (k in ref) {
      v = ref[k];
      if (v !== void 0) {
        _this.filter[k] = v;
      }
    }
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

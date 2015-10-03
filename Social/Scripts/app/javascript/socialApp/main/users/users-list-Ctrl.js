(function(){
var UsersList;

UsersList = (function() {
  function UsersList($q, $scope, $state, userService) {
    var _this, getList, k, ref, setUrl, v;
    $scope.$root.title = 'Fortress | Список атлетов';
    _this = this;
    _this.loader = true;
    _this.pageError = false;
    _this.showMoreLoading = false;
    _this.filter = {
      page: 1,
      count: 20
    };
    ref = $state.params;
    for (k in ref) {
      v = ref[k];
      if (v !== void 0) {
        _this.filter[k] = v;
      }
    }
    setUrl = function(params) {
      $state.params = params;
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    };
    getList = function(filter) {
      setUrl(filter);
      return $q(function(resolve, reject) {
        return userService.getList(filter).then(function(res) {
          _this.showMore = res.data.isMore;
          return resolve(res.data.list);
        }, function(res) {
          _this.list = [];
          _this.showMore = false;
          return reject(res);
        });
      });
    };
    _this.updateList = function() {
      var filter;
      _this.loader = true;
      filter = angular.extend({}, _this.filter);
      filter.count = filter.page * filter.count;
      filter.page = 1;
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
    _this.updateList();
  }

  return UsersList;

})();

angular.module('socialApp.controllers').controller('usersListController', ['$q', '$scope', '$state', 'userService', UsersList]);

})();
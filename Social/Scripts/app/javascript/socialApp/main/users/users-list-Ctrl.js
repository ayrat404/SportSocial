(function(){
var UsersList;

UsersList = (function() {
  function UsersList($q, $scope, $state, userService) {
    var _this, getList, loadProp, setUrl;
    $scope.$root.title = 'Fortress | Список атлетов';
    _this = this;
    _this.loader = true;
    _this.pageError = false;
    _this.showMoreLoading = false;
    loadProp = {
      count: 20,
      page: 3
    };
    _this.filter = {
      test: 'test'
    };
    setUrl = function() {
      $state.params = angular.extend({}, _this.filter, loadProp);
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    };
    getList = function(filter) {
      setUrl();
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
      filter = angular.extend({}, _this.filter, loadProp);
      filter.page = 1;
      filter.count = loadProp.page * loadProp.count;
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
        filter = angular.extend({}, _this.filter, loadProp);
        filter.page = +filter.page + 1;
        return getList(filter).then(function(list) {
          _this.list.push(list);
          return loadProp.page = filter.page;
        })["finally"](function() {
          return _this.showMoreLoading = false;
        });
      }
    };
    userService.getFilterProp().then(function(res) {
      _this.prop = res.data;
      return _this.updateList();
    }, function() {
      return _this.pageError = true;
    });
  }

  return UsersList;

})();

angular.module('socialApp.controllers').controller('usersListController', ['$q', '$scope', '$state', 'userService', UsersList]);

})();
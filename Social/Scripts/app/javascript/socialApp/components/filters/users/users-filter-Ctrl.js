(function(){
var UsersFilter;

UsersFilter = (function() {
  function UsersFilter($scope, userService, geoService, base) {
    var _this, getGeo;
    _this = this;
    _this.loading = true;
    _this.error = false;
    _this.filter = $scope.filter;
    _this.update = function() {
      return $scope.callback();
    };
    getGeo = function(entity, query) {
      return geoService[entity]({
        query: query,
        count: $scope.queryListLimit
      }).then(function(res) {
        if (res.data.length) {
          return res.data;
        } else {
          base.notice.show({
            text: "Location " + query + " is not found",
            type: 'info'
          });
          return [];
        }
      });
    };
    _this.getCountry = function(query) {
      return getGeo('getCountry', query);
    };
    _this.getCity = function(query) {
      return getGeo('getCity', query);
    };
    userService.getFilterProp().then(function(res) {
      return _this.prop = res.data;
    }, function() {
      return _this.error = true;
    })["finally"](function() {
      return _this.loading = false;
    });
  }

  return UsersFilter;

})();

angular.module('socialApp.controllers').controller('usersFilterController', ['$scope', 'userService', 'geoService', 'base', UsersFilter]);

})();
(function(){
var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $state, $stateParams, $rootScope, mixpanel, profileService, modalService, defaultAvatarUrl) {
    var _this, loadProp;
    $scope.$root.title = "Fortress | " + $rootScope.user.fullName;
    $scope.$on('$viewContentLoaded', function() {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
    _this = this;
    _this.loading = true;
    _this.unknown = false;
    _this.user = {
      loaded: false
    };
    loadProp = {
      count: 20,
      page: 3
    };
    _this.complain = function(title) {
      return modalService.show({
        name: 'complainSubmit',
        data: {
          userId: $rootScope.user.id,
          entityId: _this.user.id,
          type: 'profile',
          title: title
        }
      });
    };
    _this.newRecord = function(res) {
      return _this.user.journal.list.unshift(res.data);
    };
    _this.shareRecord = function(obj) {
      return modalService.show({
        name: 'socialShare',
        data: {
          url: $state.href('main.journalIt', {
            id: obj.id
          }, {
            absolute: true
          }),
          text: obj.text,
          media: obj.media,
          hashtags: obj.tags
        }
      });
    };
    _this.remove = function(id) {
      return modalService.show({
        name: 'journalRemove',
        data: {
          id: id,
          success: function(res) {
            var i, j, k, len, ref, results;
            ref = _this.user.journal.list;
            results = [];
            for (i = k = 0, len = ref.length; k < len; i = ++k) {
              j = ref[i];
              if (j.id === id) {
                _this.user.journal.list.splice(i, 1);
                break;
              } else {
                results.push(void 0);
              }
            }
            return results;
          }
        }
      });
    };
    _this.edit = function(model) {
      return modalService.show({
        name: 'journalSubmit',
        data: {
          model: model,
          success: function(record) {
            var i, j, k, len, ref, results;
            ref = _this.user.journal.list;
            results = [];
            for (i = k = 0, len = ref.length; k < len; i = ++k) {
              j = ref[i];
              if (j.id === id) {
                _this.user.journal.list[i] = record;
                break;
              } else {
                results.push(void 0);
              }
            }
            return results;
          }
        }
      });
    };
    _this.loadMoreRecords = function() {
      if (!_this.user.journal.loading) {
        _this.user.journal.loading = true;
        loadProp.page += 1;
        $state.params = loadProp;
        $state.transitionTo($state.current, $state.params, {
          notify: false
        });
        return getList(loadProp).then(function(list) {
          return _this.list.push(list);
        })["finally"](function() {
          return _this.user.journal.loading = false;
        });
      }
    };
    _this.avatarResponse = function(stringRes) {
      var objRes;
      objRes = angular.fromJson(stringRes);
      if (objRes.success) {
        $rootScope.$emit('changeAvatar', objRes.data.url);
        $rootScope.user.avatar = objRes.data.url;
        return _this.user.avatar = objRes.data.url;
      }
    };
    _this.removeAvatar = function($flow) {
      return profileService.removeAvatar().then(function(res) {
        $flow.cancel();
        $rootScope.$emit('changeAvatar', defaultAvatarUrl);
        $rootScope.user.avatar = defaultAvatarUrl;
        return _this.user.avatar = defaultAvatarUrl;
      });
    };
    profileService.getInfo({
      id: $stateParams.userId
    }).then(function(res) {
      _this.user = res.data;
      if ($rootScope.user.id === +$stateParams.userId) {
        _this.user.isOwner = true;
      } else {
        _this.user.isOwner = false;
      }
      _this.user.id = $stateParams.userId;
      _this.user.loaded = true;
      return loadProp.authorId = _this.user.id;
    }, function(res) {
      return _this.unknown = true;
    })["finally"](function(res) {
      return _this.loading = false;
    });
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$state', '$stateParams', '$rootScope', 'mixpanel', 'profileService', 'modalService', 'defaultAvatarUrl', ProfileView]);

})();
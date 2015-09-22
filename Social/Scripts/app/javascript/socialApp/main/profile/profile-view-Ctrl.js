var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $state, $stateParams, $rootScope, mixpanel, profileService, modalService) {
    var _this;
    $scope.$root.title = ['Fortress | ', $rootScope.user.fullName].join('');
    _this = this;
    _this.unknown = false;
    _this.user = {
      loaded: false
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
      return _this.user.journals.unshift(res.data);
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
            ref = _this.user.journals;
            results = [];
            for (i = k = 0, len = ref.length; k < len; i = ++k) {
              j = ref[i];
              if (j.id === id) {
                _this.user.journals.splice(i, 1);
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
            ref = _this.user.journals;
            results = [];
            for (i = k = 0, len = ref.length; k < len; i = ++k) {
              j = ref[i];
              if (j.id === id) {
                _this.user.journals[i] = record;
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
    _this.avatarResponse = function(stringRes) {
      var objRes;
      objRes = angular.fromJson(stringRes);
      if (objRes.success) {
        return _this.user.avatar = objRes.data.url;
      }
    };
    _this.removeAvatar = function() {
      return profileService.removeAvatar().then(function(res) {
        return _this.user.avatar = null;
      });
    };
    profileService.getInfo($stateParams.userId).then(function(res) {
      _this.user = res.data;
      if ($rootScope.user.id === +$stateParams.userId) {
        _this.user.isOwner = true;
      } else {
        _this.user.isOwner = false;
      }
      _this.user.id = $stateParams.userId;
      return _this.user.loaded = true;
    }, function(res) {
      return _this.unknown = true;
    });
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$state', '$stateParams', '$rootScope', 'mixpanel', 'profileService', 'modalService', ProfileView]);

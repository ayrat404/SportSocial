var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $state, $stateParams, $rootScope, mixpanel, profileService, modalService) {
    var _this;
    $scope.$root.title = ['Fortress | ', $rootScope.user.fullName].join('');
    _this = this;
    _this.user = {
      loaded: false
    };
    _this.newRecord = function(res) {
      return _this.user.journals.unshift(res.data);
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
    profileService.getInfo($stateParams.userId).then(function(res) {
      _this.user = res.data;
      if ($rootScope.user.id === +$stateParams.userId) {
        _this.user.isOwner = true;
      } else {
        _this.user.isOwner = false;
      }
      return _this.user.loaded = true;
    });
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$state', '$stateParams', '$rootScope', 'mixpanel', 'profileService', 'modalService', ProfileView]);

var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $stateParams, mixpanel, profileService) {
    var _this;
    _this = this;
    _this.user = {
      loaded: false
    };
    profileService.getInfo($stateParams.userId).then(function(res) {
      _this.user = res.data;
      return _this.user.loaded = true;
    });
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$stateParams', 'mixpanel', 'profileService', ProfileView]);

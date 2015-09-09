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

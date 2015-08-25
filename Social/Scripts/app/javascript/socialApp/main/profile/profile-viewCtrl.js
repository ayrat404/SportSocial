(function(){
var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, mixpanel) {
    console.log('profile view controller start');
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', 'mixpanel', ProfileView]);

})();
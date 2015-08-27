(function(){
var MainSocial;

MainSocial = (function() {
  function MainSocial($state) {
    $state.go('main.profile', {
      userId: 666
    });
  }

  return MainSocial;

})();

angular.module('socialApp.controllers').controller('mainSocialController', ['$state', MainSocial]);

})();
(function(){
var MainSocial;

MainSocial = (function() {
  function MainSocial($state) {
    $state.go('landing', {
      userId: 666
    });
  }

  return MainSocial;

})();

angular.module('socialApp.controllers').controller('mainSocialController', ['$state', MainSocial]);

})();
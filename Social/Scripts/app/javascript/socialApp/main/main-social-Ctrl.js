var MainSocial;

MainSocial = (function() {
  function MainSocial($state, $rootScope, userService) {}

  return MainSocial;

})();

angular.module('socialApp.controllers').controller('mainSocialController', ['$state', '$rootScope', 'userService', MainSocial]);

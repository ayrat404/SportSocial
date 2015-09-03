var MainSocial;

MainSocial = (function() {
  function MainSocial($rootScope, userService) {
    $rootScope.user = userService.get();
  }

  return MainSocial;

})();

angular.module('socialApp.controllers').controller('mainSocialController', ['$rootScope', 'userService', MainSocial]);

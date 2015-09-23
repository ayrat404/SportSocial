(function(){
var MainSocial;

MainSocial = (function() {
  function MainSocial($rootScope, userService) {}

  return MainSocial;

})();

angular.module('socialApp.controllers').controller('mainSocialController', ['$rootScope', 'userService', MainSocial]);

})();
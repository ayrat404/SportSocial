(function(){
var app;

app = angular.module('socialApp.controllers', []);

app = angular.module('socialApp.services', []);

app = angular.module('socialApp.directives', []);

app = angular.module('socialApp', ['socialApp.controllers', 'socialApp.services', 'socialApp.directives']).run(function() {
  return angular.element('body').css('opacity', 1);
});

})();
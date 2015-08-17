'use strict';

angular.module('socialApp.controllers', []);
angular.module('socialApp.services', []);
angular.module('socialApp.directives', []);

angular.module('socialApp', [
    'socialApp.controllers',
    'socialApp.services',
    'socialApp.directives'])
    .run(function() {
        angular.element('body').css('opacity', 1);
    });
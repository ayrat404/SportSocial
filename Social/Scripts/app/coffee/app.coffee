# CoffeeScript
angular.module 'app', [
    'ui.router',
     'ngCookies',
     'shared',
     'appSrvc',
     'socialApp'
] .config [
        '$stateProvider',
        '$locationProvider',
        '$httpProvider',
        ($stateProvider,
        $locationProvider,
        $httpProvider)->
        
        tmplUrl = '/Scripts/templates/'
        tmplView = (viewUrl)->
            tmplUrl + viewUrl + '.html'
            

]
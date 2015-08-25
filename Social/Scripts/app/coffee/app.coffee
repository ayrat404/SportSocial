# CoffeeScript
angular.module('app', [
    'ui.router',
    'ui.bootstrap',
    'ngCookies',
    'flow',
    'shared',
    'appSrvc',
    'socialApp'
]).config([
    '$stateProvider',
    '$locationProvider',
    '$httpProvider',
    ($stateProvider,
     $locationProvider,
     $httpProvider)->
        tmplUrl = '/Scripts/templates/'
        tmplView = (viewUrl) ->
            tmplUrl + viewUrl + '.html'

        # routes
        # ---------------
        $stateProvider
        .state('landing',
            url: '/'
            templateUrl: tmplView('landing/index')
            controller: 'landingController')
        .state('registration'
            url: '/registration'
            templateUrl: tmplView('registration/index')
            controller: 'registrationController'
            fullHeight: true)

        $locationProvider.html5Mode({
            enabled: true
            requireBase: false
        })

        return
]).run([
    '$templateCache',
    '$rootScope',
    '$state',
    '$stateParams',
    ($templateCache,
     $rootScope,
     $state,
     $stateParams)->

        view = angular.element '#ui-view'
        $templateCache.put(view.data('tmpl-url'), view.html())

        $rootScope.$state = $state
        $rootScope.$stateParams = $stateParams

        # global scripts init
        # ---------------
        NProgress.configure({ minimum: 0.3 })

        # ---------------
        $rootScope.$on '$stateChangeStart', (event, toState) ->
            $rootScope.loading = true
            NProgress.start()

        # ---------------
        $rootScope.$on '$stateChangeSuccess', (event, toState) ->
            $rootScope.loading = false
            $rootScope.fullHeight = toState.fullHeight
            NProgress.done()

        return
])
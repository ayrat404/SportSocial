# CoffeeScript
app = angular.module('app', [
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
    '$urlRouterProvider',
    'templateUrl',
    ($stateProvider,
     $locationProvider,
     $httpProvider,
     $urlRouterProvider
     templateUrl)->

        tmplView = (viewPath) ->
            templateUrl + '/' + viewPath

        # routes
        # ---------------
        $stateProvider
        .state 'main',
            abstract: true
            #url: '/'
            views:
                '@':
                    templateUrl: tmplView('main/_layout')
                    controller: 'mainSocialController as social'
        .state 'main.profile',
            url: '/id:userId'
            views:
                'socialContent@main':
                    templateUrl: tmplView('profile/index')
                    controller: 'profileViewController as profile'
        .state 'landing',
            url: '/'
            templateUrl: tmplView('landing/index')
            controller: 'landingController'
        .state '404',
            url: '/404'
            templateUrl: tmplView('errors/404')
        .state 'registration',
            url: '/registration'
            templateUrl: tmplView('registration/index')
            controller: 'registrationController'
            fullHeight: true

        $urlRouterProvider.otherwise '/404'

        $httpProvider.interceptors.push('apiInterseptorProvider')

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

app
.constant('templateUrl', '/template')
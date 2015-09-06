# CoffeeScript
app = angular.module('app', [
    'ui.router',
    'ui.bootstrap',
    'angular-storage'
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
            data:
                requireLogin: true
            views:
                '@':
                    templateUrl: tmplView('main/_layout')
                    controller: 'mainSocialController'
                    controllerAs: 'social'
        .state 'main.profile',
            url: '/id:userId'
            views:
                'socialContent@main':
                    templateUrl: tmplView('profile/index')
                    controller: 'profileViewController'
                    controllerAs: 'profile'
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

        $urlRouterProvider.otherwise '/404'

        $httpProvider.interceptors.push('apiInterseptorProvider')

        $locationProvider.html5Mode({
            enabled: true
            requireBase: false
        })

        return
]).run([
    '$templateCache'
    '$rootScope'
    '$state'
    '$stateParams'
    'modalService'
    'base'
    'userService'
    ($templateCache
     $rootScope
     $state
     $stateParams
     modalService
     base
     userService)->

        view = angular.element '#ui-view'
        $templateCache.put(view.data('tmpl-url'), view.html())

        $rootScope.$state = $state
        $rootScope.$stateParams = $stateParams

        # global scripts init
        # ---------------
        NProgress.configure({ minimum: 0.3 })

        # current user model
        $rootScope.user = {}

        # ---------------
        $rootScope.$on '$stateChangeStart', (event, toState, toParams) ->
            if toState.data != undefined
                requireLogin = toState.data.requireLogin
            if requireLogin && userService.get() == undefined
                event.preventDefault()
                base.notice.show(text: 'The page you requested is available only to registered users', type: 'warning')
                modalService.show(
                  name: 'loginSubmit'
                  data:
                      success: (res)->
                          $state.go(toState.name, toParams)
                      cancel: (res)->
                          $state.go 'registration'
                          base.notice.show(text: 'Please register if you do not have an Fortress account ', type: 'info')
                )
            else
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
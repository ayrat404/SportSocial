app = angular.module('app', [
    'ui.router'
    'ui.bootstrap'
    'angular-storage'
    'flow'
    '720kb.socialshare'
    'youtube-embed'
    'shared'
    'appSrvc'
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
                    templateUrl: tmplView 'main/_layout'
                    controller: 'mainSocialController'
                    controllerAs: 'social'
        .state 'main.profile',
            url: '/id:userId?media&index&entityType&count&page'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'profile/index'
                    controller: 'profileViewController'
                    controllerAs: 'profile'
        .state 'main.journalIt',
            url: '/record/:id?media&index&entityType'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'journal/view'
                    controller: 'recordViewController'
                    controllerAs: 'record'
        .state 'main.achievementAdd',
            url: '/achievement/add'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'achievement/achievement-submit'
                    controller: 'achievementSubmitController'
                    controllerAs: 'ach'
        .state 'main.achievementView',
            url: '/achievement/:id'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'achievement/achievement-view'
                    controller: 'achievementViewController'
                    controllerAs: 'ach'
        .state 'main.achievementList',
            url: '/achievements?actual&status&type&count&page'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'achievement/achievement-list'
                    controller: 'achievementListController'
                    controllerAs: 'ach'
        .state 'main.users',
            url: '/users'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'users/users-list'
                    controller: 'usersListController'
                    controllerAs: 'ulist'
        .state 'main.tape',
            url: '/tape??media&index&entityType&count&page'
            views:
                'socialContent@main':
                    templateUrl: tmplView 'tape/tape-view'
                    controller: 'tapeController'
                    controllerAs: 'tape'
        .state 'landing',
            url: '/'
            templateUrl: tmplView 'landing/index'
            controller: 'landingController'
        .state '404',
            url: '/404'
            templateUrl: tmplView 'errors/404'
        .state 'registration',
            url: '/registration'
            templateUrl: tmplView 'registration/index'
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
    'queryParamsService'
    ($templateCache
     $rootScope
     $state
     $stateParams
     modalService
     base
     queryParamsService)->

        view = angular.element '#ui-view'
        $templateCache.put(view.data('tmpl-url'), view.html())

        $rootScope.$state = $state
        $rootScope.$stateParams = $stateParams

        $rootScope.back = ->
            window.history.back()

        # global scripts init
        # ---------------
        NProgress.configure({ minimum: 0.3 })

        # ---------------
        $rootScope.$on '$stateChangeStart', (event, toState, toParams) ->
            if toState.data != undefined
                requireLogin = toState.data.requireLogin
            if requireLogin && $rootScope.user.id == undefined
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
                $rootScope.loader = true
                NProgress.start()

        # ---------------
        $rootScope.$on '$stateChangeSuccess', (event, toState, toParams, fromState, fromParams) ->
            modalService.closeAll()
            $rootScope.loader = false
            $rootScope.fullHeight = toState.fullHeight
            queryParamsService.check(toParams)
            NProgress.done()

        return
])

app.constant('templateUrl', '/template')
app.constant('defaultAvatarUrl', '/Content/images/default-avatar.png')
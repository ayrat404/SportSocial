'use strict';

// Declares how the application should be bootstrapped. See: http://docs.angularjs.org/guide/module
angular
    .module('app', [
        'ui.router',
        'ui.bootstrap',
        'ngCookies',
        'flow',
        'shared',
        'appSrvc',
        'socialApp'
    ])

    // Gets executed during the provider registrations and configuration phase. Only providers and constants can be
    // injected here. This is to prevent accidental instantiation of services before they have been fully configured.
    .config([
        '$stateProvider',
        '$locationProvider',
        '$httpProvider',
        function (
            $stateProvider,
            $locationProvider,
            $httpProvider) {

            var tmplUrl = '/Scripts/templates/',
                tmplView = function (viewUrl) {
                    return tmplUrl + viewUrl + '.html';
                };


            // routes
            // ---------------
            $stateProvider
                .state('landing', {
                    url: '/',
                    templateUrl: tmplView('landing/index'),
                    controller: 'LandingCtrl'
                })
                .state('registration', {
                    url: '/registration',
                    templateUrl: tmplView('registration/index'),
                    controller: 'RegistrationCtrl',
                    fullHeight: true
                });
                //.state('otherwise', {
                //    url: '*path',
                //    templateUrl: tmplView('home/404'),
                //    controller: 'Error404Ctrl'
                //});

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });

        }])

    // Gets executed after the injector is created and are used to kickstart the application. Only instances and constants
    // can be injected here. This is to prevent further system configuration during application run time.
    .run([
        '$templateCache',
        '$rootScope',
        '$state',
        '$stateParams',
        //'$modalInstance',
        function (
            $templateCache,
            $rootScope,
            $state,
            $stateParams) {

            // <ui-view> contains a pre-rendered template for the current view
            // caching it will prevent a round-trip to a server at the first page load
            // ---------------
            var view = angular.element('#ui-view');
            $templateCache.put(view.data('tmpl-url'), view.html());

            // Allows to retrieve UI Router state information from inside templates
            // ---------------
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            // global scripts init
            // ---------------
            NProgress.configure({ minimum: 0.3 });

            // ---------------
            $rootScope.$on('$stateChangeStart', function (event, toState) {
                $rootScope.loading = true;
                NProgress.start();
            });

            // ---------------
            $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                $rootScope.loading = false;
                $rootScope.fullHeight = toState.fullHeight;
                NProgress.done();
            });
        }]);
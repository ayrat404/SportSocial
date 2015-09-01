var app;

app = angular.module('app', ['ui.router', 'ui.bootstrap', 'ngCookies', 'flow', 'shared', 'appSrvc', 'socialApp']).config([
  '$stateProvider', '$locationProvider', '$httpProvider', '$urlRouterProvider', 'templateUrl', function($stateProvider, $locationProvider, $httpProvider, $urlRouterProvider, templateUrl) {
    var tmplView;
    tmplView = function(viewPath) {
      return templateUrl + '/' + viewPath;
    };
    $stateProvider.state('main', {
      abstract: true,
      views: {
        '@': {
          templateUrl: tmplView('main/_layout'),
          controller: 'mainSocialController as social'
        }
      }
    }).state('main.profile', {
      url: '/id:userId',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('profile/index'),
          controller: 'profileViewController as profile'
        }
      }
    }).state('landing', {
      url: '/',
      templateUrl: tmplView('landing/index'),
      controller: 'landingController'
    }).state('404', {
      url: '/404',
      templateUrl: tmplView('errors/404')
    }).state('registration', {
      url: '/registration',
      templateUrl: tmplView('registration/index'),
      controller: 'registrationController',
      fullHeight: true
    });
    $urlRouterProvider.otherwise('/404');
    $httpProvider.interceptors.push('apiInterseptorProvider');
    $locationProvider.html5Mode({
      enabled: true,
      requireBase: false
    });
  }
]).run([
  '$templateCache', '$rootScope', '$state', '$stateParams', function($templateCache, $rootScope, $state, $stateParams) {
    var view;
    view = angular.element('#ui-view');
    $templateCache.put(view.data('tmpl-url'), view.html());
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    NProgress.configure({
      minimum: 0.3
    });
    $rootScope.$on('$stateChangeStart', function(event, toState) {
      $rootScope.loading = true;
      return NProgress.start();
    });
    $rootScope.$on('$stateChangeSuccess', function(event, toState) {
      $rootScope.loading = false;
      $rootScope.fullHeight = toState.fullHeight;
      return NProgress.done();
    });
  }
]);

app.constant('templateUrl', '/template');

var app;

app = angular.module('app', ['ui.router', 'ui.bootstrap', 'angular-storage', 'flow', 'shared', 'appSrvc', 'socialApp']).config([
  '$stateProvider', '$locationProvider', '$httpProvider', '$urlRouterProvider', 'templateUrl', function($stateProvider, $locationProvider, $httpProvider, $urlRouterProvider, templateUrl) {
    var tmplView;
    tmplView = function(viewPath) {
      return templateUrl + '/' + viewPath;
    };
    $stateProvider.state('main', {
      abstract: true,
      data: {
        requireLogin: true
      },
      views: {
        '@': {
          templateUrl: tmplView('main/_layout'),
          controller: 'mainSocialController',
          controllerAs: 'social'
        }
      }
    }).state('main.profile', {
      url: '/id:userId',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('profile/index'),
          controller: 'profileViewController',
          controllerAs: 'profile'
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
      controller: 'registrationController'
    });
    $urlRouterProvider.otherwise('/404');
    $httpProvider.interceptors.push('apiInterseptorProvider');
    $locationProvider.html5Mode({
      enabled: true,
      requireBase: false
    });
  }
]).run([
  '$templateCache', '$rootScope', '$state', '$stateParams', 'modalService', 'base', 'userService', function($templateCache, $rootScope, $state, $stateParams, modalService, base, userService) {
    var view;
    view = angular.element('#ui-view');
    $templateCache.put(view.data('tmpl-url'), view.html());
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    NProgress.configure({
      minimum: 0.3
    });
    $rootScope.user = {};
    $rootScope.$on('$stateChangeStart', function(event, toState, toParams) {
      var requireLogin;
      if (toState.data !== void 0) {
        requireLogin = toState.data.requireLogin;
      }
      if (requireLogin && userService.get().id === void 0) {
        event.preventDefault();
        base.notice.show({
          text: 'The page you requested is available only to registered users',
          type: 'warning'
        });
        return modalService.show({
          name: 'loginSubmit',
          data: {
            success: function(res) {
              return $state.go(toState.name, toParams);
            },
            cancel: function(res) {
              $state.go('registration');
              return base.notice.show({
                text: 'Please register if you do not have an Fortress account ',
                type: 'info'
              });
            }
          }
        });
      } else {
        $rootScope.loading = true;
        return NProgress.start();
      }
    });
    $rootScope.$on('$stateChangeSuccess', function(event, toState) {
      $rootScope.loading = false;
      $rootScope.fullHeight = toState.fullHeight;
      return NProgress.done();
    });
  }
]);

app.constant('templateUrl', '/template');

(function(){
angular.module('app', ['ui.router', 'ui.bootstrap', 'ngCookies', 'flow', 'shared', 'appSrvc', 'socialApp']).config([
  '$stateProvider', '$locationProvider', '$httpProvider', function($stateProvider, $locationProvider, $httpProvider) {
    var tmplView;
    tmplView = function(viewUrl) {
      return '/template/' + viewUrl;
    };
    $stateProvider.state('main', {
      url: '/',
      views: {
        '@': {
          templateUrl: tmplView('main/_layout'),
          controller: 'mainSocialController'
        }
      }
    }).state('main.profile', {
      url: 'id:userId',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('profile/index'),
          controller: 'profileViewController'
        }
      }
    }).state('landing', {
      url: '/landing',
      templateUrl: tmplView('landing/index'),
      controller: 'landingController'
    }).state('registration', {
      url: '/registration',
      templateUrl: tmplView('registration/index'),
      controller: 'registrationController',
      fullHeight: true
    });
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

})();
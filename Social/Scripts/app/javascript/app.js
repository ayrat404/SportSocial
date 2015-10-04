(function(){
var app;

app = angular.module('app', ['ui.router', 'ui.bootstrap', 'angular-storage', 'flow', '720kb.socialshare', 'youtube-embed', 'shared', 'appSrvc', 'socialApp']).config([
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
      url: '/id:userId?media&index&entityType&count&page',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('profile/index'),
          controller: 'profileViewController',
          controllerAs: 'profile'
        }
      }
    }).state('main.journalIt', {
      url: '/record/:id?media&index&entityType',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('journal/view'),
          controller: 'recordViewController',
          controllerAs: 'record'
        }
      }
    }).state('main.achievementAdd', {
      url: '/achievement/add',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('achievement/achievement-submit'),
          controller: 'achievementSubmitController',
          controllerAs: 'ach'
        }
      }
    }).state('main.achievementView', {
      url: '/achievement/:id',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('achievement/achievement-view'),
          controller: 'achievementViewController',
          controllerAs: 'ach'
        }
      }
    }).state('main.achievementList', {
      url: '/achievements?actual&status&type&count&page',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('achievement/achievement-list'),
          controller: 'achievementListController',
          controllerAs: 'ach'
        }
      }
    }).state('main.users', {
      url: '/users?country&city&age&insport&query&count&page',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('users/users-list'),
          controller: 'usersListController',
          controllerAs: 'ulist'
        }
      }
    }).state('main.payment', {
      url: '/payment',
      data: {
        payment: true
      },
      views: {
        'socialContent@main': {
          templateUrl: tmplView('payment/payment-index'),
          controller: 'paymentIndexController',
          controllerAs: 'pay'
        }
      }
    }).state('main.tape', {
      url: '/tape??media&index&entityType&count&page',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('tape/tape-view'),
          controller: 'tapeController',
          controllerAs: 'tape'
        }
      }
    }).state('main.settings', {
      abstract: true,
      url: '/settings',
      views: {
        'socialContent@main': {
          templateUrl: tmplView('settings/settings-layout')
        }
      }
    }).state('main.settings.profile', {
      url: '/profile',
      templateUrl: tmplView('settings/settings-profile'),
      controller: 'settingsProfileController',
      controllerAs: 'sett'
    }).state('main.settings.account', {
      url: '/account',
      templateUrl: tmplView('settings/settings-account'),
      controller: 'settingsAccountController',
      controllerAs: 'sett'
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
  '$templateCache', '$rootScope', '$state', '$stateParams', 'modalService', 'base', 'loginService', 'queryParamsService', function($templateCache, $rootScope, $state, $stateParams, modalService, base, loginService, queryParamsService) {
    var view;
    view = angular.element('#ui-view');
    $templateCache.put(view.data('tmpl-url'), view.html());
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    $rootScope.back = function() {
      return window.history.back();
    };
    $rootScope.logout = function() {
      return loginService.logout();
    };
    NProgress.configure({
      minimum: 0.3
    });
    $rootScope.$on('$stateChangeStart', function(event, toState, toParams) {
      var requireLogin;
      if (toState.data !== void 0) {
        requireLogin = toState.data.requireLogin;
      }
      if (requireLogin && $rootScope.user.id === void 0) {
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
        $rootScope.loader = true;
        return NProgress.start();
      }
    });
    $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams) {
      modalService.closeAll();
      $rootScope.loader = false;
      $rootScope.fullHeight = toState.fullHeight;
      queryParamsService.check(toParams);
      return NProgress.done();
    });
  }
]);

app.constant('templateUrl', '/template');

app.constant('defaultAvatarUrl', '/Content/images/default-avatar.png');

})();
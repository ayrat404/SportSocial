var apiInterseptor;

apiInterseptor = (function() {
  function apiInterseptor() {
    this.$get = [
      '$q', '$injector', '$timeout', 'base', 'servicesDefault', function($q, $injector, $timeout, base, servicesDefault) {
        return {
          'responseError': function(res) {
            var deferred;
            $timeout(function() {
              var $http, $state, modalService;
              modalService = $injector.get('modalService');
              base = $injector.get('base');
              $http = $injector.get('$http');
              return $state = $injector.get('$state');
            });
            if (servicesDefault.noticeShow.errors) {
              base.notice.show({
                text: 'Error ' + res.status + ': ' + res.statusText + '<br>' + res.data.message,
                type: 'warning'
              });
            }
            if (res.status !== 401) {
              return res;
            }
            deferred = $q.defer();
            modalService.show({
              name: 'loginSubmit',
              data: {
                success: function(res) {
                  return deferred.resolve($http(res.config));
                },
                cancel: function(res) {
                  $state.go('registration');
                  base.notice.show({
                    text: 'Please register if you do not have an Fortress account ',
                    type: 'info'
                  });
                  return deferred.reject(res);
                }
              }
            });
            return deferred.promise;
          }
        };
      }
    ];
  }

  return apiInterseptor;

})();

angular.module('shared').provider('apiInterseptorProvider', [apiInterseptor]);

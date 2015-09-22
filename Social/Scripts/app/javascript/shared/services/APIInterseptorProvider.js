var apiInterseptor;

apiInterseptor = (function() {
  function apiInterseptor() {
    this.$get = [
      '$q', '$injector', '$timeout', 'base', 'srvcConfig', function($q, $injector, $timeout, base, srvcConfig) {
        var modalService;
        modalService = {};
        $timeout(function() {
          var $http, $state;
          modalService = $injector.get('modalService');
          $http = $injector.get('$http');
          return $state = $injector.get('$state');
        });
        return {
          'response': function(res) {
            var noticeClass;
            if (res.data) {
              noticeClass = res.data.success === true ? 'success' : 'warning';
              if (res.data.message && res.data.message.length) {
                base.notice.show({
                  text: res.data.message,
                  type: noticeClass
                });
              }
            }
            return res;
          },
          'responseError': function(res) {
            var deferred;
            if (srvcConfig.noticeShow.errors) {
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

var apiInterseptor;

apiInterseptor = (function() {
  function apiInterseptor() {
    this.$get = [
      'base', 'servicesDefault', function(base, servicesDefault) {
        return {
          'responseError': function(res) {
            if (servicesDefault.noticeShow.errors) {
              base.notice.show({
                text: 'Error ' + res.status + ': ' + res.statusText + '<br>' + res.data.message,
                type: 'warning'
              });
            }
            return res;
          }
        };
      }
    ];
  }

  return apiInterseptor;

})();

angular.module('shared').provider('apiInterseptorProvider', [apiInterseptor]);

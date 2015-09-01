var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit($q, $http, $location, $rootScope, base, servicesDefault) {
    var submit, url, valid;
    url = servicesDefault.baseServiceUrl + '/journal__add';
    valid = {
      minText: 50
    };
    submit = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.text && data.text.length >= valid.minText) {
          return $http.post(url, data).then(function(res) {
            if (res.success) {
              resolve(res);
            } else {
              reject(res);
            }
            if (servicesDefault.noticeShow.errors) {
              return base.notice.response(res);
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          reject();
          if (servicesDefault.noticeShow.errors) {
            return base.notice.show({
              text: 'Journal submit validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      submit: submit
    };
  }

  return JournalSubmit;

})();

angular.module('appSrvc').service('journalSubmitService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', JournalSubmit]);

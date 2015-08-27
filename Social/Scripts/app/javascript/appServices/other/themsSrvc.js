(function(){
var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit($http, $q, servicesDefault, base) {
    var get, url;
    url = servicesDefault + '/sport_themes';
    get = function(search) {
      return $q(function(resolve, reject) {
        if (search && search.length) {
          return $http.get(url, {
            search: search
          }).then(function(res) {
            return resolve(res);
          }, function(res) {
            reject();
            if (servicesDefault.showNotice) {
              return base.notice.show({
                text: 'Search theme server error',
                type: 'danger'
              });
            }
          });
        } else {
          reject();
          if (servicesDefault.showNotice) {
            return base.notice.show({
              text: 'Search theme string error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      get: get
    };
  }

  return JournalSubmit;

})();

angular.module('appSrvc').service('journalSubmitService', ['$http', '$q', 'servicesDefault', 'base', JournalSubmit]);

})();
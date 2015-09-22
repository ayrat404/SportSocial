var Cup;

Cup = (function() {
  function Cup($http, $q, store, srvcConfig, base) {
    var cupStorage, getByExercise, url;
    url = srvcConfig.baseServiceUrl + '/cups';
    cupStorage = store.getNamespacedStore(srvcConfig.storeName + ".cup");
    cupStorage.set('version', srvcConfig.version);
    getByExercise = function(exercise) {
      return $q(function(resolve, reject) {
        if (exercise) {
          if (cupStorage.get(exercise)) {
            if (cupStorage.get('version') === srvcConfig.version) {
              return resolve(cupStorage.get(exercise));
            } else {
              return cupStorage.remove(exercise);
            }
          } else {
            return $http.get(url, {
              params: {
                exercise: exercise
              }
            }).then(function(res) {
              if (res.data.success) {
                cupStorage.set(exercise, res.data.data);
                return resolve(res.data);
              } else {
                return reject(res.data);
              }
            }, function(res) {
              return reject(res);
            });
          }
        } else {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Search theme string error',
              type: 'danger'
            });
          }
          return reject();
        }
      });
    };
    return {
      getByExercise: getByExercise
    };
  }

  return Cup;

})();

angular.module('appSrvc').service('cupService', ['$http', '$q', 'store', 'srvcConfig', 'base', Cup]);

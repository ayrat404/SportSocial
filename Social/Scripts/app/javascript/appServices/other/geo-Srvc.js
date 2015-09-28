(function(){
var Geo;

Geo = (function() {
  function Geo($q, $http, srvcConfig, RequestConstructor) {
    var facade, rqst, urlCity, urlCountry;
    urlCountry = srvcConfig.baseServiceUrl + '/geo/country';
    urlCity = srvcConfig.baseServiceUrl + '/geo/city';
    rqst = {
      getCountry: new RequestConstructor.klass('get', urlCountry),
      getCity: new RequestConstructor.klass('get', urlCity)
    };
    facade = {
      getCountry: rqst.getCountry["do"],
      getCity: rqst.getCity["do"]
    };
    return facade;
  }

  return Geo;

})();

angular.module('appSrvc').service('geoService', ['$q', '$http', 'srvcConfig', 'RequestConstructor', Geo]);

})();
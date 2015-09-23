(function(){
var QueryParams;

QueryParams = (function() {
  function QueryParams(modalService) {
    var check, queryServices;
    queryServices = {
      media: function(params) {
        return modalService.show({
          name: 'mediaShow',
          data: params
        });
      }
    };
    check = function(params) {
      var k, results, v;
      results = [];
      for (k in params) {
        v = params[k];
        if (queryServices[k] !== void 0 && v !== void 0) {
          results.push(queryServices[k](params));
        } else {
          results.push(void 0);
        }
      }
      return results;
    };
    return {
      check: check
    };
  }

  return QueryParams;

})();

angular.module('shared').service('queryParamsService', ['modalService', QueryParams]);

})();
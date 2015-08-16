(function(){
var landing;
landing = function () {
  function landing() {
    return {
      restrict: 'A',
      link: function (scope, element, attrs) {
      }
    };
  }
  return landing;
}();
angular.module('socialApp.directives').directive('landing', [landing]);
}).call(this);
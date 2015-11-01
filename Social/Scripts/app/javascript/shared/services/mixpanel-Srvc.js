(function(){
angular.module('shared').factory('mixpanel', [
  '$location', function($location) {
    var api;
    api = function(action, event, prop) {
      if (window.mixpanel === null || window.mixpanel === void 0) {
        console.log('mixpanel: mixpanel isn\'t defined');
        return;
      }
      if (typeof mixpanel[action] !== 'function') {
        console.log('mixpanel: method "mixpanel.' + action + '" is not defined');
        return;
      }
      switch (action) {
        case 'identify':
          return mixpanel.identify(event, prop);
        case 'people.set':
          return mixpanel.people.set(event, prop);
        default:
          return mixpanel[action](event, prop);
      }
    };
    return {
      api: api,
      ev: {
        visitPage: function(title) {
          return api('track', 'visit page', {
            title: title,
            url: $location.path()
          });
        }
      }
    };
  }
]);

})();
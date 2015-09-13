var socialShare;

socialShare = (function() {
  function socialShare($location) {
    var defaults;
    defaults = {
      socials: ['vk', 'twitter', 'facebook', 'google+'],
      counters: false
    };
    return {
      restrict: 'E',
      replace: true,
      templateUrl: '/template/components/social-shareTpl',
      link: function(scope, element, attr) {
        var attributeName, k, propDefaults, properties, v;
        properties = {};
        propDefaults = {
          'url': '',
          'counters': '',
          'socials': '',
          'text': '',
          'title': ''
        };
        for (k in propDefaults) {
          v = propDefaults[k];
          if (propDefaults.hasOwnProperty(k)) {
            attributeName = 'ss' + k.substring(0, 1).toUpperCase() + k.substring(1);
            if (properties[k] === void 0) {
              properties[k] = propDefaults[k];
            }
            if (attr[attributeName] !== void 0) {
              properties[k] = attr[attributeName];
            }
          }
        }
        scope.prop = properties;
        return scope.list = defaults.socials;
      }
    };
  }

  return socialShare;

})();

angular.module('shared').directive('socialShare', ['$location', socialShare]);

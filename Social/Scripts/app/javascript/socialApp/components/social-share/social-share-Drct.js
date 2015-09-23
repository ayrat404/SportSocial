(function(){
var socialShareList;

socialShareList = (function() {
  function socialShareList($location) {
    var defaults;
    defaults = {
      socials: ['vk', 'twitter', 'facebook', 'google+'],
      counters: false
    };
    return {
      restrict: 'E',
      replace: true,
      scope: {
        prop: '='
      },
      templateUrl: '/template/components/social-shareTpl',
      link: function(scope, element, attr) {
        var k, propDefaults, properties, v;
        propDefaults = {
          url: $location.absUrl(),
          title: 'Fortress. Sport social network.',
          text: 'Test text test text test text',
          media: '~/Content/socialApp/images/common/logo-big.png',
          hashtags: 'fortress, sport, fitness'
        };
        properties = {};
        if (scope.prop.media && scope.prop.media.length && typeof scope.prop.media !== 'string') {
          scope.prop.media = scope.prop.media[0].url;
        }
        if (scope.prop.hashtags && scope.prop.hashtags.length) {
          scope.prop.hashtags = scope.prop.hashtags.join(', ');
        }
        for (k in propDefaults) {
          v = propDefaults[k];
          if (propDefaults.hasOwnProperty(k)) {
            if (scope.prop[k]) {
              properties[k] = scope.prop[k];
            } else {
              properties[k] = propDefaults[k];
            }
          }
        }
        scope.prop = properties;
        return scope.providerList = defaults.socials;
      }
    };
  }

  return socialShareList;

})();

angular.module('shared').directive('socialShareList', ['$location', socialShareList]);

})();
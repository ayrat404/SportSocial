var mixpanelEvent;

mixpanelEvent = (function() {
  function mixpanelEvent(mixpanel, $location) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var nt;
        nt = attrs.name || attrs.title || '';
        if (nt.trim() !== '') {
          return angular.element(element).on(attrs.mixpanelEvent, function() {
            return mixpanel.api('track', ['on', '"' + nt + '"', attrs.mixpanelEvent].join(' '), {
              url: $location.path(),
              title: scope.$root.title
            });
          });
        } else {
          return console.log('mixpanel event on element: invalid name or title');
        }
      }
    };
  }

  return mixpanelEvent;

})();

angular.module('shared').directive('mixpanelEvent', ['mixpanel', '$location', mixpanelEvent]);

(function(){
var TrialVisual;

TrialVisual = (function() {
  function TrialVisual() {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        currentDays: '=',
        allDays: '='
      },
      templateUrl: '/template/components/payment-trialTpl',
      link: function(scope, element, attrs) {
        var canvas, circ, ctx, draw, quart;
        canvas = element.find('canvas');
        ctx = canvas[0].getContext('2d');
        circ = Math.PI * 2;
        quart = Math.PI / 2;
        draw = function(current) {
          ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
          ctx.beginPath();
          ctx.arc(85, 85, 70, 0, 2 * Math.PI, false);
          ctx.lineWidth = 1;
          ctx.strokeStyle = '#c8cdcf';
          ctx.stroke();
          ctx.closePath();
          ctx.beginPath();
          ctx.strokeStyle = '#1fba2a';
          ctx.lineWidth = 3.0;
          ctx.arc(85, 85, 70, -quart, (circ * current) - quart, false);
          ctx.stroke();
          return ctx.closePath();
        };
        return draw(scope.currentDays / scope.allDays);
      }
    };
  }

  return TrialVisual;

})();

angular.module('socialApp.directives').directive('trialVisual', [TrialVisual]);

})();
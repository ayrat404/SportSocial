(function(){
var imageResize;

imageResize = (function() {
  function imageResize(base) {
    return function(imageSrc, params) {
      params.mode = 'crop';
      return base.image.resize(imageSrc, params);
    };
  }

  return imageResize;

})();

angular.module('shared').filter('imageResize', ['base', imageResize]);

})();
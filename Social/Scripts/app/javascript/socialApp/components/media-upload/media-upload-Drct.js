(function(){
var MediaUpload;

MediaUpload = (function() {
  function MediaUpload() {
    return {
      restrict: 'EA',
      require: 'ngModel',
      replace: true,
      scope: {
        media: '=ngModel'
      },
      controller: 'mediaUploadController',
      templateUrl: '/template/components/media-uploadTpl'
    };
  }

  return MediaUpload;

})();

angular.module('socialApp.directives').directive('mediaUpload', [MediaUpload]);

})();
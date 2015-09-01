var MediaUpload;

MediaUpload = (function() {
  function MediaUpload($scope, youtubeVideoService, base) {
    if (base.isArray($scope.media)) {
      $scope.media = [];
    }
    $scope.sendVideoLink = function(link) {
      var videoProp;
      videoProp = {
        size: {
          w: 45,
          h: 45
        }
      };
      return youtubeVideoService.getVideoInfo(link, videoProp).then(function(res) {
        return $scope.media.push({
          id: res.data.id,
          type: 'video',
          img: res.data.img
        });
      });
    };
    $scope.imgResponse = function(res) {
      if (res.success) {
        return $scope.media.push({
          id: res.data.id,
          type: 'image',
          img: res.data.url
        });
      }
    };
  }

  return MediaUpload;

})();

angular.module('socialApp.controllers').controller('mediaUploadController', ['$scope', 'youtubeVideoService', 'base', MediaUpload]);

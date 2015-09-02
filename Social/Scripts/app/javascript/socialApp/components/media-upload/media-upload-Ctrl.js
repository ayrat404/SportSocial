var MediaUpload;

MediaUpload = (function() {
  function MediaUpload($scope, youtubeVideoService, base) {
    var size;
    size = {
      w: 45,
      h: 45,
      mode: 'crop'
    };
    if (base.isArray($scope.media)) {
      $scope.media = [];
    }
    $scope.sendVideoLink = function(link) {
      return youtubeVideoService.getVideoInfo(link).then(function(res) {
        $scope.youtubeLink = '';
        return $scope.media.push({
          id: res.data.id,
          type: 'video',
          img: base.image.resize(res.data.img, size)
        });
      });
    };
    $scope.imgResponse = function(stringData) {
      var obj;
      obj = angular.fromJson(stringData);
      if (obj.success) {
        return $scope.media.push({
          id: obj.data.id,
          type: 'image',
          img: base.image.resize(obj.data.url, size)
        });
      }
    };
  }

  return MediaUpload;

})();

angular.module('socialApp.controllers').controller('mediaUploadController', ['$scope', 'youtubeVideoService', 'base', MediaUpload]);

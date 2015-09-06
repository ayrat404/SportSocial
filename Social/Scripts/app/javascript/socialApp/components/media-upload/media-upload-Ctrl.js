var MediaUpload;

MediaUpload = (function() {
  function MediaUpload($scope, youtubeVideoService, base) {
    if (base.isArray($scope.media)) {
      $scope.media = [];
    }
    $scope.sendVideoLink = function(link) {
      return youtubeVideoService.getVideoInfo(link).then(function(res) {
        $scope.youtubeLink = '';
        return $scope.media.push({
          id: res.data.id,
          type: 'video',
          img: res.data.img
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
          img: obj.data.url
        });
      }
    };
  }

  return MediaUpload;

})();

angular.module('socialApp.controllers').controller('mediaUploadController', ['$scope', 'youtubeVideoService', 'base', MediaUpload]);

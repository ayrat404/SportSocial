class MediaUpload extends Controller('socialApp.controllers')
    constructor: (
        $scope
        youtubeVideoService
        base)->

        if base.isArray($scope.media)
            $scope.media = []

        # send youtube link & get video params
        # ---------------
        $scope.sendVideoLink = (link)->
            youtubeVideoService.getVideoInfo(link).then((res)->
                # {red.data = { id: xxx, remoteId: xxx, img: xxx }}
                $scope.youtubeLink = ''
                $scope.media.push(
                    id: res.data.id
                    type: 'video'
                    img: res.data.img
                ))

        # image upload
        # ---------------
        $scope.imgResponse = (stringData)->
            obj = angular.fromJson(stringData);
            if obj.success
                $scope.media.push(
                    id: obj.data.id
                    type: 'image'
                    img: obj.data.url
                )

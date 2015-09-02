class MediaUpload extends Controller('socialApp.controllers')
    constructor: (
        $scope
        youtubeVideoService
        base)->

        size =
            w: 45
            h: 45
            mode: 'crop'

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
                    img: base.image.resize(res.data.img, size)
                ))

        # image upload
        # ---------------
        $scope.imgResponse = (stringData)->
            obj = angular.fromJson(stringData);
            if obj.success
                $scope.media.push(
                    id: obj.data.id
                    type: 'image'
                    img: base.image.resize(obj.data.url, size)
                )

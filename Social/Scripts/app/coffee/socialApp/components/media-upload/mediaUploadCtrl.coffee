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
            # video object params
            videoProp =
                size:
                    w: 45
                    h: 45
            youtubeVideoService.getVideoInfo(link, videoProp).then((res)->
                # {red.data = { id: xxx, remoteId: xxx, img: xxx }}
                $scope.media.push(
                    id: res.data.id
                    type: 'video'
                    img: res.data.img
                ))

        # image upload
        # ---------------
        $scope.imgResponse = (res)->
            if res.success
                $scope.media.push(
                    id: res.data.id
                    type: 'image'
                    img: res.data.url
                )

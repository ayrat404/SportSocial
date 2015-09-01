class YoutubeVideo extends Service('appSrvc')
    constructor: (
        $http
        $q
        servicesDefault
        base)->

        # youtube video info get
        # ---------------
        getVideoInfo = (link, prop)->
            if link &&
              typeof link == 'string' &&
              link.length > 0
                $q (resolve, reject)->
                    $http.post(servicesDefault.baseServiceUrl + '/youtube', { link: link }).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                    , (res)->
                        reject(res)
                    )
            else
                reject()

        return {
            getVideoInfo: getVideoInfo
        }
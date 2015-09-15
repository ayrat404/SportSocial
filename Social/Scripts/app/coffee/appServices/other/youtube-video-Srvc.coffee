class YoutubeVideo extends Service('appSrvc')
    constructor: (
        $http
        $q
        servicesDefault
        base)->

        url = servicesDefault.baseServiceUrl + '/youtube'

        # youtube video info get
        # ---------------
        getVideoInfo = (link, prop)->
            $q (resolve, reject)->
                if link &&
                  typeof link == 'string' &&
                  link.length > 0
                    $http.post(url, { link: link }).then((res)->
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
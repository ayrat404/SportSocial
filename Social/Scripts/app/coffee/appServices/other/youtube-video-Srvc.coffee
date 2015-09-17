class YoutubeVideo extends Service('appSrvc')
    constructor: (
        $http
        $q
        servicesDefault
        base)->

        url = servicesDefault.baseServiceUrl + '/youtube'

        # youtube video info get
        # ---------------
        getVideoInfo = (data)->
            $q (resolve, reject)->
                if data &&
                  typeof data.link == 'string' &&
                  data.type
                    $http.post(url, data).then((res)->
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
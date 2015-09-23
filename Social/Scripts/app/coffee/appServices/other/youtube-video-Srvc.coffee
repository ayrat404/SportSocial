class YoutubeVideo extends Service('appSrvc')
    constructor: (
        $http
        $q
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/youtube'

        rqst =
            getVideoInfo: new RequestConstructor.klass 'post', url, (data)->
                if !data || !typeof data.link == 'string' || !data.type
                    return false
                true

        facade =
            getVideoInfo: rqst.getVideoInfo.do

        return facade

#        # youtube video info get
#        # ---------------
#        getVideoInfo = (data)->
#            $q (resolve, reject)->
#                if data &&
#                  typeof data.link == 'string' &&
#                  data.type
#                    $http.post(url, data).then((res)->
#                        if res.data.success
#                            resolve(res.data)
#                        else
#                            reject(res.data)
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    reject()
#
#        return {
#            getVideoInfo: getVideoInfo
#        }
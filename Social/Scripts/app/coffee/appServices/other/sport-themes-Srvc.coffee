class SportThemes extends Service('appSrvc')
    constructor: (
        srvcConfig
        base
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/sport_themes'

        rqst =
            get: new RequestConstructor.klass 'get', url, (data)->
                if !data || !data.query
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Search theme string error',
                            type: 'danger'
                    return false
                true

        facade =
            get: rqst.get.do

        return facade

#        get = (search)->
#            $q (resolve, reject)->
#                if search && search.length
#                    $http.get(url, { params: {query: search}}).then((res)->
#                        if res.data.success && res.data.data.length
#                            resolve(res.data)
#                        else
#                            reject(res.data)
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Search theme string error',
#                            type: 'danger'
#                    reject()
#
#        return {
#            get: get
#        }

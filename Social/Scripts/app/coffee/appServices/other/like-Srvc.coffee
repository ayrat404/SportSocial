class Like extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/like'

        validate = (data)->
            if !data || !data.entityType || !data.id || typeof data.current != 'boolean'
                if srvcConfig.noticeShow.errors
                    base.notice.show
                        text: 'Like validation error'
                        type: 'danger'
                return false
            true

        rqst =
            post: new RequestConstructor.klass 'post', url

        facade =
            set: (data)->
                if validate data
                    data.actionType = if data.current then 'remove' else 'like'
                    return rqst.post.do

        return facade

#        # like post (type: x, id: x, current: true/false)
#        # ---------------
#        set = (data)->
#            $q (resolve, reject)->
#                if data && data.entityType && data.id && typeof data.current == 'boolean'
#                    data.actionType = if data.current then 'remove' else 'like'
#                    $http.post(url, data).then((res)->
#                        if res.data.success
#                            resolve(!data.current)
#                        else
#                            reject(res.data)
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show(
#                            text: 'Like validation error'
#                            type: 'danger'
#                        )
#
#        return {
#            set: set
#        }
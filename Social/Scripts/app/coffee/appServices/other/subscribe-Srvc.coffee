class Subscribe extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/subscribe'

        validate = (data)->
            if !data || !data.id || typeof data.current != 'boolean'
                if srvcConfig.noticeShow.errors
                    base.notice.show
                        text: 'Subscribe validation error'
                        type: 'danger'
                return false
            true

        rqst =
            set: new RequestConstructor.klass 'post', url

        facade =
            set: (data)->
                if validate data
                    data.actionType = if data.current then 'remove' else 'like'
                    return rqst.set.do

        return facade

#        # like post (type: x, id: x, current: true/false)
#        # ---------------
#        set = (data)->
#            $q (resolve, reject)->
#                if data && data.id && typeof data.current == 'boolean'
#                    data.actionType = if data.current then 'unsubscribe' else 'subscribe'
#                    $http.post(url, data).then (res)->
#                        if res.data.success
#                            resolve !data.current
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Subscribe validation error'
#                            type: 'danger'
#
#        return {
#            set: set
#        }
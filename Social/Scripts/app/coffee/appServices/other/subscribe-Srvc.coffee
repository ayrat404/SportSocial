class Subscribe extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        srvcConfig)->

        url = srvcConfig.baseServiceUrl + '/subscribe'

        # like post (type: x, id: x, current: true/false)
        # ---------------
        set = (data)->
            $q (resolve, reject)->
                if data && data.id && typeof data.current == 'boolean'
                    data.actionType = if data.current then 'unsubscribe' else 'subscribe'
                    $http.post(url, data).then (res)->
                        if res.data.success
                            resolve !data.current
                        else
                            reject res.data
                    , (res)->
                        reject res
                else
                    reject()
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Subscribe validation error'
                            type: 'danger'

        return {
            set: set
        }
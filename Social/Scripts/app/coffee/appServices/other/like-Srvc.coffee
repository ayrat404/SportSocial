class Like extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        srvcConfig)->

        url = srvcConfig.baseServiceUrl + '/like'

        # like post (type: x, id: x, current: true/false)
        # ---------------
        set = (data)->
            $q (resolve, reject)->
                if data && data.entityType && data.id && typeof data.current == 'boolean'
                    data.actionType = if data.current then 'remove' else 'like'
                    $http.post(url, data).then((res)->
                        if res.data.success
                            resolve(!data.current)
                        else
                            reject(res.data)
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if srvcConfig.noticeShow.errors
                        base.notice.show(
                            text: 'Like validation error'
                            type: 'danger'
                        )

        return {
            set: set
        }
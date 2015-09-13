class Like extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/like'

        # like post (type: x, id: x, current: true/false)
        # ---------------
        set = (data)->
            $q (resolve, reject)->
                if data && data.entityType && data.id && typeof data.current == 'boolean'
                    data.actionType = if data.current then 'dislike' else 'like'
                    $http.post(url, data).then((res)->
                        if res.data.success
                            resolve(!data.current)
                        else
                            reject(res.data)
                            base.notice.response(res) if servicesDefault.noticeShow.errors
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Like validation error'
                            type: 'danger'
                        )

        return {
            set: set
        }
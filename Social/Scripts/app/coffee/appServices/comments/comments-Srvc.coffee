class Comments extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/comment'
        valid =
            minText: 50

        # submit new comment
        # ---------------
        submit = (data)->
            $q (resolve, reject)->
                # todo validate
                if data
                    $http.post(url, data).then((res)->
                        if res.data.success
                            resolve(res.data)
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
                            text: 'Comment submit validate error'
                            type: 'danger'
                        )


        # remove journal item
        # ---------------
        remove = (itemId)->
            $q (resolve, reject)->
                if itemId && typeof itemId == 'number'
                    $http.delete(url, { params: { id: itemId } }).then((res)->
                        if res.data.success
                            resolve(res.data)
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
                            text: 'Comment delete: itemId variable error'
                            type: 'danger'
                        )



        return {
            submit: submit
            remove: remove
        }
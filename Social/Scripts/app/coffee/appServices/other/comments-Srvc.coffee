class Comments extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/comment'

        rqst =

            # submit new comment
            # ---------------
            post: new RequestConstructor.klass 'post', url, (data)->
                if !data
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Comment submit validate error'
                            type: 'danger'
                    return false
                true

            # remove journal item
            # ---------------
            remove: new RequestConstructor.klass 'delete', url, (data)->
                if !data || !data.id
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Comment delete: itemId variable error'
                            type: 'danger'
                    return false
                true

        facade =
            submit: rqst.post.do
            remove: rqst.remove.do

        return facade


        # submit new comment
        # ---------------
#        submit = (data)->
#            $q (resolve, reject)->
#                # todo validate
#                if data
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
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show(
#                            text: 'Comment submit validate error'
#                            type: 'danger'
#                        )
#
#
        # remove journal item
        # ---------------
#        remove = (itemId)->
#            $q (resolve, reject)->
#                if itemId && typeof itemId == 'number'
#                    $http.delete(url, { params: { id: itemId } }).then((res)->
#                        if res.data.success
#                            resolve(res.data)
#                        else
#                            reject(res.data)
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show(
#                            text: 'Comment delete: itemId variable error'
#                            type: 'danger'
#                        )


#
#        return {
#            submit: submit
#            remove: remove
#        }
class Complain extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/complain'

        # submit new complain
        # ---------------
        submitRqst = new RequestConstructor.klass 'post', url, (data)->
            if !data || !data.entityId || !data.userId || !data.type || !data.text
                if srvcConfig.noticeShow.errors
                    base.notice.show
                        text: 'Complain submit validate error'
                        type: 'danger'
                return false
            true

        facade =
            submit: submitRqst.do

        return facade

#        submit = (data)->
#            $q (resolve, reject)->
#                if data &&
#                  data.entityId &&
#                  data.userId &&
#                  data.type &&
#                  data.text
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
#                            text: 'Complain submit validate error'
#                            type: 'danger'
#                        )

#        return {
#            submit: submit
#        }
class Complain extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/complain'

        # submit new complain
        # ---------------
        submit = (data)->
            $q (resolve, reject)->
                if data &&
                  data.entityId &&
                  data.userId &&
                  data.type &&
                  data.text
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
                            text: 'Complain submit validate error'
                            type: 'danger'
                        )

        return {
            submit: submit
        }
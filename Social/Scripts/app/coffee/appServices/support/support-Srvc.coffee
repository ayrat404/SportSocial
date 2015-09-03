class support extends Service('appSrvc')
    constructor: (
        $state,
        $location,
        $q,
        $rootScope,
        $http,
        base,
        mixpanel,
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/support'
        isSending = false

        # send question func ({ name: x, email: x, problem: x })
        # ---------------
        sendQuestion = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q (resolve, reject)->
                if data &&
                  base.validate.email(data.email) &&
                  data.name &&
                  data.problem &&
                  !isSending
                    mixpanel.api 'track', 'Support__send', evTrackProp
                    isSending = true
                    $http.post(url, data).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                            base.notice.response(res) if opts.noticeShow.errors
                    , (res)->
                        reject(res)
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
                        base.notice.show(
                            text: 'Support validation error'
                            type: 'danger'
                        )

        return {
            send: sendQuestion
        }



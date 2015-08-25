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
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.showNotice
                    , (res)->
                        reject(res)
                        if opts.showNotice
                            base.notice.show(
                                text: 'Support server error'
                                type: 'danger'
                            )
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.showNotice
                        base.notice.show(
                            text: 'Support validation error'
                            type: 'danger'
                        )

        return {
            send: sendQuestion
        }



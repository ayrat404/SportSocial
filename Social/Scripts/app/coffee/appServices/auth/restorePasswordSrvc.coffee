class restorePassword extends Service('appSrvc')
    constructor: (
        $state,
        $location,
        $q,
        $rootScope,
        $http,
        base,
        mixpanel,
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/restorePassword'
        isPhoneSending = false
        isNewPassSending = false


        # send func
        # ---------------
        send = (action, data)->
            $http.post([url, action].join('/'), data)

        # send phone ({ phone: x })
        # ---------------
        sendPhone = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data && data.phone && !isPhoneSending
                    isPhoneSending = true
                    mixpanel.api('track', 'RestorePassword__phone-send', evTrackProp)
                    send('phone', data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.showNotice
                    , (res)->
                        reject(res)
                        if opts.showNotice
                            base.notice.show(
                                text: 'Restore phone server error'
                                type: 'danger'
                            )
                    ).finally(->
                        isPhoneSending = false
                    )
                else
                    reject()
                    if opts.showNotice
                        base.notice.show(
                            text: 'Restore phone validation error'
                            type: 'danger'
                        )
            )

        # send new password ({ password: x, passwordRepeat: x, code: x })
        # ---------------
        sendNewPassword = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data && data.phone && data.password && !isNewPassSending
                    isNewPassSending = true
                    mixpanel.api('track', 'RestorePassword__new-password-send', evTrackProp)
                    send('new', data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.showNotice
                    , (res)->
                        reject(res)
                        if opts.showNotice
                            base.notice.show(
                                text: 'Restore new password server error'
                                type: 'danger'
                            )
                    ).finally(->
                        isNewPassSending = false
                    )
                else
                    reject()
                    if opts.showNotice
                        base.notice.show(
                            text: 'Restore new password validation error'
                            type: 'danger'
                        )
            )

        return {
            sendPhone: sendPhone
            sendNewPassword: sendNewPassword
        }
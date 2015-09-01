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

        urlOne = servicesDefault.baseServiceUrl + '/restore_password_one'
        urlTwo = servicesDefault.baseServiceUrl + '/restore_password_two'
        isPhoneSending = false
        isNewPassSending = false

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
                    $http(urlOne, data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.noticeShow.errors
                    , (res)->
                        reject(res)
                    ).finally(->
                        isPhoneSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
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
                    $http(urlTwo, data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                            base.notice.response(res) if opts.noticeShow.errors
                    , (res)->
                        reject(res)
                    ).finally(->
                        isNewPassSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
                        base.notice.show(
                            text: 'Restore new password validation error'
                            type: 'danger'
                        )
            )

        return {
            sendPhone: sendPhone
            sendNewPassword: sendNewPassword
        }
﻿# CoffeeScript
class registration extends Service('appSrvc')
    constructor: (
        $state
        $location
        $q
        $rootScope
        $http
        base
        mixpanel
        srvcConfig) ->

        urlFirst = srvcConfig.baseServiceUrl + '/register/step1'
        urlTwo = srvcConfig.baseServiceUrl + '/register/step2'
        isSending = false

        # (data: { name: x, sername: x, birthday: x, gender: x, sportTime: x  }, opts: {...})
        # ---------------
        registerFirst = (data, prop)->
            opts = angular.extend(srvcConfig, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data &&
                  data.imgId &&
                  data.name &&
                  data.lastName &&
                  data.birthday &&
                  data.sportTime &&
                  data.phone &&
                  data.gender &&
                  !isSending
                    isSending = true
                    mixpanel.api('track', 'Registration__1-step__send', evTrackProp)
                    $http.post(urlFirst, data).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                    , (res)->
                        reject(res)
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
                        base.notice.show(
                            text: 'Registration first step validation error'
                            type: 'danger'
                        )
            )

        registerTwo = (data, prop)->
            opts = angular.extend(srvcConfig, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q((resolve, reject)->
                if data &&
                  data.imgId &&
                  data.name &&
                  data.lastName &&
                  data.birthday &&
                  data.sportTime &&
                  data.phone &&
                  data.gender &&
                  data.password &&
                  data.confirmPassword &&
                  data.password == data.confirmPassword &&
                  data.code &&
                  !isSending
                    mixpanel.api('track', 'Registration__2-step__send', evTrackProp)
                    $http.post(urlTwo, data).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                    , (res)->
                        reject(res)
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
                        base.notice.show(
                            text: 'Registration two step validation error'
                            type: 'danger'
                        )
            )

        # public methods
        # ---------------
        return {
            registerFirst: registerFirst
            registerTwo: registerTwo
        }
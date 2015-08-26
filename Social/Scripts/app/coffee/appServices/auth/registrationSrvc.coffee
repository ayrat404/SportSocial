# CoffeeScript
class registration extends Service('appSrvc')
    constructor: (
        $state,
        $location,
        $q,
        $rootScope,
        base,
        mixpanel,
        servicesDefault) ->

        urlFirst = servicesDefault.baseServiceUrl + '/register_one'
        urlTwo = servicesDefault.baseServiceUrl + '/register_two'
        isSending = false

        # (data: { name: x, sername: x, birthday: x, gender: x, sportTime: x  }, opts: {...})
        # ---------------
        registerFirst = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data &&
                  data.imgId &&
                  data.name &&
                  data.sername &&
                  data.birthday &&
                  data.sprotTime &&
                  data.phone &&
                  data.gender &&
                  !isSending
                    isSending = true
                    mixpanel.api('track', 'Registration__1-step__send', evTrackProp)
                    $http.post(urlFirst, data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.showNotice
                    , (res)->
                        reject(res)
                        if opts.showNotice
                            base.notice.show(
                                text: 'Registration first step server error'
                                type: 'danger'
                            )
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.showNotice
                        base.notice.show(
                            text: 'Registration first step validation error'
                            type: 'danger'
                        )
            )

        registerTwo = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q((resolve, reject)->
                if data &&
                  data.imgId &&
                  data.name &&
                  data.sername &&
                  data.birthday &&
                  data.sprotTime &&
                  data.phone &&
                  data.gender &&
                  data.password &&
                  data.repeatPassword &&
                  data.password == data.repeatPassword &&
                  data.code &&
                  !isSending
                    mixpanel.api('track', 'Registration__2-step__send', evTrackProp)
                    $http.post(urlTwo, data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if opts.showNotice
                    , (res)->
                        reject(res)
                        if opts.showNotice
                            base.notice.show(
                                text: 'Registration two step server error'
                                type: 'danger'
                            )
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.showNotice
                        base.notice.show(
                            text: 'Registration two step validation error'
                            type: 'danger'
                        )
            )

        # public methods
        # ---------------
        return registerFirst: registerFirst
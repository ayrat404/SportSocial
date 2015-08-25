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

        url = servicesDefault.baseServiceUrl + '/registration'
        isSending = false

        # (data: { name: x, sername: x, birthday: x, gender: x, sportTime: x  }, opts: {...})
        # ---------------
        registerFirst = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data && data.imgId && !isSending
                    isSending = true
                    mixpanel.api('track', 'Registration__1-step__send', evTrackProp)
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

        # public methods
        # ---------------
        return registerFirst: registerFirst
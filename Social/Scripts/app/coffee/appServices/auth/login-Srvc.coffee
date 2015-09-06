# CoffeeScript
class login extends Service('appSrvc')
    constructor: (
        $state
        $location
        $q
        $rootScope
        base
        mixpanel
        servicesDefault
        userService) ->

        url = servicesDefault.baseServiceUrl + '/login'
        isSending = false

        # ({ phone: x, password: x })
        # ---------------
        logIn = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title

            $q((resolve, reject)->
                if data && data.phone && data.password && !isSending
                    mixpanel.api('track', 'Login__send', evTrackProp)
                    $http.post(url, data).then((res)->
                        if res.success
                            userService.set res.data.data.id
                            $rootScope.user = res.data.data
                            resolve res.data
                        else
                            reject res
                            base.notice.response res if opts.noticeShow.errors
                    , (res)->
                        reject res
                    ).finally(->
                        isSending = false
                    )
                else
                    reject()
                    if opts.noticeShow.errors
                        base.notice.show(
                            text: 'Login validation error'
                            type: 'danger'
                        )
            )


        # methods
        # ---------------
        return logIn: logIn
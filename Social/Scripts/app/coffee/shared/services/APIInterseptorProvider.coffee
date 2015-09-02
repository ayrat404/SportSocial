class apiInterseptor extends Provider('shared')
    constructor:->
        @$get = [
            '$q'
            '$injector'
            '$timeout'
            'base'
            'servicesDefault'
            ($q
             $injector
             $timeout
             base
             servicesDefault)->
                'responseError': (res)->

                    # trick with Circular dependency found
                    # ---------------
                    $timeout ->
                        modalService = $injector.get 'modalService'
                        base = $injector.get 'base'
                        $http = $injector.get '$http'
                        $state = $injector.get '$state'

                    # show error
                    # ---------------
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Error ' + res.status + ': ' + res.statusText + '<br>' + res.data.message
                            type: 'warning'
                        )

                    # if user is authorized
                    # ---------------
                    if res.status != 401
                        return res

                    # if user is non authorized
                    # ---------------
                    deferred = $q.defer()
                    modalService.show(
                        name: 'loginSubmit'
                        data:
                            success: (res)->
                                deferred.resolve($http(res.config))
                            cancel: (res)->
                                $state.go 'registration'
                                base.notice.show(text: 'Please register if you do not have an Fortress account ', type: 'info')
                                deferred.reject(res);
                    )
                    return deferred.promise
        ]
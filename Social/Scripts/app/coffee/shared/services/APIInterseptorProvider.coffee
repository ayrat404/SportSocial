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

                modalService = {}

                # trick with Circular dependency found
                # ---------------
                $timeout ->
                    modalService = $injector.get 'modalService'
                    $http = $injector.get '$http'
                    $state = $injector.get '$state'

                'response': (res)->

                    if res.data
                        noticeClass = if res.data.success == true then 'success' else 'warning'

                        if res.data.message &&
                          res.data.message.length
                            base.notice.show
                                text: res.data.message
                                type: noticeClass
                    return res

                'responseError': (res)->

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
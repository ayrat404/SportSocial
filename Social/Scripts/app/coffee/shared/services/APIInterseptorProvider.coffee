class apiInterseptor extends Provider('shared')
    constructor:->
        @$get = [
            'base'
            'servicesDefault'
            (base
             servicesDefault)->
                'responseError': (res)->
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Error ' + res.status + ': ' + res.statusText + '<br>' + res.data.message
                            type: 'warning'
                        )
                    return res
        ]
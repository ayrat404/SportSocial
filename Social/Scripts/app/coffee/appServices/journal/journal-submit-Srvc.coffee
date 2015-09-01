class JournalSubmit extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/journal__add'
        valid =
            minText: 50

        submit = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q (resolve, reject)->
                if data && data.text && data.text.length >= valid.minText
                    $http.post(url, data).then((res)->
                        if res.success
                            resolve(res)
                        else
                            reject(res)
                        base.notice.response(res) if servicesDefault.noticeShow.errors
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Journal submit validation error'
                            type: 'danger'
                        )

        return {
        submit: submit
        }
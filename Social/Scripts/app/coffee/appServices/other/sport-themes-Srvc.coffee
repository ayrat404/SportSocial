class SportThemes extends Service('appSrvc')
    constructor: (
        $http
        $q
        servicesDefault
        base)->

        url = servicesDefault.baseServiceUrl + '/sport_themes'

        get = (search)->
            $q (resolve, reject)->
                if search && search.length
                    $http.get(url, { params: {query: search}}).then((res)->
                        if res.success
                            resolve(res.data)
                        else
                            reject(res)
                    , (res)->
                        reject(res)
                    )
                else
                    base.notice.show(text: 'Search theme string error', type: 'danger') if servicesDefault.noticeShow.errors
                    reject()

        return {
            get: get
        }

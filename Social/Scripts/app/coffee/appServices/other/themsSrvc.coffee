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
                    $http.get(url, query: search).then((res)->
                        if res.success
                            resolve(res.data)
                        else
                            reject(res)
                    , (res)->
                        base.notice.show(text: 'Search theme server error', type: 'danger') if servicesDefault.showNotice
                        reject(res)
                    )
                else
                    base.notice.show(text: 'Search theme string error', type: 'danger') if servicesDefault.showNotice
                    reject()

        return {
            get: get
        }

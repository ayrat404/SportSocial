class JournalSubmit extends Service('appSrvc')
    constructor: (
        $http
        $q
        servicesDefault
        base)->

        url = servicesDefault + '/sport_themes'

        get = (search)->
            $q (resolve, reject)->
                if search && search.length
                    $http.get(url, search: search).then((res)->
                        resolve(res)
                    , (res)->
                        reject()
                        base.notice.show(text: 'Search theme server error', type: 'danger') if servicesDefault.showNotice
                    )
                else
                    reject()
                    base.notice.show(text: 'Search theme string error', type: 'danger') if servicesDefault.showNotice

        return {
            get: get
        }

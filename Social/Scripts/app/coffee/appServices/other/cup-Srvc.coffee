class Cup extends Service('appSrvc')
    constructor: (
        $http
        $q
        store
        srvcConfig
        base)->

        url = srvcConfig.baseServiceUrl + '/cups'
        cupStorage = store.getNamespacedStore "#{srvcConfig.storeName}.cup"
        cupStorage.set 'version', srvcConfig.version

        getByExercise = (exercise)->
            $q (resolve, reject)->
                if exercise
                    # cache in localstorage
                    if cupStorage.get(exercise)
                        if cupStorage.get('version') == srvcConfig.version
                            resolve cupStorage.get(exercise)
                        else
                            cupStorage.remove exercise
                    else
                        $http.get(url, params: {exercise: exercise}).then (res)->
                            if res.data.success
                                cupStorage.set exercise, res.data.data
                                resolve res.data
                            else
                                reject res.data
                        , (res)->
                            reject res
                else
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Search theme string error',
                            type: 'danger'
                    reject()

        return getByExercise: getByExercise

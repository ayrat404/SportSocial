class Tape extends Service('appSrvc')
    constructor: (
        $q
        $http
        base
        srvcConfig)->

        url = srvcConfig.baseServiceUrl + '/tape'

        # get list
        # ---------------
        getList = (data)->
            $q (resolve, reject)->
                $http.get(url, { params: data }).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject res


        return {
            getList: getList
        }
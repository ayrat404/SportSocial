class Tape extends Service('appSrvc')
    constructor: (
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/tape'

        rqst =
            getList: new RequestConstructor.klass 'get', url

        facade =
            getList: rqst.getList.do

        return facade

#        # get list
#        # ---------------
#        getList = (data)->
#            $q (resolve, reject)->
#                $http.get(url, { params: data }).then (res)->
#                    if res.data.success
#                        resolve res.data
#                    else
#                        reject res.data
#                , (res)->
#                    reject res
#
#
#        return {
#            getList: getList
#        }
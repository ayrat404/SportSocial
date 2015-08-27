class remoteTemplateSrvc
    constructor: (
        $http
        $q)->

        baseUrl: '/Scripts/templates/common/'

        return {
            get: (template)->
                $q (resolve, reject)->
                    $http(
                        method: 'GET'
                        url: baseUrl + template + '.html'
                    ).success (res)->
                        resolve res
                    .error ->
                        reject()
        }

angular.module('shared').factory('removeTemplateSrvc', ['$http', '$q', remoteTemplateSrvc])

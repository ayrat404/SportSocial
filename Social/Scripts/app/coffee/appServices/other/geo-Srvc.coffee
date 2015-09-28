class Geo extends Service('appSrvc')
    constructor: (
        $q
        $http
        srvcConfig
        RequestConstructor)->

        urlCountry = srvcConfig.baseServiceUrl + '/geo/country'
        urlCity = srvcConfig.baseServiceUrl + '/geo/city'

        rqst =
            getCountry: new RequestConstructor.klass 'get', urlCountry
            getCity: new RequestConstructor.klass 'get', urlCity

        facade =
            getCountry: rqst.getCountry.do
            getCity: rqst.getCity.do

        return facade
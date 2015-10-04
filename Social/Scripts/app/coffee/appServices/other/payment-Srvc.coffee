class Payment extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        baseUrl = srvcConfig.baseServiceUrl + '/payment'
        payUrl = srvcConfig.baseServiceUrl + '/payment/pay'

        rqst =
            getInfo: new RequestConstructor.klass 'get', baseUrl

            init: new RequestConstructor.klass 'post', payUrl

        facade =
            getInfo: rqst.getInfo.do
            init: rqst.init.do

        return facade


class Settings extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        urlAccount = srvcConfig.baseServiceUrl + '/settings/account'
        urlProfile = srvcConfig.baseServiceUrl + '/settings/profile'
        urlPassword = srvcConfig.baseServiceUrl + '/settings/password'

        urlPhoneOne = srvcConfig.baseServiceUrl + '/settings/change_phone_one'
        urlPhoneTwo = srvcConfig.baseServiceUrl + '/settings/change_phone_two'

        rqst =

            # get account settings
            # ---------------
            getAccountSettings: new RequestConstructor.klass 'get', urlAccount

            # change password
            # ---------------
            changePassword: new RequestConstructor.klass 'post', urlPassword, (data)->
                if !data || !data.oldPassword || !data.newPassword || !data.newRepeatPassword
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Change password submit validate error'
                            type: 'danger'
                    return false
                true

            # get profile settings
            # ---------------
            getProfileSettings: new RequestConstructor.klass 'get', urlProfile

            # save profile settings
            # ---------------
            saveProfileSettings: new RequestConstructor.klass 'put', urlProfile

            # phone change get code
            # ---------------
            sendPhoneForCode: new RequestConstructor.klass 'post', urlPhoneOne

            # phone change finish
            # ---------------
            sendPhoneWithCode: new RequestConstructor.klass 'post', urlPhoneTwo


        facade =
            getAccountSettings: rqst.getAccountSettings.do
            getProfileSettings: rqst.getProfileSettings.do
            saveProfileSettings: rqst.saveProfileSettings.do
            changePassword: rqst.changePassword.do
            sendPhoneForCode: rqst.sendPhoneForCode.do
            sendPhoneWithCode: rqst.sendPhoneWithCode.do

        return facade
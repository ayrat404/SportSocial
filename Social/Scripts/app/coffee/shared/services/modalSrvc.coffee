class modal extends Service('shared')
    constructor:(
        $http,
        $modal,
        base,
        servicesDefault)->

        baseUrl = 'Scripts/templates/modals/'

        modals =
            supportSubmit:
                tplName: 'support/support-submit'
                controller: 'supportSubmitModalController'
                classname: 'fs-modal--transparent'
            supportSuccess:
                tplName: 'support/support'
                controller: 'supportSuccessModalController'
                classname: 'fs-modal--transparent'
            loginSubmit:
                tplName: 'auth/login-submit'
                controller: 'loginSubmitModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            restorePasswordSubmitPhone:
                tplName: 'auth/restore-password-submit-phone'
                controller: 'restorePasswordSubmitPhoneModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            restorePasswordSubmitNewData:
                tplName: 'auth/restore-password-submit-new-data'
                controller: 'restorePasswordSubmitNewModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            aboutAchievement:
                tplName: 'achievement/about'
                controller: 'aboutAchievementModalController'
                classname: 'fs-modal--transparent'
            apiModal:
                tplName: 'dev/api'
            policy:
                tplName: 'policy'

        # get remote modal content
        # ---------------
        getRemoteModal = (name)->
            $http(
                method: 'GET'
                url: baseUrl + name + '.html'
            )

        # show preset modals
        # ---------------
        show = (prop)->
            # todo loader
            if modals[prop.name] != undefined
                getRemoteModal(modals[prop.name].tplName).then (res)->
                    $modal.open(
                        template: res.data
                        controller: if modals[prop.name] != undefined then modals[prop.name].controller else null
                        windowClass: ['fs-modal', if modals[prop.name] != undefined then modals[prop.name].classname else null].join(' ')
                        resolve:
                            modalData: ()-> prop.data
                    )
                    return
                .finally ->
                    # todo loader
            else if servicesDefault.showNotice
                base.notice.show(
                    text: 'Modal "' + prop.name + '" not exist'
                    type: 'danger'
                )

        return {
            show: show
        }
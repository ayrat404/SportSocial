class modal extends Service('shared')
    constructor:(
        $http
        $modal
        $modalStack
        base
        globalLoaderService
        srvcConfig
        templateUrl)->

        modalBaseUrl = (path)->
            templateUrl + '/modals/' + path

        modals =
            supportSubmit:
                tplName: modalBaseUrl 'support/support-submit'
                controller: 'supportSubmitModalController'
                classname: 'fs-modal--transparent'
            supportSuccess:
                tplName: modalBaseUrl 'support/support'
                controller: 'supportSuccessModalController'
                classname: 'fs-modal--transparent'
            loginSubmit:
                tplName: modalBaseUrl 'auth/login-submit'
                controller: 'loginSubmitModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            restorePasswordSubmitPhone:
                tplName: modalBaseUrl 'auth/restore-password-submit-phone'
                controller: 'restorePasswordSubmitPhoneModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            restorePasswordSubmitNewData:
                tplName: modalBaseUrl 'auth/restore-password-submit-new-data'
                controller: 'restorePasswordSubmitNewModalController'
                classname: 'fs-modal--transparent fs-modal--xs-content'
            aboutAchievement:
                tplName: modalBaseUrl 'achievement/about'
                controller: 'aboutAchievementModalController'
                classname: 'fs-modal--transparent'
            journalSubmit:
                tplName: templateUrl + '/journal/submit'
                controller: 'journalModalSubmitController'
                classname: 'fs-modal--transparent'
            journalRemove:
                tplName: modalBaseUrl 'journal/remove'
                controller: 'journalModalRemoveController'
                classname: 'fs-modal--transparent'
            mediaShow:
                tplName: modalBaseUrl 'media/show'
                controller: 'mediaModalShowController'
                classname: 'fs-modal--transparent fs-modal--lg-content'
            complainSubmit:
                tplName: modalBaseUrl 'complain'
                controller: 'complainSubmitModalController'
                classname: 'fs-modal--transparent'
            socialShare:
                tplName: modalBaseUrl 'social-share'
                controller: 'socialShareModalController'
                classname: 'fs-modal--transparent sharing-modal'
            changePhoneGetCode:
                tplName: modalBaseUrl 'settings/change-phone-get-code'
                controller: 'changePhoneGetCodeModalController'
                classname: 'fs-modal--transparent'
            changePhoneSubmitCode:
                tplName: modalBaseUrl 'settings/change-phone-submit-code'
                controller: 'changePhoneSubmitCodeModalController'
                classname: 'fs-modal--transparent'
            apiModal:
                tplName: modalBaseUrl 'dev/api'
            policy:
                tplName: modalBaseUrl 'policy'

        # close all modals
        # ---------------
        closeAll = (reason)->
            $modalStack.dismissAll reason

        # show preset modals
        # ---------------
        show = (prop)->
            globalLoaderService.add 'load-modal'
            if modals[prop.name]
                $http.get(modals[prop.name].tplName).then (res)->
                    $modal.open(
                        template: res.data
                        controller: if modals[prop.name] != undefined then modals[prop.name].controller else null
                        windowClass: ['fs-modal', if modals[prop.name] != undefined then modals[prop.name].classname else null].join(' ')
                        resolve:
                            modalData: -> prop.data
                    )
                    return
                .finally ->
                    globalLoaderService.remove 'load-modal'
            else if srvcConfig.noticeShow.errors
                base.notice.show(
                    text: 'Modal "' + prop.name + '" not exist'
                    type: 'danger'
                )

        return {
            show: show
            closeAll: closeAll
        }
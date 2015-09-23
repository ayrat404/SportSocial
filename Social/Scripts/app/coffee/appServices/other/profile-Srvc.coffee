class Profile extends Service('appSrvc')
  constructor: (
    base
    mixpanel
    srvcConfig
    RequestConstructor)->

    url = srvcConfig.baseServiceUrl + '/profile'
    avatarUrl = srvcConfig.baseServiceUrl + '/profile/avatar'

    rqst =

        # get profile info (for profile view page)
        # ---------------
        getInfo: new RequestConstructor.klass 'get', url, (data)->
          if !data.id
            if srvcConfig.noticeShow.errors
              base.notice.show
                text: 'Get profile data: userId variable error'
                type: 'danger'
            return false
          true

        # remove avatar
        # ---------------
        removeAvatar: new RequestConstructor.klass 'delete', avatarUrl

    facade =
      getInfo: rqst.getInfo.do
      removeAvatar: rqst.removeAvatar.do

    return facade

#    # get profile info (for profile view page)
#    # ---------------
#    getInfo = (userId)->
#      $q (resolve, reject)->
#        if userId
#          $http.get(url, { params: {id: userId}}).then((res)->
#            if res.data.success
#              resolve res.data
#            else
#              reject res.data
#          , (res)->
#            reject res
#          )
#        else
#          reject()
#          if srvcConfig.noticeShow.errors
#            base.notice.show(
#              text: 'Get profile data: userId variable error'
#              type: 'danger'
#            )
#
#    # remove avatar
#    # ---------------
#    removeAvatar = ->
#      $q (resolve, reject)->
#        $http.delete(avatarUrl).then (res)->
#          if res.data.success
#            resolve res.data
#          else
#            reject res.data
#        , (res)->
#          reject res
#
#    return {
#      getInfo: getInfo
#      removeAvatar: removeAvatar
#    }

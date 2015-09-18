class Profile extends Service('appSrvc')
  constructor: (
    $q
    $rootScope
    $http
    base
    mixpanel
    servicesDefault)->

    url = servicesDefault.baseServiceUrl + '/profile'
    avatarUrl = servicesDefault.baseServiceUrl + '/profile/avatar'

    # get profile info (for profile view page)
    # ---------------
    getInfo = (userId)->
      $q (resolve, reject)->
        if userId
          $http.get(url, { params: {id: userId}}).then((res)->
            if res.data.success
              resolve res.data
            else
              reject res.data
          , (res)->
            reject res
          )
        else
          reject()
          if servicesDefault.noticeShow.errors
            base.notice.show(
              text: 'Get profile data: userId variable error'
              type: 'danger'
            )

    # remove avatar
    # ---------------
    removeAvatar = ->
      $q (resolve, reject)->
        $http.post(avatarUrl).then (res)->
          if res.data.success
            resolve res.data
          else
            reject res.data
        , (res)->
          reject res

    return {
      getInfo: getInfo
      removeAvatar: removeAvatar
    }

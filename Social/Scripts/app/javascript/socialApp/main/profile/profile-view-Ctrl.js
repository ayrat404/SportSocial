var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $state, $stateParams, $rootScope, mixpanel, profileService, modalService) {
    var _this;
    $scope.$root.title = ['Fortress | ', $rootScope.user.fullName].join('');
    _this = this;
    _this.user = {
      loaded: false
    };
    _this.test = function() {
      return modalService.show({
        name: 'mediaShow',
        data: {
          media: 666,
          index: 4
        }
      });
    };
    profileService.getInfo($stateParams.userId).then(function(res) {
      _this.user = res.data;
      if ($rootScope.user.id === +$stateParams.userId) {
        _this.user.isOwner = true;
      } else {
        _this.user.isOwner = false;
      }
      return _this.user.loaded = true;
    });
    _this.wall = [
      {
        id: 1,
        text: 'asdasdasdasdasd',
        date: '09/05/2015',
        media: [
          {
            id: 1,
            type: 'video',
            img: 'srctest1'
          }, {
            id: 2,
            type: 'image',
            img: 'srctest2'
          }, {
            id: 3,
            type: 'video',
            img: 'srctest3'
          }, {
            id: 4,
            type: 'image',
            img: 'srctest4'
          }
        ],
        likes: {
          list: [
            {
              id: 1,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 2,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 3,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 4,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }
          ],
          count: 23
        }
      }, {
        id: 2,
        text: 'asdasdasdasdasd',
        date: '09/05/2015',
        media: [
          {
            id: 1,
            type: 'image',
            img: 'srctest1'
          }, {
            id: 2,
            type: 'image',
            img: 'srctest2'
          }, {
            id: 3,
            type: 'video',
            img: 'srctest3'
          }, {
            id: 4,
            type: 'image',
            img: 'srctest4'
          }
        ],
        likes: {
          list: [
            {
              id: 1,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 2,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 3,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 4,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }
          ],
          count: 23
        }
      }, {
        id: 3,
        text: 'asdasdasdasdasd',
        date: '09/05/2015',
        media: [],
        likes: {
          list: [
            {
              id: 1,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 2,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 3,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }, {
              id: 4,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            }
          ],
          count: 23
        }
      }
    ];
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$state', '$stateParams', '$rootScope', 'mixpanel', 'profileService', 'modalService', ProfileView]);

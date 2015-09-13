var RecordView;

RecordView = (function() {
  function RecordView($scope, $stateParams, $rootScope, journalService, mixpanel) {
    var _this;
    $scope.$root.title = 'Fortress | Запись в дневнике';
    _this = this;
    _this.it = {
      loader: true
    };
    journalService.getById(+$stateParams.id).then(function(res) {
      _this.it = res.data;
      _this.it.loader = false;
      if ($rootScope.user.id === _this.it.author.id) {
        return _this.it.isOwner = true;
      } else {
        return _this.it.isOwner = false;
      }
    });
    _this.edit = function() {
      return console.log('edit');
    };
    _this.remove = function() {
      return modalService.show({
        name: 'journalRemove',
        data: {
          id: _this.it.id,
          success: function(res) {
            return $state.go('main.profile', {
              userId: $rootScope.user.id
            });
          }
        }
      });
    };
    _this.it = {
      isOwner: true,
      loader: false,
      id: 123,
      text: '123123123',
      author: {
        id: 12,
        avatar: 'avatar',
        fullName: 'Павел Козловский'
      },
      date: '19 июля 2015 | 15:08',
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
          }, {
            id: 5,
            fullName: 'Владимир Владимирович',
            avatar: 'avatartest1'
          }
        ],
        count: 23
      },
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
      tags: ['Питание', 'Программа тренировок'],
      comments: {
        list: [
          {
            id: 1,
            text: 'wwwwww wwwwww',
            author: {
              id: 5,
              fullName: 'Владимир Владимирович',
              avatar: 'avatartest1'
            },
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
                }
              ],
              count: 5,
              isLiked: false
            },
            date: "19 июня 2015 | 15:08",
            commentFor: {
              id: 2,
              name: "Вася"
            }
          }, {
            id: 2,
            text: 'wwwwww wwwwww',
            author: {
              id: 5,
              fullName: 'Вася',
              avatar: 'avatartest1'
            },
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
                }
              ],
              count: 5,
              isLiked: false
            },
            date: "19 июня 2015 | 15:08"
          }
        ],
        count: 23
      }
    };
    _this.it.comments.form = {};
  }

  return RecordView;

})();

angular.module('socialApp.controllers').controller('recordViewController', ['$scope', '$stateParams', '$rootScope', 'journalService', 'mixpanel', RecordView]);

var MediaModalShow;

MediaModalShow = (function() {
  function MediaModalShow($scope, $state, $modalInstance, journalService, modalData) {
    var setByIndex;
    $scope.maxText = 40;
    if (modalData.media !== void 0) {
      $state.params.media = modalData.media;
      $scope.currentIndex = modalData.index !== void 0 ? +modalData.index : 1;
      journalService.getById(modalData.media).then(function(res) {
        $scope.it = res.data;
        $scope.it.loader = false;
        if ($rootScope.user.id === $scope.it.author.id) {
          return $scope.it.isOwner = true;
        } else {
          return $scope.it.isOwner = false;
        }
      }, function(res) {});
      $scope.it = {
        isOwner: true,
        loader: false,
        id: 123,
        text: '1231231asdasddasdasd asd asd as dasasdsdasdasdasd asd asd  asd asdasdas dasdas das das 23',
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
            img: 'srctest1',
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
            }
          }, {
            id: 2,
            type: 'image',
            img: 'srctest2',
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
            }
          }, {
            id: 3,
            type: 'video',
            img: 'srctest3',
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
            }
          }, {
            id: 4,
            type: 'image',
            img: 'srctest4',
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
            }
          }
        ],
        tags: ['Питание', 'Программа тренировок']
      };
      $scope.itemsCount = $scope.it.media.length;
      setByIndex = function(index) {
        var i;
        i = index - 1;
        if ($scope.it.media[i] !== void 0) {
          $scope.current = $scope.it.media[i];
        }
        $state.params.index = index;
        return $state.transitionTo($state.current, $state.params, {
          notify: false
        });
      };
      setByIndex($scope.currentIndex);
      $scope.before = function() {
        if ($scope.currentIndex === 1) {
          $scope.currentIndex = $scope.itemsCount;
        } else {
          --$scope.currentIndex;
        }
        return setByIndex($scope.currentIndex);
      };
      $scope.next = function() {
        if ($scope.currentIndex === $scope.itemsCount) {
          $scope.currentIndex = 1;
        } else {
          ++$scope.currentIndex;
        }
        return setByIndex($scope.currentIndex);
      };
    } else {
      console.log('media id undefined');
    }
    $modalInstance.result["catch"](function() {
      $state.params.index = null;
      $state.params.media = null;
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    });
  }

  return MediaModalShow;

})();

angular.module('socialApp.controllers').controller('mediaModalShowController', ['$scope', '$state', '$modalInstance', 'journalService', 'modalData', MediaModalShow]);

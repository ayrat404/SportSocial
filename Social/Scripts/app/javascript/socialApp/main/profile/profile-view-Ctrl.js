var ProfileView;

ProfileView = (function() {
  function ProfileView($scope, $stateParams, mixpanel, profileService) {
    var _this;
    _this = this;
    _this.user = {
      loaded: false
    };
    profileService.getInfo($stateParams.userId).then(function(res) {
      _this.user = res.data;
      return _this.user.loaded = true;
    });
    _this.user = {
      avatar: '123123',
      fullName: 'Kaka Lakovich',
      age: 25,
      sportTime: 21,
      location: 'Заинск, Россия',
      isOwner: true,
      loaded: true,
      followers: {
        list: [
          {
            id: 1,
            fullName: 'Александр Русаков',
            avatar: '#'
          }, {
            id: 2,
            fullName: 'Александр Русаков',
            avatar: '#'
          }, {
            id: 3,
            fullName: 'Александр Русаков',
            avatar: '#'
          }
        ],
        count: 3
      },
      subscribe: {
        list: [
          {
            id: 5,
            fullName: 'Александр Русаков',
            avatar: '#'
          }, {
            id: 6,
            fullName: 'Александр Русаков',
            avatar: '#'
          }, {
            id: 7,
            fullName: 'Александр Русаков',
            avatar: '#'
          }, {
            id: 8,
            fullName: 'Александр Русаков',
            avatar: '#'
          }
        ],
        count: 120
      }
    };
  }

  return ProfileView;

})();

angular.module('socialApp.controllers').controller('profileViewController', ['$scope', '$stateParams', 'mixpanel', 'profileService', ProfileView]);

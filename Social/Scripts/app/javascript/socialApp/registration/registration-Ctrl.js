(function(){
var Registration;

Registration = (function() {
  function Registration($scope, $rootScope, $state, mixpanel, registrationService, modalService) {
    $scope.$root.title = 'Fortress | Регистрация';
    $scope.loading = false;
    $scope.first = {};
    $scope.firstStepValidation = {};
    $scope.twoStepValidation = {};
    $scope.two = {};
    $scope.step = 1;
    $scope.datepickerOptions = {
      maxDate: Date.now(),
      initDate: new Date(1992, 0, 1)
    };
    $scope.sendImg = function($flow) {
      if ($scope.first.imgId !== null && $scope.first.imgId !== void 0) {
        return $scope.sendFirstStepData('', true);
      } else {
        return $flow.upload();
      }
    };
    $scope.removeImg = function($flow) {
      $flow.cancel();
      return $scope.first.imgId = null;
    };
    $scope.sendFirstStepData = function(stringResponse, isRepeat) {
      var obj;
      if (!isRepeat) {
        obj = angular.fromJson(stringResponse);
        if (obj.success) {
          $scope.first.imgId = obj.data.id;
        }
      }
      return registrationService.registerFirst($scope.first).then(function(res) {
        return $scope.step = 2;
      }, function(res) {
        return $scope.firstStepValidation = res.errors;
      });
    };
    $scope.sendCodeAgain = function() {
      $scope.sendAgainLoading = true;
      return registrationService.registerFirst($scope.first)["finally"](function(res) {
        return $scope.sendAgainLoading = false;
      });
    };
    $scope.sendTwo = function() {
      var fullData;
      fullData = angular.extend($scope.first, $scope.two);
      return registrationService.registerTwo(fullData).then(function(res) {
        return modalService.show({
          name: 'loginSubmit'
        });
      }, function(res) {
        return $scope.twoStepValidation = res.errors;
      });
    };
    $scope.flowObj = {};
    $scope.imgProp = {
      vertical: false,
      sizeError: false,
      style: {}
    };
    $scope.processImage = function(files) {
      $scope.imgProp.vertical = false;
      $scope.imgProp.sizeError = false;
      return angular.forEach(files, function(flow) {
        var fileReader, image;
        fileReader = new FileReader();
        image = new Image();
        fileReader.onload = function(event) {
          var uri;
          uri = event.target.result;
          image.src = uri;
          return image.onload = function() {
            return $scope.$apply(function() {
              if (image.width < 200 || image.height < 20) {
                $scope.flowObj.flow.cancel();
                return $scope.imgProp.sizeError = true;
              } else {
                if (image.height > image.width) {
                  $scope.imgProp.vertical = true;
                  $scope.imgProp.style.width = 186;
                  $scope.imgProp.style.height = Math.round(image.height / (image.width / 186));
                  return $scope.imgProp.style.margin = (98 - $scope.imgProp.style.height / 2) + 'px 0 0 0';
                } else {
                  $scope.imgProp.style.height = 186;
                  $scope.imgProp.style.width = Math.round(image.width / (image.height / 186));
                  return $scope.imgProp.style.margin = '0 0 0 ' + (98 - $scope.imgProp.style.width / 2) + 'px';
                }
              }
            });
          };
        };
        return fileReader.readAsDataURL(flow.file);
      });
    };
    $scope.$on('$viewContentLoaded', function() {
      return mixpanel.ev.visitPage($scope.$root.title);
    });
  }

  return Registration;

})();

angular.module('socialApp.controllers').controller('registrationController', ['$scope', '$rootScope', '$state', 'mixpanel', 'registrationService', 'modalService', Registration]);

})();
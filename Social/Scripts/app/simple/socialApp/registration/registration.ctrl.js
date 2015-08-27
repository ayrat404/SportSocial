'use strict';

angular.module('socialApp.controllers')
    .controller('RegistrationCtrl', [
        '$scope',
        'mixpanel',
        'registrationSrvc',
        'base',
        function (
            $scope,
            mixpanel,
            registrationSrvc,
            base) {

            // $scope data
            // ---------------
            $scope.$root.title = 'Fortress | Регистрация';
            $scope.loading = false;
            $scope.first = {};              // first step data model
            $scope.firstStepValidation = {};    // first step validation server object
            $scope.twoStepValidation = {};      // two step validation server object
            $scope.two = {};                // two step data model
            $scope.step = 1;                // active step

            // datepicker opts
            $scope.datepickerOptions = {
                maxDate: Date.now(),
                initDate: new Date(1992, 0, 1)
            }

            // send first data
            // ---------------
            $scope.sendImg = function ($flow) {
                $flow.upload();

                // common error with error fields example
                //$scope.firstStepValidation = { common: { error: 'Common error', fields: ['Name', 'Sername'] } };

                // fields errors example
                //$scope.firstStepValidation = { common: { error: 'Common error' }, fields: [{ name: 'Name', error: 'Name error' }] };
            }
            // send all data after image server upload
            // ---------------
            $scope.sendFirstStepData = function (imgResponse) {
                if (imgResponse.success) {
                    $scope.first.imgId = imgResponse.data.id;
                    registrationSrvc.registerFirst($scope.first)
                    .then(function (res) {
                        $scope.step = 2;
                    }, function (res) {
                        $scope.firstValidation = res.errors;
                    });
                }
            }

            // send two data
            // ---------------
            $scope.sendTwo = function () {
                var fullData = angular.extend($scope.first, $scope.two);
                registrationSrvc.registerTwo(fullData)
                    .then(function (res) {
                        // todo success redirect to profile
                    }, function (res) {
                        $scope.twoStepValidation = res.errors;
                });
            }

            // proccess image
            // ---------------
            $scope.flowObj = {};
            $scope.imgProp = {
                vertical: false,
                sizeError: false,
                style: {}
            }
            $scope.processImage = function (files) {
                $scope.imgProp.vertical = false;
                $scope.imgProp.sizeError = false;
                angular.forEach(files, function (flow) {
                    var fileReader = new FileReader(),
                        image = new Image();
                    fileReader.onload = function (event) {
                        var uri = event.target.result;
                        image.src = uri;
                        image.onload = function () {
                            $scope.$apply(function () {
                                if (image.width < 200 || image.height < 200) {
                                    $scope.flowObj.flow.cancel();
                                    $scope.imgProp.sizeError = true;
                                } else {
                                    if (image.height > image.width) {
                                        $scope.imgProp.vertical = true;
                                        $scope.imgProp.style.width = 186;
                                        $scope.imgProp.style.height = Math.round(image.height / (image.width / 186));
                                        $scope.imgProp.style.margin = (98 - $scope.imgProp.style.height / 2) + 'px 0 0 0';
                                    } else {
                                        $scope.imgProp.style.height = 186;
                                        $scope.imgProp.style.width = Math.round(image.width / (image.height / 186));
                                        $scope.imgProp.style.margin = '0 0 0 ' + (98 - $scope.imgProp.style.width / 2) + 'px';
                                    }
                                }
                            });
                        };
                    };
                    fileReader.readAsDataURL(flow.file);
                });
            }

            // mixpanel tracking
            // ----------------
            $scope.$on('$viewContentLoaded', function () {
                mixpanel.ev.visitPage($scope.$root.title);
            });
        }
    ]);
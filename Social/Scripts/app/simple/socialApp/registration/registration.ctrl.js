﻿'use strict';

angular.module('socialApp.controllers')
    .controller('RegistrationCtrl', [
        '$scope',
        'mixpanel',
        'registrationSrvc',
        function (
            $scope,
            mixpanel,
            registrationSrvc) {

            $scope.$root.title = 'Fortress | Регистрация';
            $scope.loading = false;

            $scope.step = 1;

            $scope.datepickerOptions = {
                maxDate: Date.now(),
                initDate: new Date(1992, 0, 1)
            }

            // send first data
            // ---------------
            $scope.sendImg = function ($flow) {
                $flow.upload();
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

                    });
                }
            }

            // send two data
            // ---------------
            $scope.sendTwo = function () {
                registrationSrvc.registerTwo($scope.two)
                    .then(function (res) {

                    }, function (res) {

                    });
            }

            // proccess image
            // ---------------
            $scope.flowObj = {};
            $scope.imgProp = {
                vertical: false
            }
            $scope.processImage = function (files) {
                $scope.imgProp.vertical = false;
                angular.forEach(files, function (flow) {
                    var fileReader = new FileReader(),
                        image = new Image();
                    fileReader.onload = function (event) {
                        var uri = event.target.result;
                        image.src = uri;
                        image.onload = function () {
                            if (this.height > this.width)
                                $scope.imgProp.vertical = true;
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
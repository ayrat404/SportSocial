class Tape extends Controller('socialApp.controllers')
    constructor: (
        $q
        $scope
        $state
        tapeService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Моя лента'

        _this = this
        _this.loader = false # todo true
        _this.pageError = false
        _this.showMoreLoading = false

        _this.filter =
            count: 20           # default count load
            page: 3             # default page

        for k,v of $state.params
            if v != undefined
                _this.filter[k] = v

        # set params in url
        # ---------------
        setUrl = ->
            $state.params = _this.filter
            $state.transitionTo($state.current, $state.params, { notify: false });

        # get list
        # ---------------
        getList = (filter)->
            setUrl()
            $q (resolve, reject)->
                tapeService.getList(filter).then (res)->
                    _this.showMore = res.data.isMore
                    resolve res.data.list
                , (res)->
                    _this.list = []
                    _this.showMore = false
                    reject res

        # first load
        # ---------------
        do->
            filter = angular.extend({}, _this.filter)
            filter.page = 1
            filter.count = _this.filter.page * _this.filter.count
            getList(filter).then (list)->
                _this.list = list
            , (res)->
                _this.pageError = true
            .finally ->
                _this.loader = false

        # show more
        # ---------------
        _this.loadMore = ->
            if !_this.showMoreLoading
                _this.showMoreLoading = true
                _this.filter.page = +_this.filter.page + 1
                getList(_this.filter).then (list)->
                    _this.list.push list
                .finally ->
                    _this.showMoreLoading = false

        # social share record
        # ---------------
        _this.share = (obj)->
            url = ''
            if obj.type == 'record'
                url = 'main.journalIt'
            else if obj.type == 'achievement'
                url = 'main.achievementView'
            modalService.show
                name: 'socialShare'
                data:
                    url: $state.href(url, {id: obj.id}, {absolute: true})
                    text: obj.text
                    media: obj.media
                    hashtags: obj.tags


#        _this.list = [
#            # records
#            {
#                type: 'record'
#                "id": 10,
#                "text": "нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас нормас",
#                "author": {
#                    "id": 1,
#                    "avatar": "/Content/Images/default-avatar.png",
#                    "fullName": "Алексей Рябов"
#                },
#                "likes": {
#                    "count": 0,
#                    "isLiked": false,
#                    "list": []
#                },
#                "media": [
#                    {
#                        "id": 48,
#                        "type": "video",
#                        "url": "http://www.youtube.com/watch?v=OHa4rXW7x-g",
#                        "embeddedUrl": "http://www.youtube.com/embed/OHa4rXW7x-g",
#                        "remoteId": "OHa4rXW7x-g",
#                        "remoteUrl": "http://www.youtube.com/watch?v=OHa4rXW7x-g",
#                        "likes": {
#                            "count": 1,
#                            "isLiked": true,
#                            "list": [
#                                {
#                                    "id": 1,
#                                    "avatar": "/Content/Images/default-avatar.png",
#                                    "fullName": "Алексей Рябов"
#                                }
#                            ]
#                        }
#                    }
#                ],
#                "created": "2015-09-21T10:52:13.427"
#            },
#            {
#                type: 'record'
#                "id": 9,
#                "text": "Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест Новый тест",
#                "author": {
#                    "id": 1,
#                    "avatar": "/Content/Images/default-avatar.png",
#                    "fullName": "Алексей Рябов"
#                },
#                "likes": {
#                    "count": 1,
#                    "isLiked": true,
#                    "list": [
#                        {
#                            "id": 1,
#                            "avatar": "/Content/Images/default-avatar.png",
#                            "fullName": "Алексей Рябов"
#                        }
#                    ]
#                },
#                "media": [
#                    {
#                        "id": 10,
#                        "type": "image",
#                        "url": "/Storage/Images/Journal/04c4dcae-df82-4362-8b1f-a635877b9600.png",
#                        "embeddedUrl": null,
#                        "remoteId": null,
#                        "remoteUrl": null,
#                        "likes": {
#                            "count": 1,
#                            "isLiked": true,
#                            "list": [
#                                {
#                                    "id": 1,
#                                    "avatar": "/Content/Images/default-avatar.png",
#                                    "fullName": "Алексей Рябов"
#                                }
#                            ]
#                        }
#                    },
#                    {
#                        "id": 11,
#                        "type": "video",
#                        "url": "http://www.youtube.com/watch?v=i7joSSY6ZpQ",
#                        "embeddedUrl": "http://www.youtube.com/embed/i7joSSY6ZpQ",
#                        "remoteId": "i7joSSY6ZpQ",
#                        "remoteUrl": "http://www.youtube.com/watch?v=i7joSSY6ZpQ",
#                        "likes": {
#                            "count": 1,
#                            "isLiked": true,
#                            "list": [
#                                {
#                                    "id": 1,
#                                    "avatar": "/Content/Images/default-avatar.png",
#                                    "fullName": "Алексей Рябов"
#                                }
#                            ]
#                        }
#                    }
#                ],
#                "created": "2015-09-13T15:08:49.313"
#            },
#            {
#                type: 'record'
#                "id": 8,
#                "text": "asdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asdasdasdadasddasdadadddasddasdad sd asd asd asad asd",
#                "author": {
#                    "id": 1,
#                    "avatar": "/Content/Images/default-avatar.png",
#                    "fullName": "Алексей Рябов"
#                },
#                "likes": {
#                    "count": 1,
#                    "isLiked": true,
#                    "list": [
#                        {
#                            "id": 1,
#                            "avatar": "/Content/Images/default-avatar.png",
#                            "fullName": "Алексей Рябов"
#                        }
#                    ]
#                },
#                "media": [
#                    {
#                        "id": 7,
#                        "type": "image",
#                        "url": "/Storage/Images/Journal/122ca9b6-8b81-4ec3-a0bc-0784bec98f69.jpg",
#                        "embeddedUrl": null,
#                        "remoteId": null,
#                        "remoteUrl": null,
#                        "likes": {
#                            "count": 0,
#                            "isLiked": false,
#                            "list": []
#                        }
#                    },
#                    {
#                        "id": 8,
#                        "type": "image",
#                        "url": "/Storage/Images/Journal/284c7844-004e-4d38-b7d7-f45f21342cbf.jpg",
#                        "embeddedUrl": null,
#                        "remoteId": null,
#                        "remoteUrl": null,
#                        "likes": {
#                            "count": 0,
#                            "isLiked": false,
#                            "list": []
#                        }
#                    },
#                    {
#                        "id": 9,
#                        "type": "image",
#                        "url": "/Storage/Images/Journal/dfed5940-10b3-42af-bf44-edf9ae0c065b.jpg",
#                        "embeddedUrl": null,
#                        "remoteId": null,
#                        "remoteUrl": null,
#                        "likes": {
#                            "count": 0,
#                            "isLiked": false,
#                            "list": []
#                        }
#                    }
#                ],
#                "created": "2015-09-09T12:51:52.41"
#            }
#            # achievements
#            {
#                type: 'achievement'
#                "id": 10,
#                "title": "Подтягивания - 30 повторений",
#                "author": {
#                    "id": 1,
#                    "avatar": "/Content/Images/default-avatar.png",
#                    "fullName": "Алексей Рябов"
#                },
#                "likes": {
#                    "count": 1,
#                    "isLiked": true,
#                    "list": [
#                        {
#                            "id": 1,
#                            "avatar": "/Content/Images/default-avatar.png",
#                            "fullName": "Алексей Рябов"
#                        }
#                    ]
#                },
#                cupImage: 'cupImageUrl'
#                status: 'credit'
#                "created": "2015-09-09T12:51:52.41"
#            }
#            {
#                type: 'achievement'
#                "id": 11,
#                "title": "Подтягивания - 30 повторений",
#                "author": {
#                    "id": 1,
#                    "avatar": "/Content/Images/default-avatar.png",
#                    "fullName": "Алексей Рябов"
#                },
#                "likes": {
#                    "count": 1,
#                    "isLiked": true,
#                    "list": [
#                        {
#                            "id": 1,
#                            "avatar": "/Content/Images/default-avatar.png",
#                            "fullName": "Алексей Рябов"
#                        }
#                    ]
#                },
#                cupImage: 'cupImageUrl'
#                status: null
#                "created": "2015-09-09T12:51:52.41"
#            }
#        ]

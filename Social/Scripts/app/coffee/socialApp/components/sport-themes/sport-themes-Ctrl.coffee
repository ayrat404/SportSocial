class ThemesAutocomplete extends Controller('socialApp.controllers')
    constructor: (
        $scope
        sportThemesService
        base)->

        if !base.isArray($scope.themes)
            $scope.themes = []

        # get remote tags
        # ---------------
        $scope.getThemes = (search)->
            return sportThemesService.get(query: search).then (res)->
                # if find themes
                if res.data.length
                    return res.data
                else
                    return [search]

        # select tag
        # ---------------
        $scope.format = ($item, $model, $label)->
            if $scope.themes.indexOf($item) != -1
                base.notice.show(
                    text: 'Theme "' + $item + '" is already selected'
                    type: 'warning'
                )
            else
                $scope.search = ''
                $scope.themes.push($item)
            return

        # remove tag
        # ---------------
        $scope.removeTag = (tag)->
            index = $scope.themes.indexOf(tag)
            if index != -1
                $scope.themes.splice(index, 1)
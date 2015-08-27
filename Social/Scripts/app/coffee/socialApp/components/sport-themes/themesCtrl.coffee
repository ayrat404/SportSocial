class JournalSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        journalSubmitService)->

        # get remote themes
        # ---------------
        $scope.getThemes = (search)->
            return journalSubmitService.get(search).then((res)->
                if res.length
                    return res
                else
                    return search
            )

        # select theme
        # ---------------
        $scope.format = ($item, $model, $label)->
            debugger;

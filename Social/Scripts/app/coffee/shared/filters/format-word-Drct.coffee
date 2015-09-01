class formatWord extends Filter('shared')
    constructor: (base)->
        return (count, words, withNumber)->
            if withNumber == true
                return count + ' ' + base.format.word count, words
            base.format.word count, words
class imageResize extends Filter('shared')
    constructor: (base)->
        return (imageSrc, params)->
            params.mode = 'crop'
            return base.image.resize(imageSrc, params)
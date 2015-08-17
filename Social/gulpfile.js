/* global require, console */
'use strict';

var gulp = require('gulp'),
    less = require('gulp-less'),
    rename = require('gulp-rename'),
    filesize = require('gulp-filesize'),
    autoprefixer = require('gulp-autoprefixer'),
    spritesmith = require('gulp.spritesmith'),
    header = require('gulp-header'),
    minifyCSS = require('gulp-minify-css'),
    coffee = require('gulp-coffee'),
    ngmin = require('gulp-ngmin'),
    ngClassify = require('gulp-ng-classify'),
    wrap = require('gulp-wrap'),
    pkg = require('./package.json');

var banner = '/*!\n' +
            ' * Fortress v<%= pkg.version %> (<%= pkg.homepage %>) \n' +
            ' * Copyright 2015 <%= pkg.author %>\n\n';

var apps = [
    {
        "name": "socialApp",
        "style": "fortress.social.min.css",
        "compileStylesPath": "Content/socialApp/styles/compile/",
        "baseStylesPath": "Content/socialApp/styles/less/",
        "spriteIn": "Content/socialApp/images/sprites/*.png",
        "spriteOut": "Content/socialApp/images/icons-set/",
        "baseCoffeePath": "Scripts/app/coffee/",
        "coffeeIn": "Scripts/app/coffee/**/*.coffee",
        "coffeeOut": "Scripts/app/javascript/"
    }
];

// ---------------
function compileCss(project, compress) {
    return gulp.src(project.baseStylesPath + 'style.less')
            .pipe(less({ cleancss: true, compress: compress }))
            .on('error', console.log)
            .pipe(autoprefixer(['last 2 versions', 'ie 10', 'opera 12']))
            .on('error', console.log)
            .pipe(header(banner, { pkg: pkg }))
            .pipe(rename(project.style))
            //.pipe(minifyCSS())
            .pipe(filesize())
            .pipe(gulp.dest(project.compileStylesPath));
}

// ---------------
function compileCoffee(project) {
    return gulp.src(project.coffeeIn)
        .pipe(ngClassify())
        .pipe(coffee({ bare: true }).on('error', console.log))
        .pipe(ngmin())
        .pipe(wrap('(function(){\n<%= contents %>\n}).call(this);'))
        .pipe(gulp.dest(project.coffeeOut));
}


// ---------------
function declareTasks(project) {

    // compile less
    // ---------------
    gulp.task(project.name + '-css', function () {
        return compileCss(project, true);
    });

    // compile coffee
    // ---------------
    gulp.task(project.name + '-coffee', function () {
        return compileCoffee(project);
    });

    // generate sprite
    // ---------------
    gulp.task(project.name + '-sprite', function () {
        var spriteData = gulp.src(project.spriteIn)
            .pipe(spritesmith({
                imgName: '../../images/icons-set/icons-set.png',
                cssName: 'icons-set.less',
                cssFormat: 'less',
                padding: 10
            }));
        spriteData.css.pipe(gulp.dest(project.baseStylesPath + 'base'));
        return spriteData.img.pipe(gulp.dest(project.spriteOut));
    });

    // watcher
    // ---------------
    gulp.task(project.name + '-watch', function () {
        var cssWatcher = gulp.watch([
            project.baseStylesPath + '*.less',
            project.baseStylesPath + '**/*.less'
        ], function () {
            return compileCss(project, false);
        });
        cssWatcher.on('change', function (event) {
            console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        });

        //var coffeeWatcher = gulp.watch([
        //    project.baseCoffeePath + '*.coffee',
        //    project.baseCoffeePath + '**/*.coffee'
        //], function() {
        //    return compileCoffee(project);
        //});
        //coffeeWatcher.on('change', function (event) {
        //    console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        //});
    });
}

// ---------------
for (var i = 0; i < apps.length; i++) {
    declareTasks(apps[i]);
}

// ---------------
gulp.task('default', function () {
    console.log('-----------------------------------------------------');
    for (var i = 0; i < apps.length; i++) {
        var name = apps[i].name;
        console.log('-----------------------------------------------------');
        console.log(name + '-sprite | ' + name + '-css | ' + name + '-watch | ' + name + '-coffee');
        console.log('-----------------------------------------------------');
    }
    console.log('-----------------------------------------------------');
});

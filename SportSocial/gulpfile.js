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
    pkg = require('./package.json');

var banner = '/*!\n' +
            ' * Fortress v<%= pkg.version %> (<%= pkg.homepage %>) \n' +
            ' * Copyright 2015 <%= pkg.author %>\n\n';

function lessCompile(minify) {
	return gulp.src(pkg.less)
	       .pipe(less({
	       	cleancss: minify
	       }))
	       .on('error', console.log)
	       .pipe(autoprefixer(['last 2 versions', 'ie 10', 'opera 12']))
	       .on('error', console.log)
	       .pipe(header(banner, { pkg: pkg }))
	       .pipe(rename(pkg.style))
	       .pipe(filesize())
           .pipe(minifyCSS())
	       .pipe(gulp.dest(pkg.output))

           .pipe(gulp.dest('../Social/Content/styles/compile/'));
}

gulp.task('sprite', function() {
	var spriteData = gulp.src('Content/images/icons/sprites/*.png')
	                 .pipe(spritesmith({
	                 		imgName: '../../images/icons/sprite.png',
	                 		cssName: 'icons-set.less',
	                 		algorithm: 'binary-tree',
	                 		cssFormat: 'less',
	                 		padding: 10
	                 }));
	spriteData.img.pipe(gulp.dest('Content/images/icons/'));
	spriteData.css.pipe(gulp.dest('Content/styles/base/'));

	spriteData.img.pipe(gulp.dest('../Social/Content/images/icons/'));
	spriteData.css.pipe(gulp.dest('../Social/Content/styles/base/'));
});

gulp.task('sprite-admin', function () {
    var spriteData = gulp.src('Content/images/icons/sprites-admin/*.png')
	                 .pipe(spritesmith({
	                     imgName: '../../images/icons/sprite-admin.png',
	                     cssName: 'icons-set--admin.less',
	                     algorithm: 'binary-tree',
	                     cssFormat: 'less',
	                     padding: 10
	                 }));
    spriteData.img.pipe(gulp.dest('Content/images/icons/'));
    spriteData.css.pipe(gulp.dest('Content/styles/base/'));
});

gulp.task('watch', function() {
	var watcher = gulp.watch([
		'Content/styles/*.less',
		'Content/styles/**/*.less'
	], function() {
		return lessCompile(false);
	});

	watcher.on('change', function(event) {
		console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
	});
});

gulp.task('default', function() {
	return lessCompile(true);
});

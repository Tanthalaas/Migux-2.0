const replace = require('gulp-replace');
const { src, dest } = require('gulp');
const { body, miguxContainer, socketIoStart, miguxContainerEnd, unityFooter, hiddenUnityFooter } = require('./unityReplace');

function configurar() {
    const socketIoCdn = '<script src="https://cdn.socket.io/3.1.3/socket.io.min.js" integrity="sha384-cPwlPLvBTa3sKAgddT6krw0cJat7egBga3DJepJyrLl4Q9/5WLra3rrnMcyTyOnh" crossorigin="anonymous"></script>';
    const indexjs = '<script src="./index.js"></script>';

    const loadingDisplay = 'loadingBar.style.display = "none";';

    const title = "<title>Unity WebGL Player | Migux</title>";
    const replaceTitle = "<title>Migux</title>";

    const css = '<link rel="stylesheet" href="TemplateData/style.css">';
    const cssReplace = '<link rel="stylesheet" href="TemplateData/style.css"><link rel="stylesheet" href="assets/style.css">';

    return src(['index.html'])
        .pipe(replace('<script>', `${socketIoCdn}\n${indexjs}\n<script>`))
        .pipe(replace(loadingDisplay, `unity = unityInstance;\n${loadingDisplay}`))
        .pipe(replace(title, replaceTitle))
        .pipe(replace(css, cssReplace))
        .pipe(replace(body, miguxContainer))
        .pipe(replace(unityFooter, hiddenUnityFooter))
        .pipe(replace(socketIoStart, miguxContainerEnd))
        .pipe(dest('./'));
}

exports.default = configurar;
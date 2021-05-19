const replace = require('gulp-replace');
const { src, dest } = require('gulp');

function configurar() {
    const socketIoCdn = '<script src="https://cdn.socket.io/3.1.3/socket.io.min.js" integrity="sha384-cPwlPLvBTa3sKAgddT6krw0cJat7egBga3DJepJyrLl4Q9/5WLra3rrnMcyTyOnh" crossorigin="anonymous"></script>';
    const indexjs = '<script src="./index.js"></script>';

    const loadingDisplay = 'loadingBar.style.display = "none";';

    return src(['index.html'])
        .pipe(replace('<script>', `${socketIoCdn}\n${indexjs}\n<script>`))
        .pipe(replace(loadingDisplay, `unity = unityInstance;\n${loadingDisplay}`))
        .pipe(dest('./'));
}

exports.default = configurar;
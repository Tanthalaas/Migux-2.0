mergeInto(LibraryManager.library, {
    ConectarSocketIO: function() {
        socket.connect();
    },
    EnviarRegistroSocketIO: function(json) {
        socket.emit('registrar', Pointer_stringify(json));
    },
    EnviarMovimentacaoSocketIO: function(json) {
        socket.emit('movimentacao', Pointer_stringify(json));
    }
});
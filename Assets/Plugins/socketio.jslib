mergeInto(LibraryManager.library, {
    ConectarSocketIO: function() {
        socket.connect();
    },
    EnviarRegistroSocketIO: function(json) {
        socket.emit('registrar', Pointer_stringify(json));
    },
    EnviarMovimentacaoSocketIO: function(json) {
        socket.emit('movimentacao', Pointer_stringify(json));
    },
    EnviarTrocaDeSalaSocketIO: function(sala) {
        socket.emit('trocar sala', Pointer_stringify(sala));
    },
    EnviarChatSocketIO: function(json) {
        socket.emit('chat', Pointer_stringify(json));
    },
    EnviarChapeuSocketIO: function(id, devolveu) {
        socket.emit('chapeu', id, devolveu);
    },
});
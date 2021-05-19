mergeInto(LibraryManager.library, {
    IniciarSocketIO: function(ip) {
        window.socket = io(Pointer_stringify(ip));
        window.socket.on('connect', () => {
            window.unityInstance.SendMessage('Conexao', 'AoConectar');
        });
        window.socket.on('jogador entrou na sala', (response) => {
            window.unityInstance.SendMessage('Conexao', 'JogadorEntrouNaSala', JSON.stringify(response));
        });
        window.socket.on('jogadores na sala', (response) => {
            window.unityInstance.SendMessage('Conexao', 'AoTrocarDeSala', JSON.stringify(response));
        });
        window.socket.on('movimentacao', (response) => {
            window.unityInstance.SendMessage('Conexao', 'ReceberMovimentacao', JSON.stringify(response));
        });
        window.socket.on('jogador saiu da sala', (response) => {
            window.unityInstance.SendMessage('Conexao', 'JogadorSaiuDaSala', JSON.stringify(response));
        });
    },

    HelloString: function(str) {
        window.io(Pointer_stringify(str));
    },
});
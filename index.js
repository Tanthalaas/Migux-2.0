var socket = io('http://127.0.0.1:3333/', { autoConnect: false });
socket.on('connect', () => {
    unity.SendMessage('Conexao', 'AoConectar');
});
socket.on('jogador entrou na sala', (response) => {
    console.log(response);
    window.unity.SendMessage('Conexao', 'JogadorEntrouNaSala', response);
});
socket.on('jogadores na sala', (response) => {
    console.log(response);
    window.unity.SendMessage('Conexao', 'AoTrocarDeSala', response);
});
socket.on('movimentacao', (response) => {
    window.unity.SendMessage('Conexao', 'ReceberMovimentacao', response);
});
socket.on('jogador saiu da sala', (response) => {
    window.unity.SendMessage('Conexao', 'JogadorSaiuDaSala', response);
});
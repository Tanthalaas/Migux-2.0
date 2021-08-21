var socket = io('https://miguxservidor.herokuapp.com/', { autoConnect: false });
socket.on('connect', () => {
    unity.SendMessage('Conexao', 'AoConectar');
});
socket.on('jogador entrou na sala', (response) => {
    console.log(response);
    unity.SendMessage('Conexao', 'JogadorEntrouNaSala', response);
});
socket.on('jogadores na sala', (response) => {
    console.log(response);
    unity.SendMessage('Conexao', 'AoTrocarDeSala', response);
});
socket.on('movimentacao', (response) => {
    unity.SendMessage('Conexao', 'ReceberMovimentacao', response);
});
socket.on('chat', (response) => {
    unity.SendMessage('Conexao', 'ReceberChat', response);
});
socket.on('chapeu', (response) => {
    unity.SendMessage('Conexao', 'ReceberChapeu', response);
});
socket.on('jogador saiu da sala', (response) => {
    unity.SendMessage('Conexao', 'JogadorSaiuDaSala', response);
});
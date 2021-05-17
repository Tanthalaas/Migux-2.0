using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;

public class Conexao : MonoBehaviour
{
    [SerializeField] Personagem jogadorLocal;
    SocketIO client;
    // Start is called before the first frame update
    void Start()
    {
        client = new SocketIO("http://127.0.0.1:3333/", new SocketIOOptions
        {
            EIO = 4
        });

        client.OnConnected += (s, e) => 
        {
            Jogador jogador = new Jogador();
            jogador.corPrimaria = Color.green;
            jogador.corSecundaria = Color.red;
            jogador.especie = "Peixe";
            jogador.sexo = "Masculino";

            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                jogadorLocal.SelecionarCriatura(jogador.especie);
                jogadorLocal.SelecionarSexo(jogador.sexo);
                jogadorLocal.SelecionarCores(jogador.corPrimaria, jogador.corSecundaria);
            }); 

            
            EnviarRegistro(jogador);
        };

        client.On("jogador entrou na sala", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadoresManager.Instance.RegistrarJogador(json);
            }); 
        });

        client.On("jogadores na sala", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadoresManager.Instance.RegistrarJogadores(json);
            }); 
        });

        client.On("movimentacao", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadoresManager.Instance.JogadorMovimentou(json);
            }); 
        });

        client.On("jogador saiu da sala", response => 
        {
            string json = response.GetValue<string>();
            Id idJson = JsonUtility.FromJson<Id>(json);
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadoresManager.Instance.ApagarJogador(idJson.id);
            });
        });

        client.ConnectAsync();
    }

    public void EnviarRegistro(Jogador jogador)
    {
        string json = JsonUtility.ToJson(jogador);
        client.EmitAsync("registrar", json);
    }
}

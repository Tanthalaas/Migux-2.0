using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#else
using SocketIOClient;
#endif

public class Conexao : MonoBehaviour
{
    [SerializeField] Personagem jogadorLocal;
    const string ENDERECO_IP = "http://127.0.0.1:3333/";
    #if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void IniciarSocketIO(string ip);
    [DllImport("__Internal")]
    private static extern void EnviarRegistroSocketIO(string ip);
    #else
    SocketIO client;
    #endif
    // Start is called before the first frame update
    void Start()
    {
        IniciarSocket();
    }

    void IniciarSocket()
    {
        #if UNITY_WEBGL

        IniciarSocketIO(ENDERECO_IP);

        #else
        
        client = new SocketIO(ENDERECO_IP, new SocketIOOptions
        {
            EIO = 4
        });

        client.OnConnected += (s, e) => 
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                AoConectar();
            }); 
        };

        client.On("jogador entrou na sala", response => 
        {
            string json = response.GetValue<string>();
            Debug.Log(json);
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadorEntrouNaSala(json);
            }); 
        });

        client.On("jogadores na sala", response => 
        {
            string json = response.GetValue<string>();
            Debug.Log(json);
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                AoTrocarDeSala(json);
            }); 
        });

        client.On("movimentacao", response => 
        {
            string json = response.GetValue<string>();
            Debug.Log(json);
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                ReceberMovimentacao(json);
            }); 
        });

        client.On("jogador saiu da sala", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadorSaiuDaSala(json);
            });
        });

        client.ConnectAsync();
        #endif
    }

    #region Eventos
    public void AoConectar()
    {
        Jogador jogador = new Jogador();
        jogador.corPrimaria = Color.green;
        jogador.corSecundaria = Color.red;
        jogador.especie = "Peixe";
        jogador.sexo = "Masculino";

        jogadorLocal.SelecionarCriatura(jogador.especie);
        jogadorLocal.SelecionarSexo(jogador.sexo);
        jogadorLocal.SelecionarCores(jogador.corPrimaria, jogador.corSecundaria);

        EnviarRegistro(jogador);
    }

    public void JogadorEntrouNaSala(string json)
    {
        JogadoresManager.Instance.RegistrarJogador(json);
    }

    public void JogadorSaiuDaSala(string json)
    {
        Id idJson = JsonUtility.FromJson<Id>(json);
        JogadoresManager.Instance.ApagarJogador(idJson.id);
    }

    public void AoTrocarDeSala(string json)
    {
        JogadoresManager.Instance.RegistrarJogadores(json);
    }

    public void ReceberMovimentacao(string json)
    {
        JogadoresManager.Instance.JogadorMovimentou(json);
    }
    #endregion

    public void EnviarRegistro(Jogador jogador)
    {
        string json = JsonUtility.ToJson(jogador);

        #if UNITY_WEBGL
        EnviarRegistroSocketIO(json);
        #else
        client.EmitAsync("registrar", json);
        #endif
    }
}

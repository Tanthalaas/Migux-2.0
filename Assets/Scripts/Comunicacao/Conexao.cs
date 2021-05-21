using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#else
using SocketIOClient;
#endif

public class Conexao : MonoBehaviour
{
    public static Conexao Instance;
    [SerializeField] Personagem jogadorLocal;
    const string ENDERECO_EXTERNO = "https://miguxservidor.herokuapp.com/";
    const string ENDERECO_LOCAL ="http://127.0.0.1:3333/";
    string enderecoIp;
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ConectarSocketIO();
    [DllImport("__Internal")]
    private static extern void EnviarRegistroSocketIO(string json);
    [DllImport("__Internal")]
    private static extern void EnviarMovimentacaoSocketIO(string json);
    [DllImport("__Internal")]
    private static extern void EnviarTrocaDeSalaSocketIO(string sala);
    [DllImport("__Internal")]
    private static extern void EnviarChatSocketIO(string json);
    #else
    SocketIO client;
    #endif
    // Start is called before the first frame update
    void Start()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        IniciarSocket();
    }

    void IniciarSocket()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        ConectarSocketIO();
        #else
        enderecoIp = ENDERECO_EXTERNO;
        client = new SocketIO(enderecoIp, new SocketIOOptions
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
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                JogadorEntrouNaSala(json);
            }); 
        });

        client.On("jogadores na sala", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                AoTrocarDeSala(json);
            }); 
        });

        client.On("movimentacao", response => 
        {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                ReceberMovimentacao(json);
            }); 
        });

        client.On("chat", response => {
            string json = response.GetValue<string>();
            UnityMainThreadDispatcher.Instance().Enqueue(() => 
            {
                ReceberChat(json);
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

        Mundo.MundoAtual.IniciarMusicaDeFundo();
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

    public void ReceberChat(string json)
    {
        JogadoresManager.Instance.JogadorMandouChat(json);
    }
    #endregion

    public void EnviarRegistro(Jogador jogador)
    {
        string json = JsonUtility.ToJson(jogador);

        #if UNITY_WEBGL && !UNITY_EDITOR
        EnviarRegistroSocketIO(json);
        #else
        client.EmitAsync("registrar", json);
        #endif
    }

    public void EnviarMovimentacao(Vector2 origem, Vector2 destino)
    {
        MovimentacaoModel movimentacao = new MovimentacaoModel(origem, destino);
        string json = JsonUtility.ToJson(movimentacao);

        #if UNITY_WEBGL && !UNITY_EDITOR
        EnviarMovimentacaoSocketIO(json);
        #else
        client.EmitAsync("movimentacao", json);
        #endif
    }

    public void EnviarTrocaDeSala(string sala)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        EnviarTrocaDeSalaSocketIO(sala);
        #else
        client.EmitAsync("trocar sala", sala);
        #endif
    }

    public void EnviarMensagem(string mensagem)
    {
        MensagemModel mensagemModel = new MensagemModel(mensagem);
        string json = JsonUtility.ToJson(mensagemModel);

        #if UNITY_WEBGL && !UNITY_EDITOR
        EnviarChatSocketIO(json);
        #else
        client.EmitAsync("chat", json);
        #endif
    }

    public Personagem GetJogadorLocal() => jogadorLocal;
}

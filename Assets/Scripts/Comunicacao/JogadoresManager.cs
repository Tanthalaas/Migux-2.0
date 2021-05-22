using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JogadoresManager : MonoBehaviour
{
    public static JogadoresManager Instance;
    [SerializeField] Personagem personagemPrefab;
    Dictionary<string, Personagem> jogadores = new Dictionary<string, Personagem>();

    private void Start() {
        if(Instance) 
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegistrarJogadores(string json)
    {
        ListaDeJogadores lista = JsonUtility.FromJson<ListaDeJogadores>(json);
        foreach(Jogador jogador in lista.jogadores)
        {
            InstanciarJogador(jogador);
        }
    }

    public void RegistrarJogador(string json)
    {
        Jogador jogador = JsonUtility.FromJson<Jogador>(json);
        InstanciarJogador(jogador);
    }

    public void JogadorMovimentou(string json)
    {
        //TODO: Rotação do jogador
        MovimentacaoModel movimentacao = JsonUtility.FromJson<MovimentacaoModel>(json);
        string id = movimentacao.id;
        
        Personagem jogador;
        if(jogadores.TryGetValue(id, out jogador))
        {
            Sala salaAtual = TrocaSalas.GetSalaAtual();

            float distancia = Vector2.Distance(movimentacao.origem, movimentacao.destino);
            float velocidade = Personagem.VELOCIDADE;
            float multiplicadorDeVelocidade = salaAtual.GetVelocidade();

            jogador.transform.position = movimentacao.origem;
            jogador.transform.DOKill();
            jogador.transform
                    .DOMove(movimentacao.destino, distancia / velocidade / multiplicadorDeVelocidade)
                    .SetUpdate(true)
                    .SetEase(Ease.Linear);
            
            jogador.OlharParaPonto(movimentacao.destino);
        }
    }

    public void JogadorMandouChat(string json)
    {
        MensagemModel mensagem = JsonUtility.FromJson<MensagemModel>(json);
        string id = mensagem.id;

        Personagem jogador;
        if(jogadores.TryGetValue(id, out jogador))
        {
            jogador.MostrarMensagem(mensagem.texto);
        }
    }

    void InstanciarJogador(Jogador jogador)
    {
        //TODO: Investigar duplicação de jogadores
        if(jogadores.ContainsKey(jogador.id)) return;

        Personagem personagem = Instantiate(personagemPrefab);
        personagem.SelecionarCriatura(jogador.especie);
        personagem.SelecionarSexo(jogador.sexo);
        personagem.SelecionarCores(jogador.corPrimaria, jogador.corSecundaria);
        jogadores.Add(jogador.id, personagem);

        float escala = TrocaSalas.GetSalaAtual().GetEscalaDoJogador();
        personagem.DefinirEscala(escala);
    }

    public void ApagarJogador(string id)
    {
        Personagem personagem;
        if(jogadores.TryGetValue(id, out personagem))
        {
            jogadores.Remove(id);
            Destroy(personagem.gameObject);
        }
    }

    public void ApagarJogadores()
    {
        foreach(Personagem jogador in jogadores.Values)
        {
            Destroy(jogador.gameObject);
        }

        jogadores = new Dictionary<string, Personagem>();
        Debug.Log(jogadores.Keys.Count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //TODO: Movimentação multiplayer
    }

    void InstanciarJogador(Jogador jogador)
    {
        Personagem personagem = Instantiate(personagemPrefab);
        personagem.SelecionarCriatura(jogador.especie);
        personagem.SelecionarSexo(jogador.sexo);
        personagem.SelecionarCores(jogador.corPrimaria, jogador.corSecundaria);
        jogadores.Add(jogador.id, personagem);
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
}

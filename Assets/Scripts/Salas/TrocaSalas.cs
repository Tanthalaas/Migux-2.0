using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaSalas : MonoBehaviour
{
    [SerializeField] Sala salaDestino;
    static Sala salaAtual;
    [SerializeField] Vector3 posicaoDestino;
    [SerializeField] float multiplicadorDeVelocidade = 1f;

    public void Trocar()
    {
        Transicao.Instance.Carregar(OcultarSalaAtual, AoTerminarTransicao);
    }
    
    public void BloquearMovimentacao()
    {
        Movimentacao.Instance.BloquearMovimentacao();
    }

    void OcultarSalaAtual()
    {
        salaAtual.gameObject.SetActive(false);
        Transicao.Instance.IniciarFalsoCarregamento();
    }

    void AoTerminarTransicao()
    {
        salaDestino.gameObject.SetActive(true);
        salaAtual = salaDestino;

        Movimentacao jogador = Movimentacao.Instance;
        Personagem personagem = jogador.GetComponent<Personagem>();

        personagem.DefinirEscala(salaDestino.GetEscalaDoJogador());
        jogador.transform.position = posicaoDestino;
        jogador.SetMultiplicadorDeVelocidade(multiplicadorDeVelocidade);

        JogadoresManager.Instance.ApagarJogadores();
        
        Conexao.Instance.EnviarTrocaDeSala(salaAtual.gameObject.name);

        Mundo.MundoAtual.TocarFundo(salaAtual.GetMusicaDeFundo());
    }

    public static void SetSalaAtual(Sala sala)
    {
        salaAtual = sala;
    }

    public static Sala GetSalaAtual() => salaAtual;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaSalas : MonoBehaviour
{
    [SerializeField] Sala salaDestino;
    [SerializeField] Sala salaAtual;
    [SerializeField] Vector3 posicaoDestino;
    [SerializeField] float multiplicadorDeVelocidade = 1f;

    public void Trocar(Movimentacao jogador)
    {
        salaAtual.gameObject.SetActive(false);
        salaDestino.gameObject.SetActive(true);

        jogador.transform.localScale = Vector3.one * salaDestino.GetEscalaDoJogador();
        jogador.transform.position = posicaoDestino;
        jogador.SetMultiplicadorDeVelocidade(multiplicadorDeVelocidade);
    }
}

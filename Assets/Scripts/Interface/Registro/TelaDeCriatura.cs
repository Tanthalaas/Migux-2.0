using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TelaDeCriatura : MonoBehaviour
{
    
    [SerializeField] GameObject conteudo;
    [SerializeField] float velocidadeDeEscolha;

    public void ProximaCriatura()
    {
        Selecao.Instance.SetCriatura(Selecao.Instance.CriaturaAtual() + 1);
        AtualizarPosicaoDoConteudo();
    }

    public void CriaturaAnterior()
    {
        Selecao.Instance.SetCriatura(Selecao.Instance.CriaturaAtual() - 1);
        AtualizarPosicaoDoConteudo();
    }

    void AtualizarPosicaoDoConteudo()
    {
        Vector3 posConteudo = conteudo.transform.localPosition;
        conteudo.transform.DOLocalMoveX(Selecao.Instance.CriaturaAtual() * -200f, velocidadeDeEscolha).SetEase(Ease.OutCubic);
    }
}

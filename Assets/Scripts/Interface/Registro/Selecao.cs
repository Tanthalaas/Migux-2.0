using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Selecao : MonoBehaviour
{
    [SerializeField] GameObject conteudo;
    [SerializeField] float velocidadeDeEscolha;
    int criaturaAtual = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProximaCriatura()
    {
        criaturaAtual++;
        if(criaturaAtual > 5)
        {
            criaturaAtual = 5;
            return;
        }
        Vector3 posConteudo = conteudo.transform.localPosition;
        conteudo.transform.DOLocalMoveX(criaturaAtual * -200f, velocidadeDeEscolha).SetEase(Ease.OutCubic);
    }

    public void CriaturaAnterior()
    {
        criaturaAtual--;
        if(criaturaAtual < 0)
        {
            criaturaAtual = 0;
            return;
        }
        Vector3 posConteudo = conteudo.transform.localPosition;
        conteudo.transform.DOLocalMoveX(criaturaAtual * -200f, velocidadeDeEscolha).SetEase(Ease.OutCubic);
    }
}

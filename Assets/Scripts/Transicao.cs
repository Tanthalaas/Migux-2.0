using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Transicao : MonoBehaviour
{
    public static Transicao Instance;
    [SerializeField] GameObject mascara, fundo;
    [SerializeField] TextMeshPro textoCarregamento;
    [SerializeField] float tempoDeTransicao;
    bool falsaTransicao;
    float porcentagem;
    Action finalDaTransicaoFalsa;

    private void Start() {
        if(Instance) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void Carregar(TweenCallback aoTransicionar, Action finalDaTransicaoFalsa = null)
    {
        this.finalDaTransicaoFalsa = finalDaTransicaoFalsa;

        fundo.SetActive(true);
        mascara.SetActive(true);
        Movimentacao.Instance.SetEmTransicao(true);

        if(finalDaTransicaoFalsa == null) textoCarregamento.gameObject.SetActive(true);

        mascara.transform
            .DOScale(Vector3.zero, tempoDeTransicao)
            .SetUpdate(true)
            .OnComplete(aoTransicionar);
    }

    public void IniciarFalsoCarregamento()
    {
        falsaTransicao = true;
        porcentagem = 100;
    }

    private void FixedUpdate() {
        textoCarregamento.text = $"{Mathf.CeilToInt(porcentagem)}%";
        if(porcentagem >= 100f)
        {
            TerminarCarregamento();
        }
    }

    void TerminarCarregamento()
    {
        falsaTransicao = false;
        porcentagem = 0;

        textoCarregamento.gameObject.SetActive(false);
        Movimentacao.Instance.SetEmTransicao(false);

        if(finalDaTransicaoFalsa != null) finalDaTransicaoFalsa();

        mascara.transform
            .DOScale(Vector3.one * 1.1f, tempoDeTransicao)
            .SetUpdate(true)
            .OnComplete(OcultarMascara);
    }

    //Economia de recursos
    void OcultarMascara()
    {
        fundo.SetActive(false);
        mascara.SetActive(false);
    }
}
